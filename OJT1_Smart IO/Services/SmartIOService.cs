using System;
using System.Threading;
using System.Threading.Tasks;

namespace OJT1_Smart_IO.Services
{
    public class SmartIOService
    {
        private readonly ModbusTcpService _modbus;
        private readonly Random _rand = new Random();

        private CancellationTokenSource _cts;
        private Task _pollTask;

        public SmartIOService(ModbusTcpService modbus)
        {
            _modbus = modbus ?? throw new ArgumentNullException(nameof(modbus));
        }

        // true: Fake / false: Real Modbus
        public bool TestMode { get; set; } = true;

        public ushort DiStart { get; set; } = 0;
        public ushort DiCount { get; set; } = 16;

        public ushort AiStart { get; set; } = 0;
        public ushort AiCount { get; set; } = 8;

        public int PollIntervalMs { get; set; } = 500;

        public bool IsConnected => _modbus.IsConnected;

        public event Action<bool> ConnectionChanged;
        public event Action<bool[]> DIUpdated;
        public event Action<ushort[]> AIUpdated;
        public event Action<string> ErrorOccurred;

        public void Connect(string ip, int port, byte unitId)
        {
            try
            {
                _modbus.Connect(ip, port);
                ConnectionChanged?.Invoke(true);
                StartPolling(unitId);
            }
            catch (Exception ex)
            {
                ConnectionChanged?.Invoke(false);
                ErrorOccurred?.Invoke(ex.Message);
            }
        }

        public void Disconnect()
        {
            StopPolling();
            _modbus.Disconnect();
            ConnectionChanged?.Invoke(false);
        }

        private void StartPolling(byte unitId)
        {
            StopPolling();

            _cts = new CancellationTokenSource();
            _pollTask = Task.Run(() => PollLoopAsync(unitId, _cts.Token));
        }

        private void StopPolling()
        {
            try
            {
                _cts?.Cancel();
            }
            catch { }

            // CTS 자원 정리
            try
            {
                _cts?.Dispose();
            }
            catch { }

            _cts = null;
            _pollTask = null;
        }

        private async Task PollLoopAsync(byte unitId, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (!IsConnected) break;

                    if (TestMode)
                    {
                        DIUpdated?.Invoke(FakeDI((int)DiCount));
                        AIUpdated?.Invoke(FakeAI((int)AiCount));
                    }
                    else
                    {
                        // ✅ 테스트 Slave 기준: Coil을 DI처럼 읽기
                        var di = _modbus.ReadCoils(unitId, DiStart, DiCount);
                        var ai = _modbus.ReadHoldingRegisters(unitId, AiStart, AiCount);

                        DIUpdated?.Invoke(di);
                        AIUpdated?.Invoke(ai);
                    }
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(ex.Message);
                }

                try
                {
                    await Task.Delay(PollIntervalMs, token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private bool[] FakeDI(int count)
        {
            var arr = new bool[count];
            for (int i = 0; i < count; i++)
                arr[i] = _rand.Next(0, 2) == 1;
            return arr;
        }

        private ushort[] FakeAI(int count)
        {
            var arr = new ushort[count];
            for (int i = 0; i < count; i++)
                arr[i] = (ushort)_rand.Next(0, 4000);
            return arr;
        }
    }
}
