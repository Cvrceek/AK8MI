using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public static class SimulatedAnnealing
    {
        private static double SA(int FES, int metropoliseIteration, int dimension, Interval interval, int function, double minT = 0.01, double maxT = 1000, double decr = 0.98)
        {
            int actualFES = 0;
            double actualTemperature = maxT;
            List<double> vector = StartVector(dimension, interval);
            double cost = GetCost(vector, function);

            while(actualFES < FES && actualTemperature > minT)
            {
                for(int i = 0; i < metropoliseIteration; i++)
                {
                    var tempVector = NextVector(vector, interval);
                    var tempCost = GetCost(tempVector, function);

                    double deltaCost = tempCost - cost;

                    if(deltaCost < 0)
                    {
                        vector = tempVector;
                        cost = tempCost;
                    }
                    else
                    {

                    }

                }
            }



            return 0;
        }


        private static double GetCost(List<double> vector, int function)
        {
            switch (function)
            {
                case 1:
                    return TestFunctions.DJ1(vector);
                case 2:
                    return TestFunctions.DJ2(vector);
                case 3:
                    return TestFunctions.Schwefel(vector);
                default:
                    return 0;
            }
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
