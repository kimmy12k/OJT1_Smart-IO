using System;
using System.Net.Sockets;
using NModbus;

namespace OJT1_Smart_IO.Services
{
    public class ModbusTcpService : IDisposable
    {
        private TcpClient _client;
        private IModbusMaster _master;

        public bool IsConnected => _client != null && _client.Connected;

        public void Connect(string ip, int port = 1502)
        {
            Disconnect();

            _client = new TcpClient();
            _client.Connect(ip, port);

            var factory = new ModbusFactory();
            _master = factory.CreateMaster(_client);
        }

        public void Disconnect()
        {
            try { _client?.Close(); } catch { }
            _client = null;
            _master = null;
        }

        private void EnsureConnected()
        {
            if (!IsConnected || _master == null)
                throw new InvalidOperationException("Modbus not connected.");
        }

        // Discrete Inputs (Input Discretes)
        public bool[] ReadDiscreteInputs(byte slaveId, ushort startAddress, ushort count)
        {
            EnsureConnected();
            return _master.ReadInputs(slaveId, startAddress, count);
        }

        // Holding Registers
        public ushort[] ReadHoldingRegisters(byte slaveId, ushort startAddress, ushort count)
        {
            EnsureConnected();
            return _master.ReadHoldingRegisters(slaveId, startAddress, count);
        }

        // Coils (Read)
        public bool[] ReadCoils(byte slaveId, ushort startAddress, ushort count)
        {
            EnsureConnected();
            return _master.ReadCoils(slaveId, startAddress, count);
        }

        // Coils (Write)
        public void WriteSingleCoil(byte slaveId, ushort address, bool value)
        {
            EnsureConnected();
            _master.WriteSingleCoil(slaveId, address, value);
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
