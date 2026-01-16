using System;
using System.Collections.Generic;
using OJT1_Smart_IO.Models;
using OJT1_Smart_IO.Services;

namespace OJT1_Smart_IO.Managers
{
    public class ModuleManager
    {
        private readonly ModbusTcpService _modbus;

        public List<IOModule> Modules { get; } = new List<IOModule>();

        public byte SlaveId { get; set; } = 1;

        // DO Coil 베이스 주소
        public ushort DoBaseAddress { get; set; } = 0;

        public ModuleManager(ModbusTcpService modbus)
        {
            _modbus = modbus ?? throw new ArgumentNullException(nameof(modbus));
        }

        public IOModule GetModule(int slotIndex)
        {
            return Modules.Find(m => m.SlotIndex == slotIndex);
        }

        private ushort MapDoAddress(int slotIndex, int channelIndex)
        {
            return (ushort)(DoBaseAddress + (slotIndex * 16) + channelIndex);
        }

        public bool SetOutput(int slotIndex, int channelIndex, bool value)
        {
            var module = GetModule(slotIndex);
            if (module == null) return false;
            if (module.Type != ModuleType.DO) return false;
            if (channelIndex < 0 || channelIndex >= module.Channels.Count) return false;
            if (!_modbus.IsConnected) return false;

            try
            {
                ushort addr = MapDoAddress(slotIndex, channelIndex);

                _modbus.WriteSingleCoil(SlaveId, addr, value);

                module.Channels[channelIndex].Value = value;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetOutput failed: {ex.Message}");
                return false;
            }
        }

        public bool UpdateInput(int slotIndex, int channelIndex, bool value)
        {
            var module = GetModule(slotIndex);
            if (module == null) return false;
            if (module.Type != ModuleType.DI) return false;
            if (channelIndex < 0 || channelIndex >= module.Channels.Count) return false;

            module.Channels[channelIndex].Value = value;
            return true;
        }

        public void ReindexSlots()
        {
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].SlotIndex = i;
        }
    }
}
