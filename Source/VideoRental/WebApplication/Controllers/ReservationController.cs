using System;
using System.Web.Mvc;
using WebApplication.Services;
namespace WebApplication.Controllers
{
    public class ReservationController : Controller
    {
        private static string CUSTOMER_ID_SESSION = "customer";
        private static string LIST_TITLE_SESSION = "listtitle";
        private static string TAG = "ReservationController: ";

        IReservationService iReservation;

        [HttpGet]
        public ActionResult ReservationManagement(string reservationID)
        {
            return View(iReservation.GetReservation(reservationID));
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
            Console.WriteLine(TAG + " in Action " + "ShowTitles");
            return View(iReservation.GetTitles(titleName));
        }

        [HttpPost]
        public ActionResult SaveTitle(string[] titleID)
        {
            Console.WriteLine(TAG + " in Action " + "SaveTitle");
            Session[LIST_TITLE_SESSION] = titleID;
            return View();
        }


        [HttpGet]
        public ActionResult ShowCustomers(string customerName)
        {
            Console.WriteLine(TAG + " in Action " + "ShowCustomers");
            return View(iReservation.GetCustomers(customerName));
        }

        [HttpPost]
        public ActionResult AddReservation(string customerID)
        {
            Console.WriteLine(TAG + " in Action " + "AddReservation");

            string[] titleID = (string[])Session[LIST_TITLE_SESSION];
            iReservation.AddReservation(titleID, customerID);

            if (titleID.Length > 0 && customerID != null)
            {
                iReservation.AddReservation(titleID, customerID);
            }
            else
            {
                if (titleID.Length <= 0)
                    Console.WriteLine(TAG + " List of Title Name < 0 " + "");
                if (customerID != null)
                    Console.WriteLine(TAG + " customerID Null " + "");
                // Handle NULL POINTER
            }
            return View();
        }

    }
}