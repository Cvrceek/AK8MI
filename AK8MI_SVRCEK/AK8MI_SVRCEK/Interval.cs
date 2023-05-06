using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public class Interval
    {
        public double From { get; set; }
        public double To { get; set; }
        public Interval(double from, double to)
        {
            From = from;
            To = to;
        }
    }
}
