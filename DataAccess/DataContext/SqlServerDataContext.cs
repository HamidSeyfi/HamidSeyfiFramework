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


        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAccess> UserAccess { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Access>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Access>()
                .HasMany(e => e.UserAccess)
                .WithRequired(e => e.Access)
                .HasForeignKey(e => e.FK_AccessId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.LogName)
                .IsUnicode(false);

          

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

         

            modelBuilder.Entity<User>()
                .HasMany(e => e.Log)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.FK_UserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserAccess)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.FK_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserStatus>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserStatus)
                .HasForeignKey(e => e.FK_UserStatusId)
                .WillCascadeOnDelete(false);
        }
    }
}

