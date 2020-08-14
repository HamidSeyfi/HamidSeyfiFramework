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
    public class UserBiz
    {
        #region Variables

        public static UserBiz Instance = new UserBiz();

        #endregion


        #region Public Methods   

        public User Login(UserWrapper model)
        {
            if (string.IsNullOrWhiteSpace(model.UserNameOrEmail) || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new BusinessException("برخی از فیلد ها خالی هستند");
            }

            using (var context = new HSFDbContext())
            {
                var result = context.Users.Where(e => (e.UserName == model.UserNameOrEmail || e.Email == model.UserNameOrEmail) && e.Password == model.Password).FirstOrDefault();
                if (result == null)
                {
                    throw new BusinessException("اطلاعات کاربری اشتباه است");
                }
                return result;
            }
        }

        public UserWrapper ForgetPassword(string userNameOrEmail)
        {

            using (var context = new HSFDbContext())
            {
                var user = context.Users.Where(e => e.UserName == userNameOrEmail || e.Email == userNameOrEmail).FirstOrDefault();
                if (user == null)
                {
                    throw new BusinessException("کاربری با این مشخصات یافت نشد");
                }

                var random = new Random((int)System.DateTime.Now.ToBinary());
                var newPassword = random.Next(1000, int.MaxValue).ToString();
                user.Password = newPassword;
                context.SaveChanges();
                //return new UserForgetPasswordEmailWrapper
                return new UserWrapper
                {
                    FullName = user.FullName,
                    NewPassword = newPassword,
                    Email = user.Email
                };
            }
        }

        public User Register(UserWrapper model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.FullName) ||
                string.IsNullOrWhiteSpace(model.RePassword) ||
                string.IsNullOrWhiteSpace(model.UserName)
                )
            {
                throw new BusinessException("برخی از فیلد ها خالی هستند");
            }


            if (model.Password != model.RePassword)
            {
                throw new BusinessException("کلمه عبور و تکرار آن با هم برابر نیستند");
            }

            var user = new User()
            {
                Email = model.Email,
                EmailVerificationID = Guid.NewGuid(),
                FullName = model.FullName,
                IsAdmin = false,
                Password = model.Password,
                FK_UserStatus_ID = 0,
                UserName = model.UserName
            };

            using (var context = new HSFDbContext())
            {
                if (context.Users.Where(e => e.UserName == model.UserName).Count() > 0)
                {
                    throw new BusinessException("نام کاربری تکراری است");
                }

                if (context.Users.Where(e => e.Email == model.Email).Count() > 0)
                {
                    throw new BusinessException("ایمیل تکراری است");
                }

                context.Users.Add(user);

                context.SaveChanges();

                return user;
            }
        }
        
        public void ActivateUser(string verificationID)
        {
            using (var myContext = new HSFDbContext())
            {


                var user = myContext.Users.Where(e => e.EmailVerificationID.ToString().Replace("-", "") == verificationID).FirstOrDefault();
                if (user == null)
                {
                    throw new BusinessException("کد فعال سازی معتبر نمیباشد");
                }

                else
                {
                    user.FK_UserStatus_ID = 1;
                    myContext.SaveChanges();
                }
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
