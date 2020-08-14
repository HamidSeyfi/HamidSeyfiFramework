using HSF.Model.Wrapper.Test.GridTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.Business
{
    public class GridTestBiz
    {


        public JqGridOutput GetData(JqGridInput input)
        {
            //Searching is not implemented


            using (var dbContext = new HSFDbContext())
            {
                var items = dbContext.Users.AsQueryable();
                var totalItems = items.Count();


                


                //------------- Sort ---------------
                if (!string.IsNullOrEmpty(input.SortIndex))
                {
                    switch (input.SortIndex)
                    {
                        case "ID":
                            items = input.SortOrder == "asc" ? items.OrderBy(e => e.ID) : items.OrderByDescending(e => e.ID);
                            break;
                        case "FullName":
                            items = input.SortOrder == "asc" ? items.OrderBy(e => e.FullName) : items.OrderByDescending(e => e.FullName);
                            break;
                        case "UserName":
                            items = input.SortOrder == "asc" ? items.OrderBy(e => e.UserName) : items.OrderByDescending(e => e.UserName);
                            break;
                    }
                }
                else
                {
                    items = items.OrderBy(e => e.ID);
                }
                //--------------------------------------


                var output = new JqGridOutput();
                output.Page = input.PageNumber;
                output.Total = (int)Math.Ceiling((double)totalItems / input.PageSize);
                output.Records = totalItems;
                output.Rows = items.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).ToList();


                return output;
            }













        }
    }
}
