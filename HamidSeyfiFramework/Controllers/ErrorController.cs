using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSF.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

     
        
    }
}