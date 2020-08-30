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
            //LogBiz.Log("Access/Index(GET)", string.Empty);
            return View();
        }

        [HttpGet]
        public JsonResult Find(int id)
        {
            try
            {
                //LogBiz.Log("Access/Find(GET)", id.ToString(), LogType.InputUser);
                var model = AccessBiz.Instance.Find(id);
                //LogBiz.Log("Access/Find(GET)", model, LogType.OutputUser);
                return Json(new { success = true, model = model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //LogBiz.Log("Access/Find(GET)", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Add([Bind(Include = "Name,NameFa,ParentId,Order,IsMenu,Url")]Access model)
        {
            try
            {
                //LogBiz.Log("Access/Add(POST)", model, LogType.InputUser);
                AccessBiz.Instance.Add(model);
                //LogBiz.Log("Access/Add(POST)", model.Id.ToString(), LogType.OutputUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //LogBiz.Log("Access/Add(POST)", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) });
            }


        }

        [HttpPost]
        public JsonResult Update([Bind(Include = "Id,Name,NameFa,ParentId,Order,IsMenu,Url")]Access model)
        {
            try
            {
                //LogBiz.Log("Access/Update(POST)", model, LogType.InputUser);
                AccessBiz.Instance.Update(model);
                //LogBiz.Log("Access/Update(POST)", string.Empty, LogType.OutputUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //LogBiz.Log("Access/Update(POST)", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                //LogBiz.Log("Access/Delete(POST)", id.ToString(), LogType.InputUser);
                AccessBiz.Instance.Delete(id);
                //LogBiz.Log("Access/Delete(POST)", string.Empty, LogType.OutputUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //LogBiz.Log("Access/Delete(POST)", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) });
            }

        }


        [HttpGet]
        public JsonResult GenerateAccessTree()
        {
            try
            {
                //LogBiz.Log("Access/GenerateAccessTree(GET)", string.Empty);
                var AccesstreeString = AccessBiz.Instance.GenerateAccessTree();
                //LogBiz.Log("Access/GenerateAccessTree(GET)", AccesstreeString,LogType.OutputUser);
                return Json(new { success = true, tree = AccesstreeString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //LogBiz.Log("Access/GenerateAccessTree(GET)", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) }, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpGet]
        public JsonResult GetAccessNameFa(int id)
        {
            try
            {
                //LogBiz.Log("Access/GetAccessNameFa(GET)", id.ToString(), LogType.InputUser);
                var result = AccessBiz.Instance.GetAccessNameFa(id);
                //LogBiz.Log("Access/GetAccessNameFa(GET)", result, LogType.OutputUser);
                return Json(new { success = true, AccessNameFa = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //LogBiz.Log("Access/GetAccessNameFa(GET)", ex, LogType.OutputUser);
                return Json(new { success = false, msg = Message.GetExceptionMessage(ex) }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        #region Sidebar Menu        
        //[HttpGet] //هنگامی که داشت از متد پست این تابع فراخونی میشد ، به خطا میخورد
        public ActionResult GenerateSidebarMenu()
        {
            //LogBiz.Log("Access/GenerateSidebarMenu(GET/POST)", string.Empty);
            string result = AccessBiz.Instance.GenerateSidebarMenu();
            //LogBiz.Log("Access/GenerateSidebarMenu(GET/POST)", result , LogType.OutputUser);
            return PartialView("_SidebarMenu", result);          
        }
        #endregion


    }
}