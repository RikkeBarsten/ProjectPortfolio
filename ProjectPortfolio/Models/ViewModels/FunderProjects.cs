using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models.ViewModels
{
    public class FunderProjects
    {
        public IEnumerable<Funder> Funders { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}