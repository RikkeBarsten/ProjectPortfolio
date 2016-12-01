using System;
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

            List<Sequences> focusSequences = createSequencesList();
            List<Node> sequenceHierarchy = buildHierarchy(focusSequences);

            ViewBag.SequencesCsv = sequenceStringCsv2();
            ViewBag.Sequences = sequenceHierarchy.ToArray();


            //StringBuilder sunString = new StringBuilder();

            //foreach(var p in db.AProjects)
            //{
            //    sunString.Append(p.ProgramId + "-" + p.PrimaryFocus.Replace("-", "") + "-" + p.SecondaryFocus.Replace("-", "") + ",");
            //}
            

            //ViewBag.sunBurstString = sunString.ToString();
            

            return View(col);
        }

        //Sequence class for creating the focus-list with count
        public class Sequences
        {
            public string[] sequenceStrings { get; set; }
            public int count { get; set; }

        }

        // Sequence with string instead of string-array
        public class SequencesString
        {
            public string sequenceStrings { get; set; }
             public int count { get; set; }
        }

        private string sequenceStringCsv()
        {
            List<SequencesString> projectsFocus = new List<SequencesString>();

            Boolean sequenceExists;

            StringBuilder projectString = new StringBuilder();

            //For each project - create sequence-string, check if sequence exist, either increase count or add new sequence to list with count 1
            foreach (var project in db.AProjects)
            {

                projectString.Clear();
                
                //Create the string-line ("programid-primaryfocus-secondaryfocus")
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

                //Check if sequences exist in list, if so, add count
                sequenceExists = false;

                foreach (var item in projectsFocus)
                {
                    if(item.sequenceStrings == projectString.ToString())
                    {
                        item.count++;
                        sequenceExists = true;
                        //Go to next project
                        break;
                    }
                }
                // If the sequence did not exist, add it to the list
                if (sequenceExists == false)
                {
                    projectsFocus.Add(new SequencesString {sequenceStrings = projectString.ToString(), count = 1 });
                }
            }

            // Build the csv from the project-focuslist
            StringBuilder csvString = new StringBuilder();

            // First create headlines
            csvString.AppendLine("parent,child,count");

            foreach (var line in projectsFocus)
            {
                csvString.AppendLine(line.sequenceStrings + "," + line.count.ToString());
            }

            return csvString.ToString();
            
        }

        private string sequenceStringCsv2()
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

        private List<Sequences> createSequencesList()
        {

            List<string> temp = new List<string>();
            Boolean sequenceExists;
            
            // create sequences list
            List<Sequences> focusList = new List<Sequences>();

            //For each project - create sequence-string, check if sequence exist, either increase count or add new sequence to list with count 1
            foreach (var project in db.AProjects)
            {
                
                temp.Clear();
                //Create the string-line ("programid-primaryfocus-secondaryfocus")
                //temp.Add(project.ProgramId);

                //Check if primary focus is null
                if (project.PrimaryFocus == null)
                {
                    temp.Add("ingen");
                }
                else
                {
                    temp.Add(project.PrimaryFocus);
                }

                //Check if secondary focus is null
                
                if (project.SecondaryFocus == null)
                {
                    temp.Add("ingen");
                }
                else
                {
                    temp.Add(project.SecondaryFocus);
                }

                //Check if sequences exist in list, if so, add count
                sequenceExists = false;

                foreach (var item in focusList)
                    {
                        if (item.sequenceStrings[0] == temp[0]
                            && item.sequenceStrings[1] == temp[1])
                        {
                            item.count++;
                            sequenceExists = true;
                            //Go to next project
                            break;
                        }
                    }
                // If the sequence did not exist, add it to the list
                if (sequenceExists == false)
                {
                    string[] listToAdd = new string[3];
                    temp.CopyTo(listToAdd);

                    focusList.Add(new Sequences { sequenceStrings = listToAdd, count = 1 });
                }
            } 
            
            return focusList;
            
        }



        
        //Node class, to convert sequence-list to hierarchical list
        public class Node
        {
            public string Name { get; set; }
            public int? Count { get; set; }
            public List<Node> Children { get; set; }
        }

        private List<Node> buildHierarchy(List<Sequences> sequences)
        {
            // Set up the top node
            Node root = new Node {Name = "root", Children = new List<Node>()  };

            // Traverse the sequences-list
            foreach (var item in sequences)
            {
                //Pull out the items sequences
                string[] sequence = new string[3];
                
                item.sequenceStrings.CopyTo(sequence, 0);
                

                // Set currentNode to root to start off
                var currentNode = root;

                //Go through each string/focus in sequence to create hierarchy
                foreach (var focus in sequence)
                {
                    //Set a variable to hold the child
                    Node childNode = new Node();

                    //Set up bool to control for found children
                    bool foundChild = false;

                    //Check if there is a child, if not, add the child
                    foreach (var child in currentNode.Children)
                    {
                        // If the child already exist
                        if (child.Name == focus)
                        {
                            // set childnode to equal the existing child
                            childNode = child;
                            foundChild = true;
                            // Stop looking through children-list 
                            break;
                        }
                    }
                    // If no child was found - create new child
                    if (foundChild == false)
                    {
                        childNode.Name = focus;
                        childNode.Children = new List<Node>();
                        currentNode.Children.Add(childNode);
                    }
                    currentNode = childNode;
                }

                // Add count and set children array to null for last node
                currentNode.Count = item.count;
                currentNode.Children = null;
                
                

            }
            List<Node> result = new List<Node> { root };
            return result;

        }

       
    }
}