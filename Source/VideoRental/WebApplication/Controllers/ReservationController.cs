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
            return RedirectToAction("ShowCustomers");
        }


        [HttpGet]
        public ActionResult ShowCustomers(string customerName)
        {
            TagDebug.D(GetType(), " in Action " + "ShowCustomers");
            return View(iReservation.GetCustomers(customerName));
        }

        [HttpGet]
        public ActionResult AddReservation(string customerID)
        {
            TagDebug.D(GetType(), " in Action " + "AddReservation");
            string[] titleID = (string[])Session[LIST_TITLE_SESSION];

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
        public ActionResult RequestCancelReservation(int titleID, int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "RequestCancelReservation");
            return View();
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