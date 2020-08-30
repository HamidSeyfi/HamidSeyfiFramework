using HSF.BaseSystemModel.Helper;
using HSF.BaseSystemModel.Model.Common;
using HSF.BaseSystemModel.Model.DTO;
using HSF.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HSF.Controllers
{
    public class UserController : BaseController
    {

        #region Login       

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UserSession"] != null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDTO model)
        {
            try
            {
                var user = UserBiz.Instance.Login(model);
                Session["UserSession"] = new UserSession
                {
                    UserID = user.Id,
                    Email = user.Email,
                    FullName = user.FirstName + " " + user.LastName,
                };

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = Message.GetExceptionMessage(ex);
                return View();
            }
        }
        #endregion



        #region Logout        
        [HttpGet]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
        #endregion


        #region ForgetPassword       
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(UserDTO model)
        {
            try
            {
                var user = UserBiz.Instance.ForgetPassword(model.Email);

                //Send Forget Password Email//
                string emailBodyHtml = RenderViewToString(ControllerContext,
                   "~/views/User/_ForgetPasswordEmail.cshtml",
                   new UserDTO
                   {
                       FullName = user.FirstName + " " + user.LastName,
                       Password = model.Password
                   },
                   false);

                var emailHeader = "فراموشی رمز عبور";
                UtilityClass.SendEmail(model.Email, emailHeader, emailBodyHtml);
                //////////////////////
                ViewBag.SuccessMsg = "رمز عبور جدید به ایمیل شما ارسال گردید";

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = Message.GetExceptionMessage(ex);
            }
            return View();
        }
        #endregion


        #region Register        
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["UserSession"] != null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDTO model)
        {
            try
            {
                var user = UserBiz.Instance.Register(model);

                //Send Activition Email//
                string emailBodyHtml = RenderViewToString(ControllerContext,
                   "~/views/User/_ConfirmationEmail.cshtml",
                   new UserDTO
                   {
                       FullName = user.FirstName + " " + user.LastName,
                       URL =  Request.Url.Authority + Url.Action("ActivateUser", "User") + " / " + user.EmailVerificationID.ToString().Replace(" - ", "")
                   },
                   false);

                var emailHeader = "فراموشی رمز عبور";
                UtilityClass.SendEmail(model.Email, emailHeader, emailBodyHtml);
                //////////////////////

                ViewBag.SuccessMsg = "ثبت نام با موفقیت انجام شد، ایمیل فعال سازی برای شما ارسال گردد";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = Message.GetExceptionMessage(ex);
            }
            return View();

        }
        #endregion


        #region ActivateUser

        #endregion
        [HttpGet]
        public ActionResult ActivateUser(string id)
        {
            try
            {
                UserBiz.Instance.ActivateUser(id);
                ViewBag.SuccessMsg = "فعال سازی با موفقیت انجام گردید";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = Message.GetExceptionMessage(ex);
            }
            return View("UserActivation");
        }
        #region Private Methods

        [HttpGet]
        private string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        #endregion

    }
}