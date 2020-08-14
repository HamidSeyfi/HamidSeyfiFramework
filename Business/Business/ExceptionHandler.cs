using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business
{
    public class ExceptionHandler
    {
        public static string LogAndGetExceptionMessage(string exSource, Exception ex, string extraLogMsg = "")
        {
            var result = "";

            if (ex.GetType() == typeof(Model.Common.BusinessException))
            {
                result = ex.Message;
            }
            else
            {
                LogBiz.Log(exSource, ex.Message + (!string.IsNullOrWhiteSpace(extraLogMsg) ? "\n" + extraLogMsg : ""));
                result = Helper.Messages.GetMessage(ex.Message);
            }

            return result;
        }
    }
}
