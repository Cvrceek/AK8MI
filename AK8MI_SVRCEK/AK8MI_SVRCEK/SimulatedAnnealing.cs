using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public static class SimulatedAnnealing
    {
        public static ResultInformation GetResults()
        {
            ResultInformation ri = new ResultInformation();
            for (int i = 0; i < 30; i++)
            {
                ri.DJ1_Dim10.Add(SADJ1(10000, 10, new Interval(-5, 5)));
                ri.DJ1_Dim5.Add(SADJ1(10000, 5, new Interval(-5, 5)));

                ri.DJ2_Dim10.Add(SADJ2(10000, 10, new Interval(-5, 5)));
                ri.DJ2_Dim5.Add(SADJ2(10000, 5, new Interval(-5, 5)));

                ri.Schwefel_Dim10.Add(SASchwefel(10000, 10, new Interval(-500, 500)));
                ri.Schwefel_Dim5.Add(SASchwefel(10000, 5, new Interval(-500, 500)));
            }
            return ri;
        }



        private static double SADJ1(int FES, int dimension, Interval interval) => SA(FES, 10, dimension, interval, 1);
        private static double SADJ2(int FES, int dimension, Interval interval) => SA(FES, 10, dimension, interval, 2);
        private static double SASchwefel(int FES, int dimension, Interval interval) => SA(FES,10, dimension, interval, 3);




        private static double SA(int FES, int metropoliseIteration, int dimension, Interval interval, int function, double minT = 0.01, double maxT = 1000, double decr = 0.98)
        {
            int actualFES = 0;
            double actualTemperature = maxT;
            List<double> vector = StartVector(dimension, interval);
            double cost = GetCost(vector, function);

            Random random = new Random(Helper.GetSeed());

            while(actualFES < FES && actualTemperature > minT)
            {
                for(int i = 0; i < metropoliseIteration && actualFES < FES; i++)
                {
                    var tempVector = NextVector(vector, interval);
                    var tempCost = GetCost(tempVector, function);
                    actualFES++;
                    double deltaCost = tempCost - cost;

                    if (deltaCost < 0)
                    {
                        vector = tempVector;
                        cost = tempCost;
                    }
                    else
                    {
                        if (random.NextDouble() < Math.Exp(-deltaCost / actualTemperature))
                        {
                            vector = tempVector;
                            cost = tempCost;
                        }
                    }
                }
                //zvyšování skrz přednáška, kde nt ze zvedáá
                metropoliseIteration++;
                //decr * T prednaska
                actualTemperature = actualTemperature * decr;
            }
            return cost;
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
