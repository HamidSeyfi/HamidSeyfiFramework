using HSF.Model.Common;
using HSF.Model.Table;
using HSF.Model.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business
{
    public class User2Biz
    {
        #region Variables

        public static User2Biz Instance = new User2Biz();
        private static List<UserWrapper> userWrappers = null;

        #endregion



        #region Public Methods

        public List<UserWrapper> FindAll()
        {
            using (var dbContext = new HSFDbContext())
            {
                var result = (from user in dbContext.Users
                              join userStatus in dbContext.UserStatus
                              on user.FK_UserStatus_ID equals userStatus.ID
                              select new UserWrapper()
                              {
                                  FullName = user.FullName,
                                  UserName = user.UserName,
                                  ID = user.ID,
                                  Email = user.Email,
                                  IsAdminDescrp = user.IsAdmin ? "بلی" : "خیر",
                                  UserStatusDescrp = userStatus.Descrp
                              }).ToList();

                return result;
            }
        }

        public List<UserWrapper> FindAllStatic()
        {
            if (userWrappers != null)
            {
                return userWrappers;
            }

            using (var dbContext = new HSFDbContext())
            {
                userWrappers = (from user in dbContext.Users
                                join userStatus in dbContext.UserStatus
                                on user.FK_UserStatus_ID equals userStatus.ID
                                select new UserWrapper()
                                {
                                    FullName = user.FullName,
                                    UserName = user.UserName,
                                    ID = user.ID,
                                    Email = user.Email,
                                    IsAdminDescrp = user.IsAdmin ? "بلی" : "خیر",
                                    UserStatusDescrp = userStatus.Descrp
                                }).ToList();

                return userWrappers;
            }
        }

        public User Find(int id)
        {
            using (var dbContext = new HSFDbContext())
            {
                var result = dbContext.Users.Where(e => e.ID == id).FirstOrDefault();
                if (result == null)
                {
                    throw new BusinessException("کاربر یافت نشد");
                }
                return result;
            }
        }

        public void Add(User model)
        {
            Validate(model);

            using (var dbContext = new HSFDbContext())
            {
                model.EmailVerificationID = Guid.Empty;
                //model.FK_UserStatus_ID = 1;
                dbContext.Users.Add(model);
                dbContext.SaveChanges();
            }
        }

        public void Edit(User model)
        {
            Validate(model);

            using (var dbContext = new HSFDbContext())
            {
                var dbModel = dbContext.Users.Where(e => e.ID == model.ID).FirstOrDefault();
                if (dbModel == null)
                {
                    throw new BusinessException("کاربر یافت نشد");
                }
                dbModel.Email = model.Email;
                dbModel.FK_UserStatus_ID = model.FK_UserStatus_ID;
                dbModel.FullName = model.FullName;
                dbModel.UserName = model.UserName;
                dbModel.IsAdmin = model.IsAdmin;
                dbModel.Password = model.Password;
                dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new HSFDbContext())
            {
                var dbModel = dbContext.Users.Where(e => e.ID == id).FirstOrDefault();
                if (dbModel == null)
                {
                    throw new BusinessException("کاربر یافت نشد");
                }
                dbContext.Users.Remove(dbModel);
                dbContext.SaveChanges();
            }
        }

        #endregion


        #region Private Methods

        private void Validate(User model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
              string.IsNullOrWhiteSpace(model.Password) ||
              string.IsNullOrWhiteSpace(model.FullName) ||
              string.IsNullOrWhiteSpace(model.UserName)
              )
            {
                throw new BusinessException("برخی از فیلد ها خالی هستند");
            }
        }

        #endregion
    }
}
