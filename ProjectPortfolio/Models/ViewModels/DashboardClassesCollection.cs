using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models.ViewModels
{
    public class DashboardClassesCollection
    {
        public ProjectsBudget[] BudgetList { get; set; }
        public List<ProgramProjects> ProgramList { get; set; }
    }
}