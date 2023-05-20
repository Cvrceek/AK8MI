using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public class KSResultInformation
    {
        public KSResult BF1 { get; set; }
        public KSResult BF2 { get; set; }
        public KSResult BF3 { get; set; }
        public KSResult SA1 { get; set; }
        public KSResult SA2 { get; set; }
        public KSResult SA3 { get; set; }
        public List<Item> Items1 { get; set; }
        public List<Item> Items2 { get; set; }
        public List<Item> Items3 { get; set; }

        public KSResultInformation()
        {
            Items1 = new List<Item>();
            Items2 = new List<Item>();
            Items3 = new List<Item>();
        }
    }
}
