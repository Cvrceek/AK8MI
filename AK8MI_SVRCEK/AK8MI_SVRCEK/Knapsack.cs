using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{

    //http://tumic.wz.cz/fel/online/36PAA/1/src/batoh_brute.c
    //http://tumic.wz.cz/fel/online/36PAA/5/
    public static class Knapsack
    {
        public static KSResult BruteForce(List<Item> values)
        {
            int maxWeight = 0;
            if (values.Count <= 15)
                maxWeight = 100;
            else if (values.Count <= 30)
                maxWeight = 200;
            else
                maxWeight = 300;





            int bestCost = 0;
            List<Item> bestArgs = new List<Item>();
            KSResult retVal = new KSResult();
            for(int i = 0; i < (1 << values.Count); i++)
            {
                int tempWeight = 0;
                int tempCost = 0;
                List<Item> tempValues = new List<Item>();

                for(int j = 0; j < values.Count; j++)
                {
                    if((i & (1 << j)) > 0)
                    {
                        tempWeight += values[j].Weight;
                        tempCost += values[j].Cost;
                        tempValues.Add(values[j]);
                    }
                }

                retVal.AllCosts.Add(tempCost);
                //váha + max cena
                if(tempWeight <= maxWeight && tempCost > bestCost)
                {
                    bestCost = tempCost;
                    bestArgs = tempValues;
                }
                retVal.AllBestCosts.Add(bestCost);
            }

            retVal.BestCost = bestCost;
            retVal.BestArgs = bestArgs;

            return retVal;
        }


        public static List<Item> GenerateItems(int count)
        {
            List<Item> retLst = new List<Item>();
            Random random = new Random(Helper.GetSeed());

            for (int i = 0; i < count; i++)
                retLst.Add(new Item()
                {
                    Weight = random.Next(1, 50),
                    Cost = random.Next(1, 50)
                });

            return retLst;
        }
    }

    public class KSResult
    {
        public KSResult()
        {
            AllCosts = new List<int>();
            BestArgs = new List<Item>();
            AllBestCosts = new List<int>();
        }
        public double BestCost { get; set; }
        public List<Item> BestArgs { get; set; }
        public List<int> AllCosts { get; set; }
        public List<int> AllBestCosts { get; set; }
    }


    public class Item
    {
        public int Cost { get; set; }
        public int Weight { get; set; }
    }


}
