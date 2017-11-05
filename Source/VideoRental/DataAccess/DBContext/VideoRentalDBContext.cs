using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccess.Entities;
using System.Data.Entity.Validation;

namespace DataAccess.DBContext
{
    public class VideoRentalDBContext : DbContext
    {
        public DbSet<User> Users { set; get; }

        public DbSet<Customer> Customers { set; get; }

        public DbSet<DiskTitle> DiskTitles { set; get; }

        public DbSet<TransactionHistory> TransactionHistories { set; get; }

        public DbSet<TransactionHistoryDetail> TransactionHistoryDetails { set; get; }

        public DbSet<Disk> Disks { set; get; }

        public DbSet<Reservation> Reservations { set; get; }

        public DbSet<RentalRate> RentalRates { set; get; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}
