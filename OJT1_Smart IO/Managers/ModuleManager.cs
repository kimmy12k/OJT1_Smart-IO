using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.Internal;
using OJT1_Smart_IO.Models;
using OJT1_Smart_IO.Services;

namespace OJT1_Smart_IO.Managers
{
    public class ModuleManager
    {
        public const int MaxModules = 8;
        public const int ChannelsPerModule = 16;
        private readonly ModbusTcpService _modbus;
        public List<IOModule> Modules { get; } = new List<IOModule>();
        public byte SlaveId { get; set; } = 1;
        public ushort DoBaseAddress { get; set; } = 0; // Coil base
        public ModuleManager(ModbusTcpService modbus)
        {
            _modbus = modbus ?? throw new ArgumentNullException(nameof(modbus));
        }

        public void ClearModules() => Modules.Clear();

        public IOModule GetModule(int Index)
        {
            if (Index < 0 || Index >= Modules.Count) return null;// modules.Count-> modules.count- DInum 으로 수정필요
            return Modules[Index];
        }
        public IOModule AddModule(IOModule m)
        {
            if (Modules.Count >= MaxModules)
                throw new InvalidOperationException($"모듈은 최대 {MaxModules}개까지 추가할 수 있습니다.");
            var module = new IOModule
            {
                SlotIndex = m.SlotIndex,
                Type = m.Type,
                Channels = new List<IOChannel>()
            };
            EnsureChannels(module,m.Channels.Count);
            Modules.Add(module);
            RecalcDisplayIndexes();
            return module;
        }
        private static void EnsureChannels(IOModule module, int ChannelCount)
        {
            if (module.Channels == null) module.Channels = new List<IOChannel>();
            module.Channels.Clear();
            for (int i = 0; i < ChannelCount; i++)
            {
                module.Channels.Add(new IOChannel
                {
                    ChannelIndex = i,   // 0~15 (주소용)
                    Value = false
                });
            }
        }
        private void RecalcDisplayIndexes() // displayIndex
        {
            int ccount = 0;
            // ✅ 슬롯 기준으로 1~16, 17~32, 33~48 … 만들기
            for (int s = 0; s < Modules.Count; s++)
            {
                var m = Modules[s];
                for (int ch = 0; ch < m.Channels.Count; ch++)
                {
                    m.Channels[ch].DisplayIndex = ccount++;
                }
            }
        }
        private ushort MapDoAddress(int historyIndex, int channelIndex)
        {
            return (ushort)(DoBaseAddress + historyIndex + channelIndex);
        }


        public bool SetOutput(int Index, int channelIndex, int historyIndex,bool value)
        {
            var module = GetModule(Index);
            if (module == null) return false;
            if (module.Type != ModuleType.DO) return false;
            if (channelIndex < 0 || channelIndex >= module.Channels.Count) return false;
            if (!_modbus.IsConnected) return false;

            try
            {
                ushort addr = MapDoAddress(historyIndex, channelIndex);

                // ✅ 네트워크(실장치) 값 변경
                _modbus.WriteSingleCoil(SlaveId, addr, value);

                // ✅ PC 메모리(바인딩된 객체) 값 변경
                module.Channels[channelIndex].Value = value;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetOutput failed: {ex.Message}");
                return false;
            }
        }
    }

}