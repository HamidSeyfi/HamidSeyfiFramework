using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Helper
{
    public class UtilityClass
    {
        public static void SendEmail(string emailAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("Manuchehr.Seyfi@gmail.com");
            message.To.Add(new MailAddress(emailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("Manuchehr.Seyfi@gmail.com", "912045222278");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }


        public static string ConvertDateTimeMiladiToShamsi(DateTime date, bool includeTime = false)
        {
            var persianCalendar = new System.Globalization.PersianCalendar();
            string year = persianCalendar.GetYear(date).ToString();
            string month = persianCalendar.GetMonth(date).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(date).ToString().PadLeft(2, '0');
            string persianDateString = string.Format("{0}/{1}/{2}", year, month, day);
            if (includeTime)
            {
                persianDateString += " " +
                    date.Hour.ToString().PadLeft(2, '0') + ":" +
                    date.Minute.ToString().PadLeft(2, '0') + ":" +
                    date.Second.ToString().PadLeft(2, '0');
            }
            return persianDateString;
        }


        public static DateTime ConvertDateTimeShamsiToMiladi(string dateStr, string time = "")
        {
            int year = Convert.ToInt32(dateStr.Substring(0, 4));
            int month = Convert.ToInt32(dateStr.Substring(5, 2));
            int day = Convert.ToInt32(dateStr.Substring(8, 2));
            int hour = 0;
            int minute = 0;
            int second = 0;

            if (!string.IsNullOrWhiteSpace(time))
            {
                var tempTimeSplit = time.Split(':');
                if (tempTimeSplit.Length > 1)
                {
                    hour = int.Parse(tempTimeSplit[0]);
                }
                if (tempTimeSplit.Length > 2)
                {
                    minute = int.Parse(tempTimeSplit[1]);
                }
                if (tempTimeSplit.Length > 3)
                {
                    second = int.Parse(tempTimeSplit[2]);
                }
            }

            var miladiDate = new DateTime(year, month, day, hour, minute, second, new System.Globalization.PersianCalendar());
            return miladiDate;
        }



    }
}
