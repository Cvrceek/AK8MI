using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{

    //http://tumic.wz.cz/fel/online/36PAA/1/src/batoh_brute.c
    //http://tumic.wz.cz/fel/online/36PAA/5/
    public static class Knapsack
    {
        public static List<Item> GenerateItems(int count)
        {
            List<Item> retLst = new List<Item>();
            Random random = new Random(Helper.GetSeed());

            //while(retLst.Count != count)
            //{
            //    var newitem = new Item()
            //    {
            //        Weight = random.Next(1, 50),
            //        Cost = random.Next(1, 50)
            //    };

            //    if (retLst.Exists(x => x.Cost == newitem.Cost && x.Weight == newitem.Weight))
            //        continue;
            //    else
            //        retLst.Add(newitem);
            //}
            for (int i = 0; i < count; i++)
                retLst.Add(new Item()
                {
                    Weight = random.Next(1, 50),
                    Cost = random.Next(1, 50)
                });

            return retLst;
        }

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
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for(int i = 0; i < (1 << values.Count); i++)
            {
                int tempWeight = 0;
                int tempCost = 0;
                List<Item> tempValues = new List<Item>();
                int iTemp = i;

                for(int j = 0; j < values.Count; j++)
                {
                    //if ((i & (1 << j)) > 0)
                    //{
                    //    tempWeight += values[j].Weight;
                    //    tempCost += values[j].Cost;
                    //    tempValues.Add(values[j]);
                    //}


                    if ((iTemp & 1) != 0)
                    {
                        tempWeight += values[j].Weight;
                        if (tempWeight < maxWeight)
                        {
                            tempCost += values[j].Cost;
                            tempValues.Add(values[j]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    iTemp = iTemp >> 1;
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
            sw.Stop();
            retVal.BestCost = bestCost;
            retVal.BestArgs = bestArgs;
            retVal.Time = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);

            return retVal;
        }

        public static KSResult SA(List<Item> values, int FES, int metropoliseIteration, int maxWeight, double minT = 0.01, double maxT = 1000, double decr = 0.98)
        {
            KSResult retRst = new KSResult();
            int actualFES = 0;
            double actualTemperature = maxT;
            SAItem vector = GenerateVector(values, maxWeight);
            int cost = vector.CostCurrentItems;
            Random random = new Random(Helper.GetSeed());

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (actualFES < FES && actualTemperature > minT)
            {
                for (int i = 0; i < metropoliseIteration && actualFES < FES; i++)
                {
                    var tempVector = GenerateVector(values, maxWeight);
                    actualFES++;
                    int deltaCost = tempVector.CostCurrentItems - vector.CostCurrentItems;
                    retRst.AllCosts.Add(tempVector.CostCurrentItems);
                        
                    if (deltaCost < 0)
                    {
                        vector = tempVector;
                        cost = tempVector.CostCurrentItems;
                    }
                    else
                    {
                        if (random.NextDouble() < Math.Exp(-deltaCost / actualTemperature))
                        {
                            vector = tempVector;
                            cost = tempVector.CostCurrentItems;
                        }
                    }
                    retRst.AllBestCosts.Add(cost);
                }
                //zvyšování skrz přednáška, kde nt ze zvedáá
                metropoliseIteration++;
                //decr * T prednaska
                actualTemperature = actualTemperature * decr;
            }
            sw.Stop();
            retRst.BestArgs = vector.Items;
            retRst.BestCost = vector.CostCurrentItems;
            retRst.Time = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
            return retRst;
        }

        private static SAItem GenerateVector(List<Item> values, int maxWeight)
        {
            Random random = new Random(Helper.GetSeed());
            SAItem retVal = new SAItem();

            while(retVal.WeightCurrentItems < maxWeight)
            {
                var value = values[random.Next(values.Count - 1)];
                if(retVal.WeightCurrentItems + value.Weight <= maxWeight)
                {
                    retVal.WeightCurrentItems += value.Weight;
                    retVal.CostCurrentItems += value.Cost;
                    retVal.Items.Add(value);
                }     
            }
            return retVal;
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
        public TimeSpan Time { get; set; }
    }


    public class Item
    {
        public int Cost { get; set; }
        public int Weight { get; set; }
    }

    public class SAItem
    {
        public List<Item> Items { get; set; }
        public int CostCurrentItems { get; set; }
        public int WeightCurrentItems { get; set; }
        public SAItem()
        {
            Items = new List<Item>();
            CostCurrentItems = 0;
            WeightCurrentItems = 0;
        }
    }
}
