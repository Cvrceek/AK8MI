using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public static class TestFunctions
    {
        //vychází z definic z přednášky
        //02-Ucelova_funkce_Benchmarking.pdf
        public static double DJ1(List<double> values)
        {
            double result = 0;
            foreach(var v in values)
                result += Math.Pow(v, 2);
            return result;
        }

        public static double DJ2(List<double> values)
        {
            double result = 0;
            for (int i = 0; i < values.Count - 1; i++)
                result += (100 * Math.Pow((Math.Pow(values[i], 2) - values[i + 1]), 2)) + Math.Pow((1 - values[i]), 2);                
            return result;
        }

        public static double Schwefel(List<double> values)
        {
            double result = 0;
            foreach (var v in values)
                result += (-1 * v) * Math.Sin(Math.Sqrt(Math.Abs(v)));
            return result;
        }
    }
}
