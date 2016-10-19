using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPortfolio.DAL;
using System.IO;
using System.Data.Entity;
using System.Net;
using ProjectPortfolio.Models;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Web.Routing;

namespace ProjectPortfolio.Controllers
{
    public class FileController : Controller
    {

        private PortfolioContext db = new PortfolioContext();
        
        // GET: File
        public ActionResult Index(int id)
        {

            var fileToRetrieve = db.Files.Find(id);

            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        //POST: Delete

        public ActionResult Delete(int id, Guid p_id)
        {
            var fileToDelete = db.Files.Find(id);
            db.Files.Remove(fileToDelete);
            db.SaveChanges();

            return RedirectToAction("Edit","Project", new {id=p_id });
        }

        //GET: Csv
        public ActionResult ExportToCsv()
        {
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Projekt\";\"Projektnummer\";\"Status\";\"Start\";\"Slut\";\"Beskrivelse\";\"Budget\";\"Egenandel\";\"Flere partnere\";\"Eksternt projektnummmer\";\"Ejer/partner\";\"Samlet budget\";\"Bevillingstype\";\"Strategisk program\";\"Primært indsatsområde\";\"Sekundært indsatsområde\";\"Kontaktperson\";\"Ansvarlig\";\"Formålsnummer\";\"Link\";\"Bemærkning\"");

            //"Name,StartDate,EndDate,Description,Budget,SelfFinancing, MultiplePartners, Owner, AggregatedBudget,FunderId,ProgramId,PrimaryFocus,SecondaryFocus,Person,Responsible,RespNo,ProjectLink, Remark"

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=projekter.csv");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.Unicode;


            var projects = db.AProjects.Include(p => p.Program).Include(p => p.Funder).ToList();
                //db.AProjects.Include.ToList();

            foreach (var p in projects)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\"",
                    p.Name,
                    p.ProjectNumber,
                    p.Status,
                    p.StartDate.ToShortDateString(),
                    p.EndDate.ToShortDateString(),
                    p.Description,
                    p.Budget,
                    p.SelfFinancing,
                    p.MultiplePartners,
                    p.ExtProjectNumber,
                    p.Owner,
                    p.AggregatedBudget,
                    p.FunderId != null ? p.Funder.Name : p.FunderId.ToString(),
                    p.ProgramId != null ? p.Program.AggName : p.FunderId.ToString(),
                    p.PrimaryFocus,
                    p.SecondaryFocus,
                    p.Person,
                    p.Responsible,
                    p.RespNo,
                    p.ProjectLink,
                    p.Remark
                    ));
            }
            Response.Write(sw.ToString());
            Response.End();

            return View();
        }
    }
}