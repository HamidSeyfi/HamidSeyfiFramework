using Hamid.Business;
using Hamid.Helper;
using Hamid.Model.Common;
using Hamid.Model.Table;
using Hamid.Model.Wrapper;
using Hamid.Models.Filters;
using PagedList;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HSF.Controllers
{
    public class User2Controller : BaseController
    {
        [HttpGet]
        public ActionResult Index(string sort, string currentSort, string currentSortType, string searchFilter, string currentSearchFilter, int page = 1)
        {

           


            var model = User2Biz.Instance.FindAll();



         

            return View(model);
        }
      


        [HttpGet]
        [AuthenticationFilter]
        public ActionResult Add()
        {
            var model = new User();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User model)
        {
            try
            {
                User2Biz.Instance.Add(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User2/Add(Post)", ex);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = User2Biz.Instance.Find(id);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User2/Edit(Get)", ex);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            try
            {
                User2Biz.Instance.Edit(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User2/Edit(Post)", ex);
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = User2Biz.Instance.Find(id);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User2/Delete(Get)", ex);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User model)
        {
            try
            {
                User2Biz.Instance.Delete(model.ID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User2/Delete(Post)", ex);
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            try
            {
                var model = User2Biz.Instance.Find(id);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User2/Detail(Get)", ex);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Print()
        {
            return View();
        }


        #region Stimulsotf

        public ActionResult GetReport()
        {
            Stimulsoft.Base.StiLicense.LoadFromFile(Server.MapPath("~/Content/Reports/license.key"));
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Reports/User.mrt"));
            report.RegBusinessObject("UserReport", User2Biz.Instance.FindAll());
            //report.Dictionary.Variables["variableName"].ValueObject = variableValue;
            return StiMvcViewer.GetReportResult(report);
        }

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }

        #endregion
    }
}