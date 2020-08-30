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
    public class AccessBiz
    {
        #region Variables

        public static AccessBiz Instance = new AccessBiz();

        #endregion


        #region Public Methods



        public AccessDTO Find(int id)
        {
            using (var context = new SqlServerDataContext())
            {
                var model = (from access in context.Accesses
                             join accessParent in context.Accesses
                             on access.ParentId equals accessParent.Id
                             where access.Id == id
                             select new AccessDTO
                             {
                                 Name = access.Name,
                                 NameFa = access.NameFa,
                                 IsMenu = access.IsMenu,
                                 Order = access.Order,
                                 Url = access.Url,
                                 ParentAccessName = accessParent.NameFa
                             }
                             ).FirstOrDefault();
                return model;
            }
        }


        public void Add(Access input)
        {
            Validate(input);

            using (var context = new SqlServerDataContext())
            {
                context.Accesses.Add(input);
                context.SaveChanges();
            }
        }


        public void Update(Access input)
        {
            Validate(input);
            using (var context = new SqlServerDataContext())
            {
                var access = context.Accesses.Find(input.Id);
                access.Name = input.Name;
                access.NameFa = input.NameFa;
                access.IsMenu = input.IsMenu;
                access.Order = input.Order;
                access.Url = input.Url;
                context.SaveChanges();
            }
        }


        public void Delete(int id)
        {
            using (var context = new SqlServerDataContext())
            {
                //Remove All childs of input access and input access itself
              
                //var access = new Access { Id = id };
                //var accessChilds = new Access { ParentId = id };
                //context.Access.Remove(access);
                //context.Access.Remove(accessChilds);

                context.Accesses.RemoveRange(context.Accesses.Where(e => e.ParentId == id || e.Id == id).ToList());
                context.SaveChanges();
            }

        }


        /// <summary>
        /// ساختن منو کنار صفحه
        /// </summary>
        /// <returns></returns>
        public string GenerateSidebarMenu()
        {
            string result = "";
            using (var context = new SqlServerDataContext())
            {
                var accessList = context.Accesses.Where(e => e.IsMenu == true).OrderBy(e => e.ParentId).ThenBy(e => e.Order)
                    .Select(e => new AccessDTO
                    {
                        NameFa = e.NameFa,
                        Id = e.Id,
                        ParentID = e.ParentId,
                        Url = e.Url
                    }).ToList();
                ;
                result = GenerateSidebarMenuRecursive(accessList, 1);
            }
            return result.ToString();
        }




        /// <summary>
        /// ساختن درخت دسترسی در فرم دسترسی 
        /// </summary>
        /// <returns></returns>
        public string GenerateAccessTree()
        {
            string result = "";
            using (var context = new SqlServerDataContext())
            {
                var accessList = context.Accesses.OrderBy(e => e.ParentId).ThenBy(e => e.Order).Select(e => new AccessDTO
                {
                    NameFa = e.NameFa,
                    Id = e.Id,
                    ParentID = e.ParentId,
                }).ToList();
                result = GenerateAccessTreeRecursive(accessList, 0);
            }
            return result.ToString();
        }



        /// <summary>
        /// برگرداندن نام فارسی دسترسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAccessNameFa(int id)
        {
            using (var context = new SqlServerDataContext())
            {
                var result = context.Accesses.Where(e => e.Id == id).Select(e => e.NameFa).FirstOrDefault();
                return result;
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// ساختن منو کنار صفحه - به صورت بازگشتی
        /// </summary>
        /// <param name="accessList"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private string GenerateSidebarMenuRecursive(List<AccessDTO> accessList, int parentID)
        {
            var result = new StringBuilder();
            foreach (var item in accessList.Where(e => e.ParentID == parentID))
            {
                if (accessList.Where(e => e.ParentID == item.Id).Count() > 0)
                {
                    result.Append("<li class='treeview'>");
                    result.Append("<a href='" + item.Url + "'>");
                    result.Append("<i class='fa fa-share'></i> <span>" + item.NameFa + "</span>");
                    result.Append("<span class='pull-left-container'>");
                    result.Append("<i class='fa fa-angle-right pull-left'></i>");
                    result.Append("</span>");
                    result.Append("</a>");
                    result.Append("<ul class='treeview-menu'>");
                    result.Append(GenerateSidebarMenuRecursive(accessList, item.Id));
                    result.Append("</ul>");
                }
                else
                {
                    result.Append("<li><a href='" + item.Url + "'><i class='fa fa-circle-o'></i>");
                    result.Append(item.NameFa);
                    result.Append("</a></li>");
                }
            }
            return result.ToString();
        }



        /// <summary>
        /// ساختن درخت دسترسی در فرم دسترسی - به صورت بازگشتی
        /// </summary>
        /// <param name="accessList"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public string GenerateAccessTreeRecursive(List<AccessDTO> accessList, int parentID)
        {
            var result = new StringBuilder();
            if (parentID == 0)
            {
                result.Append("<ul id='treeUL'>");
            }
            else
            {
                result.Append("<ul class='nested'>");
            }

            foreach (var item in accessList.Where(e => e.ParentID == parentID))
            {

                if (accessList.Where(e => e.ParentID == item.Id).Count() > 0)
                {
                    result.Append("<li id='" + item.Id + "'><span class='caretLi'>" + item.NameFa + "</span>");
                    result.Append(GenerateAccessTreeRecursive(accessList, item.Id));
                }
                else
                {
                    result.Append("<li id='" + item.Id + "'> <span class='singleLi'> " + item.NameFa + "</span>");
                }
                result.Append("</li>");
            }
            result.Append("</ul>");

            return result.ToString();
        }



        /// <summary>
        /// اعتبار سنجی
        /// </summary>
        /// <param name="model"></param>
        private void Validate(Access model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new Exception("نام دسترسی خالی است");
            }


            if (string.IsNullOrWhiteSpace(model.NameFa))
            {
                throw new Exception("نام فارسی دسترسی خالی است");
            }

            //byte temp = 0;
            //if (string.IsNullOrWhiteSpace(model.Order.ToString()) || byte.TryParse(model.Order.ToString(), out temp) == false)
            //{
            //    throw new Exception("ترتیب دسترسی خالی است و یا معتبر نیست");
            //}

            if (model.Order < 0)
            {
                throw new Exception("ترتیب دسترسی معتبر نیست");
            }


            if (string.IsNullOrWhiteSpace(model.Url))
            {
                throw new Exception("آدرس دسترسی خالی است");
            }


        }

        #endregion
    }
}
