using HSF.BaseSystemModel.Model.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSF.DataAccess
{
    public class SqlServerDataContext : DbContext
    {
        public SqlServerDataContext() : base(HSF.BaseSystemModel.Helper.ConnectionString.SqlServerConnectionString)
        {

        }

        public  DbSet<Access> Accesses { get; set; }
        public  DbSet<Log> Logs { get; set; }
        public  DbSet<User> Users { get; set; }
        public  DbSet<UserAccess> UserAccesses { get; set; }
        public  DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}

