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

        [HttpGet]
        public ActionResult ReservationManagement(string customerNameOrID)
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
        public ActionResult SaveTitle(string[] titleID)
        {
            TagDebug.D(GetType(), " in Action " + "SaveTitle");
            Session[LIST_TITLE_SESSION] = titleID;
            return View();
        }


        [HttpGet]
        public ActionResult ShowCustomers(string customerName)
        {
            TagDebug.D(GetType(), " in Action " + "ShowCustomers");
            return View(iReservation.GetCustomers(customerName));
        }

        [HttpPost]
        public ActionResult AddReservation(string customerID)
        {
            TagDebug.D(GetType(), " in Action " + "AddReservation");
            string[] titleID = (string[])Session[LIST_TITLE_SESSION];
            iReservation.AddReservation(titleID, customerID);

            if (titleID.Length > 0 && customerID != null)
            {
                iReservation.AddReservation(titleID, customerID);
            }
            else
            {
                if (titleID.Length <= 0)
                    TagDebug.D(GetType(), " List of Title Name < 0 " + "");
                if (customerID != null)
                    TagDebug.D(GetType(), " customerID Null " + "");
                // Handle NULL POINTER
            }
            return View();
        }

        [HttpGet]
        public ActionResult RequestCancelReservation(String titleID, String customerID)
        {
            TagDebug.D(GetType(), " in Action " + "RequestCancelReservation");
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmReservation(String titleID, String customerID)
        {
            TagDebug.D(GetType(), " in Action " + "ConfirmReservation");
            if (titleID != null && customerID != null)
            {
                iReservation.CancelReservation(titleID, customerID);
            }
            else
            {
                if (titleID != null)
                    TagDebug.D(GetType(), " titleID Null ");
                if (customerID != null)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
            }

            return View();
        }
    }
}