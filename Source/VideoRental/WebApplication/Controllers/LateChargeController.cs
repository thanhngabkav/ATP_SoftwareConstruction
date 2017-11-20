using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class LateChargeController : Controller
    {

        ILateChargesServices iLateChargesServices;

        public LateChargeController(ILateChargesServices iLateChargesServices)
        {
            this.iLateChargesServices = iLateChargesServices;
        }


        private const string CUSTOMER_SESSION = "currentCustomerID";

        // Show customer has late charge
        /// <summary>
        /// LateChargeManagement
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Index(string status)
        {
            ViewBag.status = status;
            TagDebug.D(GetType(), " in Action " + "ShowLateCharge");
            return View(iLateChargesServices.FindCustomersHasLateCharge());
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult RecordLateCharge(int customerID, string status)
        {
            TagDebug.D(GetType(), " in Action " + "RecordLateCharge GET");
            ViewBag.status = status;
            int currentCustomerID = 0;
            if (customerID == 0)
                currentCustomerID = (int)Session[CUSTOMER_SESSION];
            else
            {
                Session[CUSTOMER_SESSION] = customerID;
                currentCustomerID = customerID;
            }
            ViewBag.numberLateCharge = iLateChargesServices.GetNumberOfLateCharge(currentCustomerID);
            return View();
        }


        /// <summary>
        /// number Record want to pay
        /// </summary>
        /// <param name="numberRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult RecordASpecificLateCharge(int numberRequest)
        {
            TagDebug.D(GetType(), " in Action " + "RecordASpecificLateCharge");
            int customerID = (int)Session[CUSTOMER_SESSION];
            iLateChargesServices.RecordLateCharge(customerID, numberRequest);
            return RedirectToAction("RecordLateCharge", new { customerID = customerID, status = "Ghi Nhận trễ hạn thành công" });
        }

        private bool IsEnoughForRecordlateCharge(int numberLatecharge, NumberRequestView numberRequest)
        {
            return numberLatecharge >= numberRequest.number && numberLatecharge > 0 && numberRequest.number > 0 ? true : false;
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult CancelLateCharge(int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "CancelLateCharge ");
            return View(Report_OverDueCustomer(customerID));
        }
        private List<CustomerReportModel> Report_OverDueCustomer(int customerID)
        {
            TransactionDetailsDAO transactionDetailsDAO = new TransactionDetailsDAO();
            CustomerDAO customerDAO = new CustomerDAO();
            DiskDAO diskDAO = new DiskDAO();
            TitleDAO titleDAO = new TitleDAO();
            RentalRateDAO rentalRateDAO = new RentalRateDAO();

            Customer customer = customerDAO.GetCustomerById(customerID);
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();

            CustomerReportModel customerReportModel = new CustomerReportModel();
            //Set information for each customer rp model
            int cusID = customer.CustomerID;
            string cusName = customer.LastName + " " + customer.FirstName;
            string address = customer.Address;
            string phone = customer.PhoneNumber;
            int totalDisk = 0;
            float totalFines = 0;
            //list disk over due
            List<DiskOverDueModel> diskOverDues = new List<DiskOverDueModel>();
            //list late charge 
            List<LateCharge> lateCharges = new List<LateCharge>();
            foreach (TransactionHistory transaction in customer.TransactionHistorys)
            {
                List<TransactionHistoryDetail> transactionDetails = transactionDetailsDAO.GetListTransactionDetailsByTransactionId(transaction.TransactionHistoryID);
                foreach (TransactionHistoryDetail transactionDetail in transactionDetails)
                {
                    Disk disk = diskDAO.GetDiskById(transactionDetail.DiskID);
                    DiskTitle title = titleDAO.GetTitleById(disk.TitleID);
                    RentalRate nearestRentalRate = rentalRateDAO.GetNearestRentalRate(title.TitleID, transaction.CreatedDate);
                    DateTime dateReturn = (transaction.CreatedDate).AddDays(nearestRentalRate.RentalPeriod);
                    //get list over due disk
                    if (transactionDetail.Status == null)
                    {
                        if (transactionDetail.DateReturn.Equals(null))// disk wasn't return. Total disk currently has out ++
                        {
                            totalDisk++;
                            // check disk over due
                            if ((DateTime.Now - transaction.CreatedDate).TotalDays > nearestRentalRate.RentalPeriod)
                            {
                                DiskOverDueModel diskOverDue = new DiskOverDueModel();
                                diskOverDue.DiskID = disk.DiskID;
                                diskOverDue.TitleName = title.Title;
                                diskOverDue.DateReturn = dateReturn;
                                diskOverDues.Add(diskOverDue);
                            }
                        }
                    }
                    else
                    {
                        if (transactionDetail.Status.Equals(TransactionDetailStatus.DUE))//có nợ
                        {
                            LateCharge lateCharge = new LateCharge();
                            lateCharge.DiskID = disk.DiskID;
                            lateCharge.Title = title.Title;
                            //Ngày phải trả
                            lateCharge.DateReturn = dateReturn;
                            //Ngày trả thực tế
                            lateCharge.DateActuallyReturn = transactionDetail.DateReturn.Value;
                            lateCharge.Cost = nearestRentalRate.LateCharge;
                            lateCharge.transactionDetailID = transactionDetail.TransactionDetailID;
                            //add late charge in list
                            lateCharges.Add(lateCharge);
                            //
                            totalFines += lateCharge.Cost;
                        }
                    }
                    //check late charge
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
            return listResult;
        }

        /// <summary>
        /// Change status of Transaction History
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult CancelASpecificLateCharge(int transactionID)
        {
            TagDebug.D(GetType(), " in Action " + "CancelASpecificLateCharge ");
            IList<TransactionHistoryView> lateCharge =  iLateChargesServices.GetAllLateChargeOfCustomer(transactionID);
            foreach(TransactionHistoryView trans in lateCharge)
               iLateChargesServices.CancelLateCharge(trans.TransactionHistoryID);

            return RedirectToAction("Index", new { status = "Hủy Trễ Hạn Thành Công" });
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult LateChargePayment(NumberRequestView numberRequest)
        {
            int customerID = (int)Session[CUSTOMER_SESSION];
            int numberLatecharge = iLateChargesServices.GetNumberOfLateCharge(customerID);
            if (IsEnoughForRecordlateCharge(numberLatecharge, numberRequest))
            {
                ViewBag.LateChargePayment = iLateChargesServices.GetTotalLateChargePrice(customerID, numberRequest.number);
                return View(numberRequest);
            }
            return RedirectToAction("RecordLateCharge", new { customerID = customerID, status = "Không đủ để xóa" });
        }

    }
}