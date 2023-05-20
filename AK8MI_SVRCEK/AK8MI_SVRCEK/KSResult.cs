using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public class KSResult
    {
        public KSResult()
        {
            AllCosts = new List<int>();
            BestArgs = new List<Item>();
            AllBestCosts = new List<int>();
        }
        public double BestCost { get; set; }
        public List<Item> BestArgs { get; set; }
        public List<int> AllCosts { get; set; }
        public List<int> AllBestCosts { get; set; }
        public TimeSpan Time { get; set; }
    }

}
