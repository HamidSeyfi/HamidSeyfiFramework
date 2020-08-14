using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Model.DTO
{
    public class AccessDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameFa { get; set; }

        public int ParentID { get; set; }
        public int Order { get; set; }

        public bool IsMenu { get; set; }

        public string Url { get; set; }

        public string ParentAccessName { get; set; }




    }
}
