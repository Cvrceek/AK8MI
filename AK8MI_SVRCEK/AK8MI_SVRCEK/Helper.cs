using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public static class Helper
    {
        public static double Mean(List<double> values)
        {
            return values.Average();
        }

        public static double Median(List<double> values)
        {
            int index = (int)Math.Ceiling((double)(values.Count - 1) / 2);
            var sortedItems = values.ToList();
            sortedItems.Sort();
            
            return sortedItems[index];
        }

        public static double StandardDeviation(List<double> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }

        public static int GetSeed()
        {
            byte[] seedArray = new byte[4];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(seedArray);

            return BitConverter.ToInt32(seedArray, 0);
        }
    }
}
