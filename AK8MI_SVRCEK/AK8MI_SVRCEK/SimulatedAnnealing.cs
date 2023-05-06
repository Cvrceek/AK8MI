using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public static class SimulatedAnnealing
    {
        private static double SA(int iteration, int metropoliseIteration, int dimension, Interval interval, int function, double minT = 0.01, double maxT = 1000, double alfa = 0.98)
        {
            return 0;
        }

        private static List<double> StartVector(int dimension, Interval interval)
        {
            Random random = new Random(Helper.GetSeed());
            List<double> retLst = new List<double>();
            for(int i = 0; i< dimension; i++)
            {
                retLst.Add(random.NextDouble() * (interval.To - interval.From) + interval.From);
            }
            return retLst;
        }

        private static List<double> NextVector(List<double> vector, Interval interval)
        {
            var clone = vector.ToList();
            Random random = new Random(Helper.GetSeed());
            int index = random.Next(0, clone.Count);
            clone[index] = random.NextDouble() * (interval.To - interval.From) + interval.From;
            return clone;
        }




    }
}
