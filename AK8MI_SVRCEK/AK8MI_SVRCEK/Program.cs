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
            var rs = RandomSearch.GetResults();
            var sa = SimulatedAnnealing.GetResults();

            Helper.GenerateGraphs(rs, "RS");
            Helper.GenerateGraphs(sa, "SA");
            Helper.GenerateComparsionGraphs(sa, rs);




            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
