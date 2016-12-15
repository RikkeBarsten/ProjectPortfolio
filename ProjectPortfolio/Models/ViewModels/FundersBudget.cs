using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models.ViewModels
{
    public class FundersBudget
    {
        public string Funder { get; set; }
        public int Year { get; set; }
        public decimal? Budget { get; set; }
    }
}