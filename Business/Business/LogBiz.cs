using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business
{
    public static class LogBiz
    {
        public static void Log(string logName, string logText, byte logType = 0, byte logPriority = 0)
        {
            try
            {
                var userSession = (HSF.Model.Common.UserSession)System.Web.HttpContext.Current.Session["UserSession"];

                using (var context = new HSFDbContext())
                {
                    context.Logs.Add(new Model.Table.Log
                    {
                        LogDate = DateTime.Now,
                        LogName = logName,
                        LogPriority = logPriority,
                        LogText = logText,
                        LogType = logType,
                        UserCode = userSession != null ? userSession.UserID : 0
                    }) ;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
