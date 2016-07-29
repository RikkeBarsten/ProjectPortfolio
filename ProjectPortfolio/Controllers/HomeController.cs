using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPortfolio.DAL;
using ProjectPortfolio.Models.ViewModels;
using ProjectPortfolio.Models;


namespace ProjectPortfolio.Controllers
{
    public class HomeController : Controller
    {
       

        public ActionResult Index()
        {
                    

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontaktoplysninger";

            return View();
        }

        
    }
}