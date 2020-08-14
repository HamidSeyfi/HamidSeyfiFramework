using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Helper
{
    public class Message
    {

        public static string GetExceptionMessage(Exception ex)
        {
            if (ex.GetType() == typeof(BusinessException))
            {
                return ex.Message;
            }

            if (ex.Message.IndexOf("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return ExObjectRefrenceNull;
            }
            else if (ex.Message.IndexOf("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase) != -1
                || ex.Message.IndexOf("Index was out of range", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return ExIndexOutOfRange;
            }
            if (ex.Message.IndexOf("Sequence contains no", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return ExNoDataFound;
            }
            else
            {
                return "خطا رخ داده است";
            }
        }

        private static Culture GetCulture()
        {
            var culture = Culture.Fa; //get culture from somewhere
            //_culture = ((Model.Common.UserSession)HttpContext.Current.Session["UserSession"]).Culture;
            return culture;
        }


        #region Private Messages

        private static readonly string ExObjectRefrenceNull =
            GetCulture() == Culture.Fa ? MessagesFa.ExObjectRefrenceNull :
            GetCulture() == Culture.En ? MessagesEn.ExObjectRefrenceNull :
             "";

        private static readonly string ExIndexOutOfRange =
            GetCulture() == Culture.Fa ? MessagesFa.ExIndexOutOfRange :
            GetCulture() == Culture.En ? MessagesEn.ExIndexOutOfRange :
             "";

        private static readonly string ExNoDataFound =
            GetCulture() == Culture.Fa ? MessagesFa.ExNoDataFound :
            GetCulture() == Culture.En ? MessagesEn.ExNoDataFound :
             "";

        #endregion


        #region Public Messages

        #endregion








    }

    static class MessagesFa
    {
        public static readonly string ExObjectRefrenceNull = "خطای ارجاع خالی";
        public static readonly string ExIndexOutOfRange = "خطا ایندکس اشتباه";
        public static readonly string ExNoDataFound = "خطا پیدا نکردن اطلاعات";
    }


    static class MessagesEn
    {
        public static readonly string ExObjectRefrenceNull = "Empty Refrence Error";
        public static readonly string ExIndexOutOfRange = "Wrong Index Error";
        public static readonly string ExNoDataFound = "No Data Found Error";
    }
}
