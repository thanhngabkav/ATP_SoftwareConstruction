namespace DataAccess.Migrations
{
    using DataAccess.Entities;
    using DataAccess.Utilities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.DBContext.VideoRentalDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccess.DBContext.VideoRentalDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Users.AddOrUpdate(
                 u => u.UserID,
                 new User
                 {
                     UserID = 1,
                     UserName = "manager",
                    //password is manager and have encoded by sha256 algorithm
                    Password = "OwJt7Ix43MiTfXo4sZFecCo4EpPU0JeYixCB7n29PVs=",
                     Address = "Hồ Chí Minh",
                     PhoneNumber = "096281084",
                     Email = "manager01@gmail.com",
                     Role = UserRole.Manager
                 },

                 new User
                 {
                     UserID = 2,
                     UserName = "clerk",
                    //password is clerk and have encoded by sha256 algorithm
                    Password = "9XWtGS9d30jR4GFlOT7WeCBzhF94dwv7VufImcINPF0=",
                     Address = "Hồ Chí Minh",
                     PhoneNumber = "0985468764",
                     Email = "clerk01@gmail.com",
                     Role = UserRole.Clerk
                 }
             );
        }
    }
}
