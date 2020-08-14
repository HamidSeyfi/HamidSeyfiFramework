using HSF.BaseSystemModel.Helper;
using HSF.BaseSystemModel.Model.Table;
using HSF.Business;
using HSF.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HSF.Controllers
{
    public class AccessController : BaseController
    {

        #region AccessPage

        [HttpGet]
        public ActionResult Index()
        {
            LogBiz.Log("Access/Index", string.Empty, LogType.Menu);
            return View();
        }

        [HttpGet]
        public JsonResult Find(int id)
        {
            try
            {
                LogBiz.Log("Access/Find", id.ToString(), LogType.InputUser);
                var model = AccessBiz.Instance.Find(id);
                LogBiz.Log("Access/Find", model, LogType.OutputUser);
                return Json(new { success = true, model = model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogBiz.Log("Access/Find", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Add([Bind(Include = "Name,NameFa,ParentId,Order,IsMenu,Url")]Access model)
        {
            try
            {
                LogBiz.Log("Access/Add", model, LogType.InputUser);
                AccessBiz.Instance.Add(model);
                LogBiz.Log("Access/Add", model.Id.ToString(), LogType.OutputUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogBiz.Log("Access/Add", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) });
            }


        }

        [HttpPost]
        public JsonResult Update([Bind(Include = "Id,Name,NameFa,ParentId,Order,IsMenu,Url")]Access model)
        {
            try
            {
                LogBiz.Log("Access/Update", model, LogType.InputUser);
                AccessBiz.Instance.Update(model);
                LogBiz.Log("Access/Update", string.Empty, LogType.OutputUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogBiz.Log("Access/Update", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LogBiz.Log("Access/Delete", id.ToString(), LogType.InputUser);
                AccessBiz.Instance.Delete(id);
                LogBiz.Log("Access/Delete", string.Empty, LogType.OutputUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogBiz.Log("Access/Delete", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) });
            }

        }


        [HttpGet]
        public JsonResult GenerateAccessTree()
        {
            try
            {
                var AccesstreeString = AccessBiz.Instance.GenerateAccessTree();
                return Json(new { success = true, tree = AccesstreeString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogBiz.Log("Access/GenerateAccessTree", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) }, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpGet]
        public JsonResult GetAccessNameFa(int id)
        {
            try
            {
                LogBiz.Log("Access/GetAccessNameFa", id.ToString(), LogType.InputUser);
                var result = AccessBiz.Instance.GetAccessNameFa(id);
                LogBiz.Log("Access/GetAccessNameFa", result, LogType.OutputUser);
                return Json(new { success = true, AccessNameFa = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogBiz.Log("Access/GetAccessNameFa", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        #region Sidebar Menu        
        //[HttpGet] //هنگامی که داشت از متد پست این تابع فراخونی میشد ، به خطا میخورد
        public ActionResult GenerateSidebarMenu()
        {                     
            string result = AccessBiz.Instance.GenerateSidebarMenu();
            return PartialView("_SidebarMenu", result);          
        }
        #endregion


    }
}