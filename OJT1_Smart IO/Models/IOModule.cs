using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OJT1_Smart_IO.Models
{
    public class IOModule
    {
        public int SlotIndex { get; set; }
        public int HistoryIndex { get; set; }
        public ModuleType Type { get; set; }
        public List<IOChannel> Channels { get;  set; } = new List<IOChannel>();
    }
}
