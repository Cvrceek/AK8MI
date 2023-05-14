using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;

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

        /// <summary>
        /// generovaní grafů, pro každý alg zvlášť... 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="algName"></param>
        public static void GenerateGraphs(ResultInformation values, string algName)
        {

            var names = values.GetType().GetProperties().Select(x=> x.Name).ToList();
            var exporter = new OxyPlot.PdfExporter();

            for (int alg = 0; alg < names.Count; alg++)
            {
                #region Iterace
                Legend legend = new Legend()
                {
                    IsLegendVisible = true,
                    LegendPlacement = LegendPlacement.Outside,
                    LegendPosition = LegendPosition.TopCenter,
                    LegendOrientation = LegendOrientation.Horizontal,
                    LegendItemAlignment = HorizontalAlignment.Center
                };
                var graf = new PlotModel();
                graf.Legends.Add(legend);
                graf.IsLegendVisible = true;
                graf.Title = names[alg];


                var colors = typeof(OxyColors).GetFields();

                var resultdata = ((List<Result>)values.GetType().GetProperties()[alg].GetValue(values));

                for (int i = 0; i < resultdata.Count; i++)
                {
                    var data = new LineSeries
                    {
                        Color = (OxyColor)colors[i].GetValue(null),
                        Title = "Iterace " + (i + 1).ToString()
                    };

                    for (int j = 0; j < resultdata[i].AllBestCosts.Count; j++)
                    {
                        data.Points.Add(new DataPoint(j, resultdata[i].AllBestCosts[j]));
                    }
                    graf.Series.Add(data);
                }

                using (var stream = new MemoryStream())
                {
                    exporter.Height = 600;
                    exporter.Width = 800;
                    exporter.Export(graf, stream);

                    File.WriteAllBytes("grafy\\" + algName + "_" + names[alg] + ".pdf", stream.ToArray());
                }
                #endregion

                #region AVG
                Legend legend_AVG = new Legend()
                {
                    IsLegendVisible = true,
                    LegendPlacement = LegendPlacement.Outside,
                    LegendPosition = LegendPosition.TopCenter,
                    LegendOrientation = LegendOrientation.Horizontal,
                    LegendItemAlignment = HorizontalAlignment.Center
                };
                var graf_AVG = new PlotModel();
                graf_AVG.Legends.Add(legend_AVG);
                graf_AVG.IsLegendVisible = true;
                graf_AVG.Title = names[alg] + "_AVG";


                var resultdata_AVG = ((List<Result>)values.GetType().GetProperties()[alg].GetValue(values));
                var y = new List<double>();

                for (int i = 0; i < resultdata_AVG[0].AllBestCosts.Count; i++)
                    y.Add(resultdata_AVG.Select(y => y.AllBestCosts[i]).Average());

                var data_AVG = new LineSeries
                {
                    Color = OxyColors.Blue,
                    Title = "AVG"
                };
                for (int i = 0; i < y.Count; i++)
                {
                    data_AVG.Points.Add(new DataPoint(i, y[i]));

                }
                graf_AVG.Series.Add(data_AVG);
                using (var stream = new MemoryStream())
                {
                    exporter.Height = 600;
                    exporter.Width = 800;
                    exporter.Export(graf_AVG, stream);

                    File.WriteAllBytes("grafy\\" + algName + "_AVG_" + names[alg] + ".pdf", stream.ToArray());
                }
                #endregion

            }


        }
    }
}
