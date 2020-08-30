namespace HSF.BaseSystemModel.Model.Table
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        

        public int Id { get; set; }

      
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

      

        public Guid EmailVerificationID { get; set; }

        public bool IsAdmin { get; set; }

        public int FK_UserStatusId { get; set; }

    }
}
