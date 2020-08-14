using HSF.BaseSystemModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Model.Common
{
    public class UserSession
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public Culture Culture { get; set; }
    }
}
