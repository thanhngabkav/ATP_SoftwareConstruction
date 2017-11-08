using DataAccess.Utilities;
using System;
using System.Web.Mvc;
using WebApplication.Services;
namespace WebApplication.Controllers
{
    public class ReservationController : Controller
    {
        private const string CUSTOMER_ID_SESSION = "customer";
        private const string LIST_TITLE_SESSION = "listtitle";

        IReservationService iReservation;
      

        public ReservationController(IReservationService iReservation)
        {
            this.iReservation = iReservation;
        }

        [HttpGet]
        public ActionResult Index(string customerNameOrID)
        {
            TagDebug.D(GetType(), " in Action " + "ReservationManagement");
            return View(iReservation.GetReservation(customerNameOrID));
        }

        /**
         * Reserve A Title View
         * Auto Complete search
         * @param no param
         * @return : 
         * */
        [HttpGet]
        public ActionResult ShowTitles(string titleName)
        {
            TagDebug.D(GetType(), " in Action " + "ShowTitles");
            return View(iReservation.GetTitles(titleName));
        }

        [HttpPost]
        public ActionResult SaveTitle(int[] titleID)
        {
            TagDebug.D(GetType(), " in Action " + "SaveTitle");
            Session[LIST_TITLE_SESSION] = titleID;
            return RedirectToAction("ShowCustomers");
        }


        [HttpGet]
        public ActionResult ShowCustomers(string customerName, string reservationState)
        {
            TagDebug.D(GetType(), " in Action " + "ShowCustomers");
            ViewBag.IsReservationExist = reservationState;
            return View(iReservation.GetCustomers(customerName));
        }

        [HttpGet]
        public ActionResult AddReservation(int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "AddReservation");
            int[] titleID = (int[])Session[LIST_TITLE_SESSION];
            string reservationState = "";
            if (titleID.Length > 0 && customerID > 0)
            {
                if (IsReservationExist(titleID, customerID))
                {
                    reservationState = "Khách hàng đã đặt đĩa đó rồi nhé!";
                  
                } else
                {
                    iReservation.AddReservation(titleID, customerID);
                    reservationState = "Đặt Thành Công";
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
            return RedirectToAction("ShowTitles", new { reservationState = reservationState});
        }

        private bool IsReservationExist(int[] titleID, int customerID)
        {
            return iReservation.CheckReservationForExistence(titleID, customerID);
        }

        [HttpGet]
        public ActionResult ConfirmReservation(int titleID, int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "ConfirmReservation");
            if (titleID != 0 && customerID != 0)
            {
                iReservation.CancelReservation(titleID, customerID);
            }
            else
            {
                if (titleID != 0)
                    TagDebug.D(GetType(), " titleID Null ");
                if (customerID != 0)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
            }

            return RedirectToAction("ReservationManagement");
        }
    }
}