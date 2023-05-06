using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public class ResultInformation
    {
        public List<double> DJ1_Dim10 { get; set; }
        public List<double> DJ1_Dim5 { get; set; }
        public List<double> DJ2_Dim10 { get; set; }
        public List<double> DJ2_Dim5 { get; set; }
        public List<double> Schwefel_Dim10 { get; set; }
        public List<double> Schwefel_Dim5 { get; set; }

        public ResultInformation()
        {
            DJ1_Dim10 = new List<double>();
            DJ1_Dim5 = new List<double>();
            DJ2_Dim10 = new List<double>();
            DJ2_Dim5 = new List<double>();
            Schwefel_Dim10 = new List<double>();
            Schwefel_Dim5 = new List<double>();
        }

    }
}
