using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AK8MI_SVRCEK
{
    class Program
    {
        static void Main(string[] args)
        {

            Directory.CreateDirectory("Part1Files\\grafy");
            Directory.CreateDirectory("Part1Files\\statistiky");

            Directory.CreateDirectory("Part2Files\\grafy");
            Directory.CreateDirectory("Part2Files\\statistiky");

            Console.WriteLine("Pracuji na RS...");
            var rs = RandomSearch.GetResults();

            Console.WriteLine("Pracuji na SA...");
            var sa = SimulatedAnnealing.GetResults();

            Console.WriteLine("Generuji grafy pro part1...");
            Helper.GenerateGraphs(rs, "RS");
            Helper.GenerateGraphs(sa, "SA");
            Helper.GenerateComparsionGraphs(sa, rs);

            Console.WriteLine("Generuji statistiky pro part1...");
            Helper.GenerateXLSX(rs, "RS");
            Helper.GenerateXLSX(sa, "SA");



            Console.WriteLine("Pracuji na KS...");
            var ks = Knapsack.GetResults();
            
            Console.WriteLine("Generuji grafy pro part2...");
            Helper.KSGenerateGraphs(ks);
            Helper.KSGenerateComparsionGraphs(ks);

            Console.WriteLine("Generuji statistiky pro part2...");
            Helper.KSGenerateXLSX(ks);


            Console.WriteLine("Jsem hotov...");
            Console.ReadLine();
        }
    }
}
