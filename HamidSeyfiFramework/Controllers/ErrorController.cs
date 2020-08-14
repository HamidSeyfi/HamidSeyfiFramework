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
        public ActionResult Index(string msg)
        {
            ViewBag.ErrorMsg = msg;
            return View();
        }

        // GET: Error
        public ActionResult General()
        {
            return View();
        }


        public ActionResult HttpError404()
        {
            return View();
        }


        public ActionResult HttpError500()
        {
            return View();
        }
    }
}