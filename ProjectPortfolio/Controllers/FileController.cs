using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPortfolio.DAL;

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
    }
}