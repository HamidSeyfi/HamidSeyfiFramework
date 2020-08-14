namespace HSF.BaseSystemModel.Model.Table
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccess")]
    public partial class UserAccess
    {
        public int Id { get; set; }

        public int FK_UserId { get; set; }

        public int FK_AccessId { get; set; }

        public virtual Access Access { get; set; }

        public virtual User User { get; set; }
    }
}
