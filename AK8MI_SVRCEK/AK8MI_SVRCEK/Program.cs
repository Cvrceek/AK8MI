using System;

namespace AK8MI_SVRCEK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var x = RandomSearch.GetResults();
            var z = SimulatedAnnealing.GetResults();
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
