using System;
using System.Collections.Generic;
using System.Linq;

namespace AK8MI_SVRCEK
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Start");
            var x = RandomSearch.GetResults();
            var z = SimulatedAnnealing.GetResults();

            Helper.GenerateGraphs(x, "RS");
            Helper.GenerateGraphs(z, "SA");

            List<double> testpico = new List<double>();
            var jj = x.DJ1_Dim10.Select(y => y.AllBestCosts[1]).Average();


            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
