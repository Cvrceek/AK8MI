using System;
using System.Collections.Generic;

namespace AK8MI_SVRCEK
{
    public static class RandomSearch
    {

        public static ResultInformation GetResults()
        {
            ResultInformation ri = new ResultInformation();

            for (int i = 0; i < 30; i++)
            {
                ri.DJ1_Dim10.Add(RSDJ1(10000, 10, new Interval(-5, 5)));
                ri.DJ1_Dim5.Add(RSDJ1(10000, 5, new Interval(-5, 5)));

                ri.DJ2_Dim10.Add(RSDJ2(10000, 10, new Interval(-5, 5)));
                ri.DJ2_Dim5.Add(RSDJ2(10000, 5, new Interval(-5, 5)));

                ri.Schwefel_Dim10.Add(RSSchwefel(10000, 10, new Interval(-500, 500)));
                ri.Schwefel_Dim5.Add(RSSchwefel(10000, 5, new Interval(-500, 500)));
            }
            return ri;
        }

  


        private static Result RSDJ1(int FES, int dimension, Interval interval) => RS(FES, dimension, interval, 1);
        private static Result RSDJ2(int FES, int dimension, Interval interval) => RS(FES, dimension, interval, 2);
        private static Result RSSchwefel(int FES, int dimension, Interval interval) => RS(FES, dimension, interval, 3);


        private static Result RS(int iteration, int dimension, Interval interval, int function, double nextDiff = 0.01)
        {
            Result retRst = new Result();

            List<double> bestArgs = StartArgs(dimension, interval);
            double costbest = GetCost(bestArgs, function);

            List<double> args;
            for(int i = 0; i < iteration; i++)
            {
                args = NextArgs(bestArgs, dimension, interval, nextDiff);
                double tempCost = GetCost(args, function);

                retRst.AllCosts.Add(tempCost);

                if (tempCost < costbest)
                {
                    costbest = tempCost;
                    bestArgs = args;
                }
                retRst.AllBestCosts.Add(costbest);

                //Console.WriteLine(costbest);
            }
            retRst.BestCost = costbest;
            retRst.BestArgs = bestArgs;
            return retRst;
        }

        private static List<double> StartArgs(int dimension, Interval interval)
        {
            Random random = new Random(Helper.GetSeed());

            List<double> retLst = new List<double>();
            for (int j = 0; j < dimension; j++)
            {
                double next = random.NextDouble();
                retLst.Add(interval.From + next * (interval.To - interval.From));
            }
            return retLst;
        }

        private static List<double> NextArgs(List<double> args, int dimension, Interval interval, double diff)
        {
            Random random = new Random(Helper.GetSeed());

            List<double> retLst = new List<double>();
            for (int j = 0; j < dimension; j++)
            {
                //10% rozsah...
                retLst.Add(Math.Max(interval.From, Math.Min(interval.To, args[j] + ((random.NextDouble() * 2 - 1) * (diff * (interval.To - interval.From))))));
            }
            return retLst;
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
    }
}
