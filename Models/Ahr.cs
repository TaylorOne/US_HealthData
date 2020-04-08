using System;
using System.Collections.Generic;

namespace USHD.Models
{
    public partial class Ahr
    {
        public int AhrId { get; set; }
        public int? Year { get; set; }
        public string Measure { get; set; }
        public string State { get; set; }
        public int? Rank { get; set; }
        public decimal? Value { get; set; }
        public decimal? Score { get; set; }
        public decimal? LowerCi { get; set; }
        public decimal? UpperCi { get; set; }
        public string Source { get; set; }
    }
}
