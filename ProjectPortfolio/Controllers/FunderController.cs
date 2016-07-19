using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectPortfolio.DAL;
using ProjectPortfolio.Models;
using ProjectPortfolio.Models.ViewModels;

namespace ProjectPortfolio.Controllers
{
    public class FunderController : Controller
    {
        private PortfolioContext db = new PortfolioContext();

        // GET: Funder
        public ActionResult Index(int? id)
        {
            var fundersprojects = new FunderProjects();
            fundersprojects.Funders = db.Funders.Include(f => f.Projects);

            if (id != null)
            {
                ViewBag.FunderId = id.Value;
                fundersprojects.Projects = fundersprojects.Funders.Where(
                    f => f.FunderId == id.Value).Single().Projects;
            }

            return View(fundersprojects);
        }

        // GET: Funder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funder funder = db.Funders.Find(id);
            if (funder == null)
            {
                return HttpNotFound();
            }
            return View(funder);
        }

        // GET: Funder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FunderId,Name,Description")] Funder funder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Funders.Add(funder);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Kunne ikke gemme ændringerne.Forsøg venligst igen.");
            }

            return View(funder);
        }

        // GET: Funder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funder funder = db.Funders.Find(id);
            if (funder == null)
            {
                return HttpNotFound();
            }
            return View(funder);
        }

        // POST: Funder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var funderToUpdate = db.Funders.Find(id);
            if (TryUpdateModel(funderToUpdate, "", 
                new string[] {"Name", "Description" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Kunne ikke gemme ændringerne. Forsøg venligst igen.");
                }
            }

            return View(funderToUpdate);
        }

        // GET: Funder/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Kunne ikke slette fond. Forsøg igen.";
            }
            Funder funder = db.Funders.Find(id);
            if (funder == null)
            {
                return HttpNotFound();
            }
            return View(funder);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Funder funder = db.Funders.Find(id);
                db.Funders.Remove(funder);
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
