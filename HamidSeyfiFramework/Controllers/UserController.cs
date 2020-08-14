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
        #region Login/Logout/Forget Password

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
        public ActionResult Login(UserWrapper model)
        {
            try
            {
                var user = UserBiz.Instance.Login(model);
                Session["UserSession"] = new Hamid.Model.Common.UserSession
                {
                    UserID = user.ID,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Culture = Common.Culture.Fa
                };


                //Messages.GetMessage("sad");

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User/Login(Post)", ex);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPasswordPost()
        {
            try
            {
                var model = UserBiz.Instance.ForgetPassword(Request.Form["Email"].ToString());

                //Send Forget Password Email//
                string emailBodyHtml = RenderViewToString(ControllerContext,
                   "~/views/User/_ForgetPasswordEmail.cshtml",
                   new UserWrapper
                   {
                       FullName = model.FullName,
                       NewPassword = model.NewPassword
                   },
                   false);

                Hamid.Helper.Utility.UtilityClass.SendEmail(model.Email, "فراموشی رمز عبور", emailBodyHtml);
                //////////////////////
                ViewBag.SuccessMsg = "پسورد جدید ارسال گردید";

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User/ForgetPasswordPost(Post)", ex);
            }
            return View("ForgetPassword");
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
        public ActionResult Register(UserWrapper model)
        {
            try
            {
                var user = UserBiz.Instance.Register(model);

                //Send Activition Email//
                string emailBodyHtml = RenderViewToString(ControllerContext,
                   "~/views/User/_ConfirmationEmail.cshtml",
                   new UserWrapper
                   {
                       FullName = model.FullName,
                       URL = "http://" + Request.Url.Authority + Url.Action("Activate", "User") + "/" + user.EmailVerificationID.ToString().Replace("-", "")
                   },
                   false);

                Hamid.Helper.Utility.UtilityClass.SendEmail(model.Email, "فعال سازی حساب کاربری", emailBodyHtml);
                //////////////////////

                return View("RegisterSuccessfull");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User/Register(Post)", ex);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Activate(string id)
        {
            try
            {
                UserBiz.Instance.ActivateUser(id);
                ViewBag.SuccessMsg = "فعال سازی با موفقت انجام گردید";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ExceptionHandler.LogAndGetExceptionMessage("User/Activate(Get)", ex);
            }
            return View("UserActivation");
        }

        #endregion


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