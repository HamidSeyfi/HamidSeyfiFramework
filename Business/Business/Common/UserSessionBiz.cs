using HSF.BaseSystemModel.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business.Common
{
    public class UserSessionBiz
    {
        public static UserSession GetUserSession()
        {
            return (UserSession)System.Web.HttpContext.Current.Session["UserSession"];
        }

    }
}
