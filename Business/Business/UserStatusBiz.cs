using HSF.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business
{
    public class UserStatusBiz
    {
        #region Variables

        public static UserStatusBiz Instance = new UserStatusBiz();

        #endregion


        #region Public Methods

        public List<UserStatus> FindAll()
        {
            using (var dbContext = new HSFDbContext())
            {
                var result = dbContext.UserStatus.ToList();
                return result;
            }
        }

        #endregion
    }
}
