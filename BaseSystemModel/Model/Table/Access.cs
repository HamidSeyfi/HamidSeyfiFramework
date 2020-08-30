namespace HSF.BaseSystemModel.Model.Table
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Access
    {
        

        public int Id { get; set; }

        public string Name { get; set; }

        public string NameFa { get; set; }

        public int ParentId { get; set; }

        public byte Order { get; set; }

        public bool IsMenu { get; set; }

        public string Url { get; set; }       

       
    }
}
