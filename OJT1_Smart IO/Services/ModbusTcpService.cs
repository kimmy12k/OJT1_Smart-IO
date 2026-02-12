using System;
using System.Net.Sockets;
using DevExpress.CodeParser;
using NModbus;
using NModbus.Device;

namespace OJT1_Smart_IO.Services
{
    public class ModbusTcpService
    {
        private TcpClient _client;
        private IModbusMaster _master;
        public int TimeoutMs { get; set; } = 2000;
        public bool IsConnected => _client != null && _client.Connected && _master != null;


        public void Connect(string ip, int port)
        {
            Disconnect();

            _client = new TcpClient();

            // ✅ timeout 적용되는 connect
            IAsyncResult ar = _client.BeginConnect(ip, port, null, null);
            if (!ar.AsyncWaitHandle.WaitOne(TimeoutMs))
            {
                try { _client.Close(); } catch { }
                _client = null;
                _master = null;
                throw new TimeoutException($"Connection timeout({TimeoutMs}ms)");
            }

            _client.EndConnect(ar);

            // ✅ send/recv timeout
            _client.ReceiveTimeout = TimeoutMs;
            _client.SendTimeout = TimeoutMs;

            try
            {
                var stream = _client.GetStream();
                stream.ReadTimeout = TimeoutMs;
                stream.WriteTimeout = TimeoutMs;
            }
            catch { }

            var factory = new ModbusFactory();
            _master = factory.CreateMaster(_client);
        }


        public void Disconnect()
        {
            try { _client?.Close(); } catch { }
            _client = null;
            _master = null;
        }

        public bool[] ReadCoils(byte unitId, ushort startAddress, ushort numberOfPoints)
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected.");
            return _master.ReadCoils(unitId, startAddress, numberOfPoints);
        }

        public bool[] ReadDiscreteInputs(byte unitId, ushort startAddress, ushort numberOfPoints)
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected.");
            return _master.ReadInputs(unitId, startAddress, numberOfPoints);
        }

        public void WriteSingleCoil(byte unitId, ushort coilAddress, bool value)
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected.");
            _master.WriteSingleCoil(unitId, coilAddress, value);
        }
    }
}