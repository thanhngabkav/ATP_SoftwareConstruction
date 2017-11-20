using DataAccess.Entities;
using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;
namespace WebApplication.Controllers
{
    public class ReservationController : Controller
    {
        private const string CUSTOMER_ID_SESSION = "customer";
        private const string TITLE_CHOSEN_SESSION = "titlerentedsession";

        IReservationService iReservation;


        public ReservationController(IReservationService iReservation)
        {
            this.iReservation = iReservation;
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Index(string customerNameOrID, string status)
        {
            TagDebug.D(GetType(), " in Action " + "ReservationManagement");
            ViewBag.status = status;
            return View(iReservation.GetReservation(customerNameOrID));
        }

        /**
         * Reserve A Title View
         * Auto Complete search
         * @param no param
         * @return : 
         * */
        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowTitles(string titleName, string titleIDs)
        {
            TagDebug.D(GetType(), " in Action " + "ShowTitles");
            ViewBag.titleName = titleName;
            IList<TitleView> listTitleName = iReservation.GetTitles(titleName);
            if (titleIDs !=null)
            {
                int titleID = Int32.Parse(titleIDs);
                if (Session[TITLE_CHOSEN_SESSION] == null)
                {
                    Session[TITLE_CHOSEN_SESSION] = new List<Int32>
                {
                    titleID
                };
                }
                else
                {
                    List<Int32> chosenTitle = (List<Int32>)Session[TITLE_CHOSEN_SESSION];
                    if (chosenTitle.Any(x => x == titleID))
                        chosenTitle.Remove(chosenTitle.SingleOrDefault(x => x == titleID));
                    else
                        chosenTitle.Add(titleID);
                    Session[TITLE_CHOSEN_SESSION] = chosenTitle;
                }
            }
            List<Int32> rentedList = (List<Int32>)Session[TITLE_CHOSEN_SESSION];
            if (rentedList != null)
                foreach (int data in rentedList)
                    if (listTitleName.Any(x => x.titleID == data))
                        listTitleName.Where(x => x.titleID == data).First().IsChosen = !listTitleName.Where(x => x.titleID == data).First().IsChosen;
            return View(listTitleName);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult SaveTitle()
        {
            TagDebug.D(GetType(), " in Action " + "SaveTitle");
            return RedirectToAction("ShowCustomers");
        }


        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowCustomers(string customerName, string reservationState)
        {
            TagDebug.D(GetType(), " in Action " + "ShowCustomers");
            ViewBag.IsReservationExist = reservationState;
            return View(iReservation.GetCustomers(customerName));
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult AddReservation(int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "AddReservation");
            int[] titleID = ((List<Int32>)Session[TITLE_CHOSEN_SESSION]).ToArray();
            string reservationState = "";
            if (titleID.Length > 0 && customerID > 0)
            {
                if (IsReservationExist(titleID, customerID))
                {
                    reservationState = "Khách hàng đã đặt đĩa đó rồi nhé!";
                }
                else
                {
                    iReservation.AddReservation(titleID, customerID);
                    reservationState = "Đặt Thành Công";
                    Session[TITLE_CHOSEN_SESSION] = null;
                }

            }
            else
            {
                if (titleID.Length <= 0)
                    TagDebug.D(GetType(), " List of Title Name < 0 " + "");
                if (customerID != 0)
                    TagDebug.D(GetType(), " customerID Null " + "");
                // Handle NULL POINTER
            }
            return RedirectToAction("Index", new { status = reservationState });
        }

        private bool IsReservationExist(int[] titleID, int customerID)
        {
            return iReservation.CheckReservationForExistence(titleID, customerID);
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ConfirmReservation(int titleID, int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "ConfirmReservation");
            string status = "";
            if (titleID != 0 && customerID != 0)
            {
                iReservation.CancelReservation(titleID, customerID);
                status = "Hủy Đặt Thành Công";
            }
            else
            {
                if (titleID != 0)
                    TagDebug.D(GetType(), " titleID Null ");
                if (customerID != 0)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
                status = "Hủy Đặt Không Thành";
            }

            return RedirectToAction("Index", new { status = status });
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowChosenTitle()
        {
            IList<Int32> titleChosen = (IList<Int32>)Session[TITLE_CHOSEN_SESSION];
            if (titleChosen == null) return new EmptyResult();
            if (titleChosen.Count > 0)
            {

                IList<TitleView> titleViews = new List<TitleView>();
               foreach(int title in titleChosen)
                {
                    DiskTitle Atitle = iReservation.GetATitle(title);
                    titleViews.Add(new TitleView(Atitle.TitleID, Atitle.Title,Atitle.Tags, Atitle.ImageLink, Atitle.Quantity));
                }
                return PartialView(titleViews);
            }
            return null;

        }
    }
}