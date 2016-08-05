﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProjectPortfolio.Models;
using ProjectPortfolio.DAL;
using Bibliotek.SortSearch;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Collections.Generic;

namespace ProjectPortfolio.Controllers
{
    public class ProjectController : Controller
    {
        private PortfolioContext db = new PortfolioContext();

        // GET: Project
        public ActionResult Index(string sortOrder)
        {
            // When no sortOrder is selected (IsNullOrEmpty is true) 
            // the sort should be by name ascending - the first time true the last case
            // in the switch falls through to default. The NameSortParm is set to "name_desc"
            // so that the project name will be sorted in descending order if toggled

            ViewBag.DateStartSortParm = string.IsNullOrEmpty(sortOrder) ? "dateStart_desc" : "";
            ViewBag.DateEndSortParm = sortOrder == "dateEnd" ? "dateEnd_desc" : "dateEnd";
            ViewBag.NameSortParm =  sortOrder == "name" ? "name_desc" : "name";
            

            var projects = from p in db.AProjects
                           select p;

            switch (sortOrder)
            {
                case "dateStart_desc":
                    projects = projects.OrderByDescending(p => p.StartDate);
                    break;

                case "DateEnd":
                    projects = projects.OrderBy(p => p.EndDate);
                    break;

                case "DateEnd_desc":
                    projects = projects.OrderByDescending(p => p.EndDate);
                    break;

                case "name":
                    projects = projects.OrderBy(p => p.Name);
                    break;

                case "name_desc":
                    projects = projects.OrderByDescending(p => p.Name);
                    break;

                default:
                    projects = projects.OrderBy(p => p.StartDate);
                    break;
            }

            // projects.Include(p => p.Funder);

            

            return View(projects.ToList());
        }

       

        // GET: Project/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projects = db.AProjects.Include(p => p.Funder).Include(p => p.Program).Include(p => p.Person).Include(p => p.Files).ToList();

            Project project = projects.SingleOrDefault(p => p.ProjectId == id);

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name");
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName");
            ViewBag.FunderId = new SelectList(db.Funders, "FunderId", "Name");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,StartDate,EndDate,Description,Budget,PersonId,FunderId,ProgramId")] Project project, HttpPostedFileBase upload)
        {

            try
            {
            if (ModelState.IsValid)
            {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var application = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.application,
                            ContentType = upload.ContentType,
                        };

                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            application.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        project.Files = new List<File> { application };
                    }
                    
                //project.Id = Guid.NewGuid();  -- not necessary as set in model annotations: [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                db.AProjects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Kunne ikke gemme ændringer. Venligst forsøg igen.");
            }

            ViewBag.PersonId = new SelectList(db.People, "PersonId", "FirstName", project.PersonId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", project.ProgramId);
            ViewBag.FunderId = new SelectList(db.Funders, "FunderId", "Name", project.FunderId);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.AProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.PersonId = new SelectList(db.People, "PersonId", "FirstName", project.PersonId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", project.ProgramId);
            ViewBag.FunderId = new SelectList(db.Funders, "FunderId", "Name", project.FunderId);
            return View(project);
        }

        // POST: Project/Edit/5
        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Find the project
            var projectToUpdate = db.AProjects.Find(id);

            // Using TryUpdateModel instead of binding to prevent overposting.(the Bind attribute clears out 
            //any pre-existing data in fields not listed in the Include parameter) Properties added will be updated if there is no input, it will
            // be set to null. 
            // Projectnumber not added (will only be added by Controller Department
            if (TryUpdateModel(projectToUpdate, "",
                new string[] { "Name", "Status","StartDate", "EndDate", "Description", "Budget", "PersonId", "FunderId", "ProgramId" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Kunne ikke gemme ændringer. Venligst forsøg igen.");
                }
            }
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "FirstName", projectToUpdate.PersonId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", projectToUpdate.ProgramId);
            ViewBag.FunderId = new SelectList(db.Funders, "FunderId", "Name", projectToUpdate.FunderId);

            return View(projectToUpdate);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Kunne ikke slette projekt. Forsøg igen.";
            }
            Project project = db.AProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                Project project = db.AProjects.Find(id);
                db.AProjects.Remove(project);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
