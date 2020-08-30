namespace HSF.BaseSystemModel.Model.Table
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        public int Id { get; set; }

        public int FK_UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        public string LogName { get; set; }

        public string LogText { get; set; }

        public byte LogType { get; set; }
        public string IP { get; set; }

    }
}
