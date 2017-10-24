using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccess.Entities;
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
    }
}
