using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolio.Models.ViewModels
{
    public class ProjectsBudget : IComparable<ProjectsBudget>
    {
        public int Year { get; set; }
        public int NoProjects { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? Budget { get; set; }

        public int CompareTo(ProjectsBudget other)
        {
            return this.Year.CompareTo(other.Year);
        }
    }
}