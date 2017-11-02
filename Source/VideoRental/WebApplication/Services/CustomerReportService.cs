using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Utilities;
using WebApplication.Models;
namespace WebApplication.Services
{
    /// <summary>
    /// Customer report service
    /// </summary>
    public class CustomerReportService : ICustomerReportService
    {
        private CustomerDAO customerDAO;
        private TranSactionDAO tranSactionDAO;
        private TransactionDetailsDAO transactionDetailsDAO;
        private DiskDAO diskDAO;
        private TitleDAO titleDAO;
        private RentalRateDAO rentalRateDAO;

        public List<CustomerReportModel> Report_AllCustomer()
        {
            List<Customer> listCustomer = customerDAO.GetAllCustomer();
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();

            foreach (Customer customer in listCustomer)
            {
                CustomerReportModel customerReportModel = new CustomerReportModel();
                //Set information for each customer rp model
                int cusID = customer.CustomerID;
                string cusName = customer.FirstName + " " + customer.LastName;
                string address = customer.Address;
                string phone = customer.PhoneNumber;
                int totalDisk = 0;
                float totalFines= 0;
                //list disk over due
                List<DiskOverDueModel> diskOverDues = new List<DiskOverDueModel>();
                //list late charge 
                List<LateCharge> lateCharges = new List<LateCharge>();
                foreach(TransactionHistory transaction in customer.TransactionHistorys)
                {                     
                    List<TransactionHistoryDetail> transactionDetails = transactionDetailsDAO.GeListTransactionDetailsByTransactionId(transaction.TransactionHistoryID);
                    foreach(TransactionHistoryDetail transactionDetail in transactionDetails)
                    {
                        Disk disk = diskDAO.GetDiskById(transactionDetail.DiskID);
                        DiskTitle title = titleDAO.GetTitleById(disk.TitleID);
                        RentalRate curentRentalRate = rentalRateDAO.GetCurrentRentalRate(title.TitleID);
                        DateTime dateReturn = (transaction.CreatedDate).AddDays(curentRentalRate.RentalPeriod);
                        //get list over due disk
                        if (transactionDetail.DateReturn.Equals(null))// disk wasn't return. Total disk currently has out ++
                        {
                            totalDisk++;
                            // check disk over due
                            if ((DateTime.Now - transaction.CreatedDate).TotalDays > curentRentalRate.RentalPeriod)
                            {
                                DiskOverDueModel diskOverDue = new DiskOverDueModel();
                                diskOverDue.DiskID = disk.DiskID;
                                diskOverDue.TitleName = title.Title;
                                diskOverDue.DateReturn = dateReturn;
                                diskOverDues.Add(diskOverDue);
                            }
                        }
                        //check late charge
                        if (transactionDetail.Status.Equals(TransactionDetailStatus.DUE))
                        {
                            LateCharge lateCharge = new LateCharge();
                            lateCharge.DiskID = disk.DiskID;
                            lateCharge.Title = title.Title;
                            //Ngày phải trả
                            lateCharge.DateReturn = dateReturn;
                            //Ngày trả thực tế
                            lateCharge.DateActuallyReturn = transactionDetail.DateReturn;
                            lateCharge.Cost = curentRentalRate.LateCharge;
                            //add late charge in list
                            lateCharges.Add(lateCharge);
                            //
                            totalFines += lateCharge.Cost;
                        }
                    }   
                }
                // set properties for customer rp model
                customerReportModel.CustomerID = cusID;
                customerReportModel.CustomerName = cusName;
                customerReportModel.Address = address;
                customerReportModel.PhoneNumber = phone;
                customerReportModel.DiskOverDues = diskOverDues;
                customerReportModel.TotalDisk = totalDisk;
                customerReportModel.LateCharges = lateCharges;
                customerReportModel.TotalFines = totalFines;
                //add in result list
                listResult.Add(customerReportModel);
            }
            return listResult;
        }

        public List<CustomerReportModel> Report_LateFeeCustomer()
        {
            throw new NotImplementedException();
        }

        public List<CustomerReportModel> Report_OverDueCustomer()
        {
            throw new NotImplementedException();
        }
    }
}