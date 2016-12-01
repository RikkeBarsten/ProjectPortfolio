﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPortfolio.DAL;
using ProjectPortfolio.Models.ViewModels;
using System.Text;

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
            col.BudgetList = dataB.OrderByDescending(p => p.Year).ToArray();


            // Set up the list of ProgramProject-class
            var groupP = db.AProjects.GroupBy(p => p.ProgramId);
            List<ProgramProjects> dataP = new List<ProgramProjects>();

            foreach (var group in groupP)
            {
                dataP.Add(new ProgramProjects { Program = group.Key, NoProjects = group.Count() });
            }

            //Add the list to the collection class
            col.ProgramList = dataP;

           

            ViewBag.SequencesCsv = sequenceStringCsv();
            


          
            

            return View(col);
        }

      

        private string sequenceStringCsv()
        {           
            StringBuilder projectString = new StringBuilder();

            // First create headlines
            projectString.AppendLine("parent,child,project,budget,count");

            //For each project - create sequence-string
            foreach (var project in db.AProjects)
            {

                

                // Only include projects from last 3 years
                if (project.StartDate.Year > DateTime.Now.Year - 3)
                {

                    //Create the string-line ("primaryfocus-secondaryfocus-project-budget")
                    //projectString.Append(project.ProgramId);

                    //Check if primary focus is null
                    if (project.PrimaryFocus != null)
                    {
                        projectString.Append(project.PrimaryFocus);
                    }
                    else
                    {
                        projectString.Append("ingen");
                    }

                    //Check if secondary focus is null
                    if (project.SecondaryFocus != null)
                    {
                        projectString.Append("," + project.SecondaryFocus);
                    }
                    else
                    {
                        projectString.Append("," + "ingen");
                    }

                    projectString.Append("," + project.Name.Substring(0, (project.Name.Length < 25 ? project.Name.Length : 25)) + "...");

                    int budget = project.Budget.HasValue ? (int)project.Budget : 0;

                    projectString.Append("," + budget.ToString());
                    projectString.Append("," + 1.ToString());

                    //Create newline
                    projectString.AppendLine();
                }
            }
            return projectString.ToString();
        }

        



        
       
       
    }
}