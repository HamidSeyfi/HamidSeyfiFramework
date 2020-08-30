using HSF.BaseSystemModel.Helper;
using HSF.BaseSystemModel.Model.DTO;
using HSF.BaseSystemModel.Model.Table;
using HSF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business
{
    public class UserBiz
    {
        #region Variables

        public static UserBiz Instance = new UserBiz();

        #endregion


        #region Public Methods   

        public User Login(UserDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new BusinessException("برخی از فیلد ها خالی هستند");
            }

            using (var dataContext = new SqlServerDataContext())
            {
                var result = dataContext.Users.Where(e => e.Email == model.Email && e.Password == model.Password).FirstOrDefault();
                if (result == null)
                {
                    throw new BusinessException("اطلاعات کاربری اشتباه است");
                }
                return result;
            }
        }

        public User ForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new BusinessException("ایمیل خالی است");
            }

            using (var dataContext = new SqlServerDataContext())
            {
                var user = dataContext.Users.Where(e => e.Email == email).FirstOrDefault();
                if (user == null)
                {
                    throw new BusinessException("کاربری با این مشخصات یافت نشد");
                }

                var random = new Random((int)System.DateTime.Now.ToBinary());
                var newPassword = random.Next(1000, int.MaxValue).ToString();
                user.Password = newPassword;
                dataContext.SaveChanges();

                //var emailHeader = "فراموشی رمز عبور";
                //var emailBody = $@"{user.FirstName} عزیز\n رمز عبور جدید شما : {newPassword}";
                //UtilityClass.SendEmail(email, emailHeader, emailBody);

                return user;
            }
        }

        public User Register(UserDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.RePassword) 
                )
            {
                throw new BusinessException("برخی از فیلد ها خالی هستند");
            }


            if (model.Password != model.RePassword)
            {
                throw new BusinessException("رمز عبور و تکرار آن با هم برابر نیستند");
            }

            using (var dataContext = new SqlServerDataContext())
            {
                if (dataContext.Users.Where(e => e.Email == model.Email).Count() > 0)
                {
                    throw new BusinessException("ایمیل تکراری است");
                }

                var user = new User()
                {
                    Email = model.Email,
                    EmailVerificationID = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsAdmin = false,
                    Password = model.Password,
                    FK_UserStatusId = (int)UserStatusEnum.InActive,
                };


                dataContext.Users.Add(user);
                dataContext.SaveChanges();

                return user;
            }
        }

        public void ActivateUser(string verificationID)
        {
            using (var dataContext = new SqlServerDataContext())
            {


                var user = dataContext.Users.Where(e => e.EmailVerificationID.ToString().Replace("-", "") == verificationID).FirstOrDefault();
                if (user == null)
                {
                    throw new BusinessException("کد فعال سازی معتبر نمیباشد");
                }
                else
                {
                    user.FK_UserStatusId = (int)UserStatusEnum.Active   ;
                    dataContext.SaveChanges();
                }
            }
        }

        #endregion


      
    }
}
