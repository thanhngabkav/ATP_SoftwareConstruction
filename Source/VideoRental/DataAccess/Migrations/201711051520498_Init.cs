namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                {
                    CustomerID = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Address = c.String(nullable: false),
                    PhoneNumber = c.String(nullable: false),
                    DateOfBirth = c.DateTime(nullable: false),
                    DateCreate = c.DateTime(nullable: false),
                    DateUpdate = c.DateTime(nullable: false),
                    UpdatedUser = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Users", t => t.UpdatedUser, cascadeDelete: false)
                .Index(t => t.UpdatedUser);

            CreateTable(
               "dbo.Users",
               c => new
               {
                   UserID = c.Int(nullable: false, identity: true),
                   UserName = c.String(nullable: false, maxLength: 200),
                   Password = c.String(nullable: false, maxLength: 200),
                   Address = c.String(nullable: false),
                   PhoneNumber = c.String(nullable: false),
                   Email = c.String(),
                   Role = c.String(maxLength: 50),
               })
               .PrimaryKey(t => t.UserID);

            CreateTable(
                "dbo.Disks",
                c => new
                {
                    DiskID = c.Int(nullable: false, identity: true),
                    TitleID = c.Int(nullable: false),
                    Status = c.String(nullable: false),
                    PurchasePrice = c.Single(nullable: false),
                    RentedTime = c.Int(nullable: false),
                    LastRentedDate = c.DateTime(),
                    DateUpdate = c.DateTime(nullable: false),
                    DateCreate = c.DateTime(nullable: false),
                    UpdatedUser = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.DiskID)
                .ForeignKey("dbo.Users", t => t.UpdatedUser, cascadeDelete: false)
                .ForeignKey("dbo.DiskTitles", t => t.TitleID, cascadeDelete: false)
                .Index(t => t.TitleID)
                .Index(t => t.UpdatedUser);

            CreateTable(
                "dbo.DiskTitles",
                c => new
                {
                    TitleID = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Tags = c.String(),
                    ImageLink = c.String(),
                    Quantity = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TitleID);

            CreateTable(
                "dbo.RentalRates",
                c => new
                {
                    RentalRateId = c.Int(nullable: false, identity: true),
                    RentalPrice = c.Single(nullable: false),
                    LateCharge = c.Single(nullable: false),
                    RentalPeriod = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    TitleID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.RentalRateId)
                .ForeignKey("dbo.DiskTitles", t => t.TitleID, cascadeDelete: true)
                .Index(t => t.TitleID);

            CreateTable(
                "dbo.Reservations",
                c => new
                {
                    TitleID = c.Int(nullable: false),
                    CustomerID = c.Int(nullable: false),
                    ReservationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.TitleID, t.CustomerID })
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.DiskTitles", t => t.TitleID, cascadeDelete: true)
                .Index(t => t.TitleID)
                .Index(t => t.CustomerID);

            CreateTable(
                "dbo.TransactionHistoryDetails",
                c => new
                {
                    TransactionDetailID = c.Int(nullable: false, identity: true),
                    TransactionID = c.Int(nullable: false),
                    Status = c.String(maxLength: 50),
                    DiskID = c.Int(nullable: false),
                    DateReturn = c.DateTime(),
                    IncurreCost = c.Single(nullable: false),
                    Note = c.String(),
                })
                .PrimaryKey(t => t.TransactionDetailID)
                .ForeignKey("dbo.Disks", t => t.DiskID, cascadeDelete: true)
                .ForeignKey("dbo.TransactionHistories", t => t.TransactionID, cascadeDelete: true)
                .Index(t => t.TransactionID)
                .Index(t => t.DiskID);

            CreateTable(
                "dbo.TransactionHistories",
                c => new
                {
                    TransactionHistoryID = c.Int(nullable: false, identity: true),
                    CreatedDate = c.DateTime(nullable: false),
                    TotalPurchaseCost = c.Single(nullable: false),
                    Status = c.String(maxLength: 50),
                    Note = c.String(),
                    ClerkID = c.Int(nullable: false),
                    CustomerID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TransactionHistoryID)
                .ForeignKey("dbo.Users", t => t.ClerkID, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false)
                .Index(t => t.ClerkID)
                .Index(t => t.CustomerID);

        }


        public override void Down()
        {
            DropForeignKey("dbo.Customers", "UpdatedUser", "dbo.Users");
            DropForeignKey("dbo.Customers", "CreatedUser", "dbo.Users");
            DropForeignKey("dbo.Customers", "User_UserID1", "dbo.Users");
            DropForeignKey("dbo.TransactionHistoryDetails", "TransactionID", "dbo.TransactionHistories");
            DropForeignKey("dbo.TransactionHistories", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.TransactionHistories", "ClerkID", "dbo.Users");
            DropForeignKey("dbo.TransactionHistoryDetails", "DiskID", "dbo.Disks");
            DropForeignKey("dbo.Reservations", "TitleID", "dbo.DiskTitles");
            DropForeignKey("dbo.Reservations", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.RentalRates", "TitleID", "dbo.DiskTitles");
            DropForeignKey("dbo.Disks", "TitleID", "dbo.DiskTitles");
            DropForeignKey("dbo.Disks", "CreatedUser", "dbo.Users");
            DropForeignKey("dbo.Customers", "User_UserID", "dbo.Users");
            DropIndex("dbo.TransactionHistories", new[] { "CustomerID" });
            DropIndex("dbo.TransactionHistories", new[] { "ClerkID" });
            DropIndex("dbo.TransactionHistoryDetails", new[] { "DiskID" });
            DropIndex("dbo.TransactionHistoryDetails", new[] { "TransactionID" });
            DropIndex("dbo.Reservations", new[] { "CustomerID" });
            DropIndex("dbo.Reservations", new[] { "TitleID" });
            DropIndex("dbo.RentalRates", new[] { "TitleID" });
            DropIndex("dbo.Disks", new[] { "CreatedUser" });
            DropIndex("dbo.Disks", new[] { "TitleID" });
            DropIndex("dbo.Customers", new[] { "User_UserID1" });
            DropIndex("dbo.Customers", new[] { "User_UserID" });
            DropIndex("dbo.Customers", new[] { "UpdatedUser" });
            DropIndex("dbo.Customers", new[] { "CreatedUser" });
            DropTable("dbo.TransactionHistories");
            DropTable("dbo.TransactionHistoryDetails");
            DropTable("dbo.Reservations");
            DropTable("dbo.RentalRates");
            DropTable("dbo.DiskTitles");
            DropTable("dbo.Disks");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
        }
    }
}
