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

            var names = values.GetType().GetProperties().Select(x => x.Name).ToList();
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
                graf.Title = algName + "_" + names[alg];


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

                    File.WriteAllBytes("Part1Files\\grafy\\" + algName + "_" + names[alg] + ".pdf", stream.ToArray());
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
                graf_AVG.Title = algName + "_" + names[alg] + "_AVG";


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

                    File.WriteAllBytes("Part1Files\\grafy\\" + algName + "_AVG_" + names[alg] + ".pdf", stream.ToArray());
                }
                #endregion

            }


        }
        public static void GenerateComparsionGraphs(ResultInformation sa, ResultInformation rs)
        {
            var names = sa.GetType().GetProperties().Select(x => x.Name).ToList();
            var exporter = new OxyPlot.PdfExporter();

            for (int alg = 0; alg < names.Count; alg++)
            {
                Legend legend_AVG = new Legend()
                {
                    IsLegendVisible = true,
                    LegendPlacement = LegendPlacement.Outside,
                    LegendPosition = LegendPosition.TopCenter,
                    LegendOrientation = LegendOrientation.Horizontal,
                    LegendItemAlignment = HorizontalAlignment.Center
                };
                var graf = new PlotModel();
                graf.Legends.Add(legend_AVG);
                graf.IsLegendVisible = true;
                graf.Title = "Comparsion_" + names[alg];



                var resultdata_RS = ((List<Result>)rs.GetType().GetProperties()[alg].GetValue(rs));
                var rs_y = new List<double>();

                for (int i = 0; i < resultdata_RS[0].AllBestCosts.Count; i++)
                    rs_y.Add(resultdata_RS.Select(y => y.AllBestCosts[i]).Average());

                var data_RS = new LineSeries
                {
                    Color = OxyColors.Blue,
                    Title = "RS"
                };
                for (int i = 0; i < rs_y.Count; i++)
                {
                    data_RS.Points.Add(new DataPoint(i, rs_y[i]));

                }
                graf.Series.Add(data_RS);





                var resultdata_SA = ((List<Result>)sa.GetType().GetProperties()[alg].GetValue(sa));
                var sa_y = new List<double>();

                for (int i = 0; i < resultdata_SA[0].AllBestCosts.Count; i++)
                    sa_y.Add(resultdata_SA.Select(y => y.AllBestCosts[i]).Average());

                var data_SA = new LineSeries
                {
                    Color = OxyColors.Yellow,
                    Title = "SA"
                };
                for (int i = 0; i < sa_y.Count; i++)
                {
                    data_SA.Points.Add(new DataPoint(i, sa_y[i]));

                }
                graf.Series.Add(data_SA);






                using (var stream = new MemoryStream())
                {
                    exporter.Height = 600;
                    exporter.Width = 800;
                    exporter.Export(graf, stream);

                    File.WriteAllBytes("Part1Files\\grafy\\Comparsion_" + names[alg] + ".pdf", stream.ToArray());
                }
            }
        }

        public static void KSGenerateGraphs(KSResultInformation values)
        {
            var names = values.GetType().GetProperties().Select(x => x.Name).ToList();
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
                graf.Title = "Knapsack" + "_" + names[alg];


                var colors = typeof(OxyColors).GetFields();

                var resultdata = (KSResult)values.GetType().GetProperties()[alg].GetValue(values);

                var data = new LineSeries
                {
                    Color = OxyColors.Blue,
                    Title = "Váha"
                };

                for (int j = 0; j < resultdata.AllBestCosts.Count; j++)
                {
                    data.Points.Add(new DataPoint(j, resultdata.AllBestCosts[j]));
                }
                graf.Series.Add(data);

                using (var stream = new MemoryStream())
                {
                    exporter.Height = 600;
                    exporter.Width = 800;
                    exporter.Export(graf, stream);

                    File.WriteAllBytes("Part2Files\\grafy\\" + "Knapsack" + "_" + names[alg] + ".pdf", stream.ToArray());
                }
                graf = null;
                resultdata = null;
                #endregion
            }
        }

        public static void KSGenerateComparsionGraphs(KSResultInformation values)
        {
            var names = values.GetType().GetProperties().Select(x => x.Name).ToList();
            var exporter = new OxyPlot.PdfExporter();
            for (int alg = 0; alg < names.Count / 2; alg++)
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
                graf.Title = "Comparion_Knapsack" + "_" + names[alg] + "_" + names[alg + 3];


                var colors = typeof(OxyColors).GetFields();

                var resultdata1 = (KSResult)values.GetType().GetProperties()[alg].GetValue(values);
                var resultdata2 = (KSResult)values.GetType().GetProperties()[alg + 3].GetValue(values);

                var data = new LineSeries
                {
                    Color = OxyColors.Blue,
                    Title = "Váha BF"
                };

                for (int j = 0; j < resultdata1.AllBestCosts.Count; j++)
                {
                    data.Points.Add(new DataPoint(j, resultdata1.AllBestCosts[j]));
                }
                graf.Series.Add(data);

                var data2 = new LineSeries
                {
                    Color = OxyColors.Red,
                    Title = "Váha SA"
                };

                for (int j = 0; j < resultdata2.AllBestCosts.Count; j++)
                {
                    data2.Points.Add(new DataPoint(j, resultdata2.AllBestCosts[j]));
                }
                graf.Series.Add(data2);

                using (var stream = new MemoryStream())
                {
                    exporter.Height = 600;
                    exporter.Width = 800;
                    exporter.Export(graf, stream);

                    File.WriteAllBytes("Part2Files\\grafy\\" + "Comparsion_Knapsack" + "_" + names[alg] + "_" + names[alg + 3] + ".pdf", stream.ToArray());
                }
                graf = null;
                resultdata1 = null;
                resultdata2 = null;
                #endregion
            }
        }
    }
}
