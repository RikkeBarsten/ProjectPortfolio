using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPortfolio.DAL;
using ProjectPortfolio.Models.ViewModels;

namespace ProjectPortfolio.Controllers
{
    public class IndicatorController : Controller
    {
        private PortfolioContext db = new PortfolioContext();

        // GET: Indicator
        public ActionResult Index()
        {
            DashboardClassesCollection col = new DashboardClassesCollection();

            // Setting up the list of ProjectBudget-items
            var groupB = db.AProjects.GroupBy(p => p.StartDate.Year);
            List<ProjectsBudget> dataB = new List<ProjectsBudget>();

            foreach (var group in groupB)
            {
                decimal? groupBudget = group.Sum(b => b.Budget);

                dataB.Add(new ProjectsBudget { Year = group.Key, NoProjects = group.Count(), Budget = groupBudget });
            }

            //Add the list to the collection-class
            col.BudgetList = dataB.OrderByDescending(p => p.Year).ToList();


            // Set up the list of ProgramProject-class
            var groupP = db.AProjects.GroupBy(p => p.ProgramId);
            List<ProgramProjects> dataP = new List<ProgramProjects>();

            foreach (var group in groupP)
            {
                dataP.Add(new ProgramProjects { Program = group.Key, NoProjects = group.Count() });
            }

            //Add the list to the collection class
            col.ProgramList = dataP;


            return View(col);
        }
    }
}