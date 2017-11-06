﻿namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataAccess.Entities;
    using DataAccess.Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.DBContext.VideoRentalDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccess.DBContext.VideoRentalDBContext context)
        {
            //This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.E.g.

            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );


            context.Users.AddOrUpdate(
                u => u.UserID,
                new User
                {
                    UserID = 1,
                    UserName = "manager",
                    //password is manager and have encoded by sha256 algorithm
                    Password = "6ee4a469cd4e91053847f5d3fcb61dbcc91e8f0ef10be7748da4c4a1ba382d17",
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
                    Password = "c40dc72b0228e5850d8b173ff861a48acfb4a15b37b2849cbb6584bbadbc7907",
                    Address = "Hồ Chí Minh",
                    PhoneNumber = "0985468764",
                    Email = "clerk01@gmail.com",
                    Role = UserRole.Clerk
                }
            );

            context.Customers.AddOrUpdate(
              c => c.CustomerID,
              new Customer
              {
                  CustomerID = 1,
                  FirstName = "Tiến",
                  LastName = "Lê Đức",
                  Address = "Buôn Mê Thuộc",
                  PhoneNumber = "0987654321",
                  DateOfBirth = new DateTime(1995, 12, 10),
                  DateCreate = DateTime.Now,
                  DateUpdate = DateTime.Now,
                  UpdatedUser = 2  //clerk Id
              },

              new Customer
              {
                  CustomerID = 2,
                  FirstName = "Phi",
                  LastName = "Nguyễn Minh",
                  Address = "Hà Tĩnh",
                  PhoneNumber = "0123456789",
                  DateOfBirth = new DateTime(1996, 05, 16),
                  DateCreate = DateTime.Now,
                  DateUpdate = DateTime.Now,
                  UpdatedUser = 2  //clerk Id
              }
          );

            context.DiskTitles.AddOrUpdate(
                t => t.TitleID,
                new DiskTitle
                {
                    TitleID = 1,
                    Title = "Siêu Nhân Gao",
                    Tags = "Hành Động",
                    ImageLink = "Default",
                    Quantity = 3
                },

                new DiskTitle
                {
                    TitleID = 2,
                    Title = "Em Chưa 18",
                    Tags = "Hành Động, Tình Cảm",
                    ImageLink = "Default",
                    Quantity = 3
                }
            );

            context.Disks.AddOrUpdate(
                d => d.DiskID,
                //disk 1, title 1 (disk id is 1)
                new Disk
                {
                    DiskID = 1,
                    TitleID = 1, //title id
                    Status = DiskStatus.RENTABLE,
                    PurchasePrice = 200000,
                    RentedTime = 0,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    UpdatedUser = 1 //manager
                },
                //disk 2, title 1 (disk id is 2)
                new Disk
                {
                    DiskID = 2,
                    TitleID = 1, //title id
                    Status = DiskStatus.BOOKED,
                    PurchasePrice = 200000,
                    RentedTime = 0,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    UpdatedUser = 1 //manager
                },
                //disk 3, title 1 (disk id is 3)
                new Disk
                {
                    DiskID = 3,
                    TitleID = 1, //title id
                    Status = DiskStatus.RENTED,
                    PurchasePrice = 200000,
                    RentedTime = 0,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    UpdatedUser = 1 //manager
                },
                //disk 1, title 2 (disk id is 4)
                new Disk
                {
                    DiskID = 4,
                    TitleID = 2, //title id
                    Status = DiskStatus.RENTABLE,
                    PurchasePrice = 400000,
                    RentedTime = 0,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    UpdatedUser = 1 //manager
                },
                //disk 2, title 2 (disk id is 5)
                new Disk
                {
                    DiskID = 5,
                    TitleID = 2, //title id
                    Status = DiskStatus.BOOKED,
                    PurchasePrice = 400000,
                    RentedTime = 0,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    UpdatedUser = 1 //manager
                },
                //disk 3, title 2 (disk id is 6)
                new Disk
                {
                    DiskID = 6,
                    TitleID = 2, //title id
                    Status = DiskStatus.RENTED,
                    PurchasePrice = 400000,
                    RentedTime = 0,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    UpdatedUser = 1 //manager
                }
            );

            context.RentalRates.AddOrUpdate(
                r => r.RentalRateId,
                //Rental Rate 1 for title 1
                new RentalRate
                {
                    RentalRateId = 1,
                    TitleID = 1,
                    RentalPrice = 5000,
                    LateCharge = 10000,
                    RentalPeriod = 10,
                    CreatedDate = DateTime.Now
                },
                //Rental Rate 2 for title 1
                new RentalRate
                {
                    RentalRateId = 2,
                    TitleID = 1,
                    RentalPrice = 4000,
                    LateCharge = 7000,
                    RentalPeriod = 8,
                    CreatedDate = new DateTime(2017, 11, 03)
                },

                //Rental Rate 1 for title 2
                new RentalRate
                {
                    RentalRateId = 3,
                    TitleID = 2,
                    RentalPrice = 10000,
                    LateCharge = 10000,
                    RentalPeriod = 20,
                    CreatedDate = DateTime.Now
                },
                //Rental Rate 2 for title 2
                new RentalRate
                {
                    RentalRateId = 4,
                    TitleID = 2,
                    RentalPrice = 10000,
                    LateCharge = 7000,
                    RentalPeriod = 10,
                    CreatedDate = new DateTime(2017, 11, 03)
                }
            );

            context.TransactionHistories.AddOrUpdate(
                t => t.TransactionHistoryID,
                // customer 1 rent disk 2, title 1 from clerk 1
                new TransactionHistory
                {
                    TransactionHistoryID = 1,
                    CreatedDate = DateTime.Now,
                    TotalPurchaseCost = 5000,
                    CustomerID = 1,
                    ClerkID = 1
                },
                // customer 2 rent disk 2, title 2 from clerk 1
                new TransactionHistory
                {
                    TransactionHistoryID = 2,
                    CreatedDate = DateTime.Now,
                    TotalPurchaseCost = 10000,
                    CustomerID = 2,
                    ClerkID = 1
                }
            );

            context.TransactionHistoryDetails.AddOrUpdate(
                td => td.TransactionDetailID,
                //Transaction detail 1 of transaction 1
                new TransactionHistoryDetail
                {
                    TransactionDetailID = 1,
                    DiskID = 2,
                    TransactionID = 1,
                },

                // Transaction detail 1 of transaction 2
                new TransactionHistoryDetail
                {
                    TransactionDetailID = 2,
                    DiskID = 5,
                    TransactionID = 1,
                }
            );

            context.Reservations.AddOrUpdate(
                r => r.TitleID,
                //Reservation of title 1, customer 1
                new Reservation
                {
                    TitleID = 1,
                    CustomerID = 1,
                    ReservationDate = DateTime.Now,
                    Status = ReservationStatus.ON_HOLD
                },

                //Reservation of title 2, customer 2
                new Reservation
                {
                    TitleID = 2,
                    CustomerID = 2,
                    ReservationDate = DateTime.Now,
                    Status = ReservationStatus.ON_HOLD
                }
            );

        }
    }
}
