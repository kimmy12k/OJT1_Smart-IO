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
            int port = 1502;
            byte slaveId = 1;

            // DI 테스트: true / DO 테스트: false
            bool enableAutoToggle = false;

            var store = new SlaveDataStore();

            bool[] coils = new bool[16];
            store.CoilDiscretes.WritePoints(0, coils);

            var factory = new ModbusFactory();
            var slave = factory.CreateSlave(slaveId, store);

            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            var network = factory.CreateSlaveNetwork(listener);
            network.AddSlave(slave);
            Task listenTask = network.ListenAsync();

            Console.WriteLine("[SLAVE] Modbus TCP Slave Started.");
            Console.WriteLine($"[SLAVE] port: {port}, SlaveID(UnitId): {slaveId}");
            Console.WriteLine("[SLAVE] Local IP example: 127.0.0.1 or 192.168.x.x");

            if (enableAutoToggle)
                Console.WriteLine("[SLAVE] (DI test) Coil 1~16 값이 0.5초마다 토글됩니다.");
            else
                Console.WriteLine("[SLAVE] (DO test) Auto toggle OFF. Master write로만 값이 바뀝니다.");

            Console.WriteLine("Enter 누르면 종료");

            bool toggle = false;

            var toggleTimer = new System.Timers.Timer(500) { AutoReset = true };
            toggleTimer.Elapsed += (s, e) =>
            {
                toggle = !toggle;

                for (int i = 0; i < coils.Length; i++)
                    coils[i] = (i % 2 == 0) ? toggle : !toggle;

                store.CoilDiscretes.WritePoints(0, coils);
                PrintCoils(coils);
            };

            var monitorTimer = new System.Timers.Timer(1000) { AutoReset = true };
            monitorTimer.Elapsed += (s, e) =>
            {
                var current = store.CoilDiscretes.ReadPoints(0, (ushort)coils.Length);
                PrintCoils(current);
            };

            if (enableAutoToggle) toggleTimer.Start();
            else monitorTimer.Start();

            Console.ReadLine();

            try { toggleTimer.Stop(); } catch { }
            try { monitorTimer.Stop(); } catch { }
            try { listener.Stop(); } catch { }

            Console.WriteLine("[SLAVE] Stopped.");
        }

        private static void PrintCoils(bool[] coils)
        {
            string text = string.Join(",", coils.Select(b => b ? "1" : "0"));
            Console.WriteLine($"[SLAVE] Coil(1~{coils.Length}): {text}");
        }
    }
}
