using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public class Result
    {
        public Result()
        {
            AllCosts = new List<double>();
            BestArgs = new List<double>();
            AllBestCosts = new List<double>();
        }
        public double BestCost { get; set; }
        public List<double> BestArgs { get; set; }
        public List<double> AllCosts { get; set; }
        public List<double> AllBestCosts { get; set; }
    }
}
