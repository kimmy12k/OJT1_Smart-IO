using DevExpress.CodeParser;
using DevExpress.Diagram.Core.Shapes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OJT1_Smart_IO.Services
{
    public class SmartIOService
    {
        public bool EnableDIPolling { get; set; } = false;
        public event Action<string> Log;
        private readonly ModbusTcpService _modbus;//SmartIOService는 통신 구현을 직접 하지 않고 _modbus에 위임한다.
        private readonly Random _rand = new Random(); // TestMode일 때 DI/AI 랜덤값 생성에 사용.
        private CancellationTokenSource _cts; // 폴링 중단용 토큰(Cancel 호출)
        private Task _pollTask;// 백그라운드 폴링 작업
        public SmartIOService(ModbusTcpService modbus)
        {
            _modbus = modbus ?? throw new ArgumentNullException(nameof(modbus));//통신 객체를 1개만 공유해서 연결상태/리소스가 꼬이지 않게.  null이 들어오면 예외를 발생시켜 현재 실행을 중단하고 호출자에게 오류를 전달
        }
        // true: Fake / false: Real Modbus
        public bool TestMode { get; set; } = false;// true면 FakeDI, FakeAI로 값 생성 -> 시험모드  false면 _modbus.ReadCoils, _modbus.ReadHoldingRegisters을 통해 실제 값 읽음
      
        public ushort DiStart { get; set; } = 0;
        public ushort DiCount { get; set; } = 16;
        public int PollIntervalMs { get; set; } = 500;//몇 ms마다 읽을지,Task.Delay(PollIntervalMs, token)에 들어가 반복주기 결정,Form1의 spinPollMs가 여기에 반영됨.
        public bool IsConnected => _modbus.IsConnected;//내부 _modbus 연결 상태를 외부에서도 확인 가능하게 노출.
        public event Action<bool> ConnectionChanged;// true/false로 연결 상태 변화 통지, Form1에서 lblConn 등을 바꿈.
        public event Action<bool[]> DIUpdated;//읽어온 DI 결과(bool[])를 UI에 전달, Form1의 OnDIUpdated(bool[] di)가 여기서 호출됨.
        public event Action<ushort[]> AIUpdated;//AI(홀딩레지스터) 결과를 전달, 현재 Form1에서는 UI 미구현 상태였음.
        public event Action<string> ErrorOccurred;//예외 메시지를 UI에 전달 (MessageBox 등)
        public void Connect(string ip, int port, byte unitId)
        {
            try
            {
                Disconnect();
                _modbus.Connect(ip, port);
                ConnectionChanged?.Invoke(true);//“연결 성공”을 구독자(Form1)에 알림, Form1의 OnConnectionChanged(true)가 호출되는 효과
                StartPolling(unitId);// 연결되었으니 이제 Task로 주기적으로 읽기 시작
            }
            catch (Exception ex)
            {
                try { _modbus.Disconnect(); } catch { }
                ConnectionChanged?.Invoke(false);//실패 시 연결 false 알림
                ErrorOccurred?.Invoke(ex.Message);//에러 메시지 전달
                throw;
            }
        }
        public void Disconnect()
        {
            StopPolling();//폴링 Task 취소
            _modbus.Disconnect();//소켓 닫기
            ConnectionChanged?.Invoke(false);//“끊김” 알림
        }

        private void StartPolling(byte unitId)
        {
            StopPolling();//Connect를 여러 번 눌러도 Task가 여러 개 생기지 않게 방지
            _cts = new CancellationTokenSource();//취소 토큰 준비
            _pollTask = Task.Run(() => PollLoopAsync(unitId, _cts.Token));//PollLoopAsync를 백그라운드에서 실행
        }
        private void StopPolling()
        {
            try
            {
                _cts?.Cancel(); //토큰 취소 요청
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
                    if (!IsConnected) break;//Cancel되지 않는 동안 계속 반복,  연결이 끊기면 break로 종료 
                    if (!EnableDIPolling)
                    {
                        await Task.Delay(PollIntervalMs, token);
                        continue;
                    }

                    if (TestMode)//실제 통신 없이 랜덤 값 생성,생성된 값이 바로 이벤트로 UI에 전달됨
                    {
                        DIUpdated?.Invoke(FakeDI((int)DiCount));
                    }
                    else
                    {
                        var di = _modbus.ReadDiscreteInputs(unitId, DiStart, DiCount);

                        DIUpdated?.Invoke(di); // form1.cs에 전달                     
                    }
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(ex.Message);//통신 오류, 연결 끊김 등 발생 시 UI에 메시지 전달,루프는 계속 돌 수도 있음(여기서는 break 안 함)
                    try { _modbus.Disconnect(); } catch { }
                    ConnectionChanged?.Invoke(false);
                    break;
                }
                try
                {
                    await Task.Delay(PollIntervalMs, token);//PollIntervalMs 만큼 기다렸다가 다시 읽음
                }
                catch (TaskCanceledException)//Cancel 되면 TaskCanceledException 발생 → break로 종료
                {
                    break;
                }
            }
        }

        private bool[] FakeDI(int count) // di test 모드
        {
            var arr = new bool[count];
            for (int i = 0; i < count; i++)
                arr[i] = _rand.Next(0, 2) == 1;
            return arr;
        }



    }
}