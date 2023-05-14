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


        private static Result RS(int iteration, int dimension, Interval interval, int function)
        {
            Result retRst = new Result();

            double costbest = double.MaxValue;
            List<double> bestArgs = new List<double>();
            Random random = new Random(Helper.GetSeed());

            List<double> args;
            for(int i = 0; i < iteration; i++)
            {
                args = new List<double>();
                for(int j = 0; j < dimension; j++)
                {
                    double next = random.NextDouble();
                    //Console.WriteLine(next);
                    args.Add(interval.From + next * (interval.To - interval.From)); 
                }

                double tempCost;
                switch (function)
                {
                    case 1:
                        tempCost = TestFunctions.DJ1(args);
                        break;
                    case 2:
                        tempCost = TestFunctions.DJ1(args);
                        break;
                    case 3:
                        tempCost = TestFunctions.Schwefel(args);
                        break;
                    default:
                        throw new Exception("Zadaná špatná hodnota");
                }

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
    }
}
