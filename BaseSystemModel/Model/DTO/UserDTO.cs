using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.BaseSystemModel.Model.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }


        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

       

        public string Email { get; set; }

        public Guid EmailVerificationID { get; set; }

        public bool IsAdmin { get; set; }

        public int FK_UserStatusId { get; set; }





        public string FullName { get; set; }
        public string RePassword { get; set; }

        public string URL { get; set; }




    }
}
