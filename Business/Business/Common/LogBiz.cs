using HSF.BaseSystemModel.Helper;
using HSF.BaseSystemModel.Model.Table;
using HSF.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace HSF.Business.Common
{
    public class LogBiz
    {
        public static void Log(string logName, object log, LogType logType=LogType.Empty)
        {

            var logText = string.Empty;


            if (log.GetType() == typeof(BusinessException))
            {
                logText = ((BusinessException)log).Message;
            }
            else if (log.GetType() == typeof(Exception))
            {
                var ex = log as Exception;
                logText = JsonConvert.SerializeObject(new { ex.Message, ex.InnerException, ex.StackTrace });
                logType = LogType.Exception;
            }
            else
            {
                logText = log.GetType() == typeof(string) ? log.ToString() : JsonConvert.SerializeObject(log);
            }

            LogIntoSqlServer(logName, logText, logType);
        }


        public static void LogFilter(RouteData routeData, LogType logType)
        {
            var areaName = (string)routeData.DataTokens["area"];
            var controllerName = (string)routeData.Values["controller"];
            var actionName = (string)routeData.Values["action"];
            var logName = $"{(string.IsNullOrWhiteSpace(areaName) ? "" : (areaName + "/"))}{controllerName}/{actionName}";
            LogIntoSqlServer(logName, string.Empty, logType);
        }

        private static void LogIntoSqlServer(string logName, string logText, LogType logType)
        {
            int userId = 1;//find it from somewhere else
            //var userSession = (Hamid.Model.Common.UserSession)System.Web.HttpContext.Current.Session["UserSession"];

            using (var context = new SqlServerDataContext())
            {
                context.Logs.Add(new Log
                {
                    Date = DateTime.Now,
                    LogName = logName,
                    LogText = logText,
                    LogType = (byte)logType,
                    FK_UserId = userId
                });
                context.SaveChanges();
            }
        }
    }
}
