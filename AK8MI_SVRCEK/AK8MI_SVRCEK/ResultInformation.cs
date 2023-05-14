using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK8MI_SVRCEK
{
    public class ResultInformation
    {
        public List<Result> DJ1_Dim10 { get; set; }
        public List<Result> DJ1_Dim5 { get; set; }
        public List<Result> DJ2_Dim10 { get; set; }
        public List<Result> DJ2_Dim5 { get; set; }
        public List<Result> Schwefel_Dim10 { get; set; }
        public List<Result> Schwefel_Dim5 { get; set; }

        public ResultInformation()
        {
            DJ1_Dim10 = new List<Result>();
            DJ1_Dim5 = new List<Result>();
            DJ2_Dim10 = new List<Result>();
            DJ2_Dim5 = new List<Result>();
            Schwefel_Dim10 = new List<Result>();
            Schwefel_Dim5 = new List<Result>();
        }

    }
}
