using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Helper
{
    public static class ExtensionMethods
    {
        public static string MakePersian(this string  arabicText)
        {
            return arabicText.Replace('ی', 'ي').Replace('ک', 'ك');
        }
    }
}
