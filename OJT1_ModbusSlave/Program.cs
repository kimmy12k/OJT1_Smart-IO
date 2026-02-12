using NModbus;
using NModbus.Data;
using NModbus.Device;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OJT1_ModbusSlave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 502는 권한 이슈가 많아서 테스트는 1502 추천
            int port = 1502;
            byte slaveId = 1;

            // true면 자동 토글(테스트), false면 콘솔 입력/마스터 write로만 변경
            bool enableAutoToggle = false;

            int coilCount = 16; // 0xxxx (Coils) 개수

            // ✅ NModbus4에서는 IModbusDataStore가 아니라 ISlaveDataStore를 쓰는 게 안전
            ISlaveDataStore store = new SlaveDataStore();

            // 초기화(전부 false)
            store.CoilDiscretes.WritePoints(0, new bool[coilCount]);

            var factory = new ModbusFactory();
            var slave = factory.CreateSlave(slaveId, store);

            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            var network = factory.CreateSlaveNetwork(listener);
            network.AddSlave(slave);
            Task listenTask = network.ListenAsync();

            Console.WriteLine("[SLAVE] Modbus TCP Slave Started.");
            Console.WriteLine($"[SLAVE] port: {port}, SlaveID(UnitId): {slaveId}");
            Console.WriteLine("[SLAVE] Master는 ReadCoils(0xxxx)로 읽으세요.");
            Console.WriteLine("명령:");
            Console.WriteLine("  on  <idx>   예) on 3   -> Coil 3 ON");
            Console.WriteLine("  off <idx>   예) off 3  -> Coil 3 OFF");
            Console.WriteLine("  toggle      -> 전체 자동 토글 ON/OFF");
            Console.WriteLine("  print       -> 현재 코일 상태 출력");
            Console.WriteLine("  exit        -> 종료");
            Console.WriteLine();

            bool toggle = false;
            var toggleTimer = new System.Timers.Timer(500) { AutoReset = true };
            toggleTimer.Elapsed += (s, e) =>
            {
                toggle = !toggle;
                var coils = new bool[coilCount];
                for (int i = 0; i < coilCount; i++)
                    coils[i] = (i % 2 == 0) ? toggle : !toggle;

                store.CoilDiscretes.WritePoints(0, coils);
                PrintCoils(store, coilCount);
            };

            if (enableAutoToggle) toggleTimer.Start();

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (line == null) continue;

                line = line.Trim();
                if (line.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (line.Equals("print", StringComparison.OrdinalIgnoreCase))
                {
                    PrintCoils(store, coilCount);
                    continue;
                }

                if (line.Equals("toggle", StringComparison.OrdinalIgnoreCase))
                {
                    if (toggleTimer.Enabled)
                    {
                        toggleTimer.Stop();
                        Console.WriteLine("[SLAVE] Auto toggle OFF");
                    }
                    else
                    {
                        toggleTimer.Start();
                        Console.WriteLine("[SLAVE] Auto toggle ON (0.5s)");
                    }
                    continue;
                }

                // ✅ Split 오류 방지: char[] + 옵션 형태로 써야 함
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2 && int.TryParse(parts[1], out int idx))
                {
                    if (idx < 0 || idx >= coilCount)
                    {
                        Console.WriteLine($"idx 범위 오류: 0 ~ {coilCount - 1}");
                        continue;
                    }

                    var cmd = parts[0].ToLowerInvariant();
                    if (cmd == "on" || cmd == "off")
                    {
                        bool value = (cmd == "on");

                        var current = store.CoilDiscretes.ReadPoints(0, (ushort)coilCount);
                        current[idx] = value;
                        store.CoilDiscretes.WritePoints(0, current);

                        Console.WriteLine($"[SLAVE] Coil[{idx}] = {(value ? 1 : 0)}");
                        PrintCoils(store, coilCount);
                        continue;
                    }
                }

                Console.WriteLine("명령 형식이 올바르지 않습니다. (on/off idx, toggle, print, exit)");
            }

            try { toggleTimer.Stop(); } catch { }
            try { listener.Stop(); } catch { }

            Console.WriteLine("[SLAVE] Stopped.");
        }

        private static void PrintCoils(ISlaveDataStore store, int coilCount)
        {
            var coils = store.CoilDiscretes.ReadPoints(0, (ushort)coilCount);
            string text = string.Join(",", coils.Select(b => b ? "1" : "0"));
            Console.WriteLine($"[SLAVE] Coil(0~{coilCount - 1}): {text}");
        }
    }
}
