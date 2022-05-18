//Name: [Kim_Angela_Eonjeong]
//Date: [Feb 18, 2022]
//Description: HW2 – Food Truck Checkout


using Microsoft.AspNetCore.Mvc;

using Kim_Angela_Eonjeong_HW2.Models;

namespace Kim_Angela_Eonjeong_HW2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckoutCatering()
        {
            return View();
        }

        public IActionResult CheckoutWalkup()
        {
            return View();
        }

        public IActionResult CateringTotals(CateringOrder cateringOrder)
        {
           
            TryValidateModel(cateringOrder);

            if (ModelState.IsValid == false)
            {
                return View("CheckoutCatering",cateringOrder);
            }

            cateringOrder.OrderCustomerType = CustomerType.Catering;

            cateringOrder.CalcTotals();

            return View("CateringTotals", cateringOrder);

        }

        public IActionResult WalkupTotals(WalkupOrder walkupOrder)
        {
            TryValidateModel(walkupOrder);

            if (ModelState.IsValid == false)
            {
                return View("CheckoutWalkup");
            }

            walkupOrder.OrderCustomerType = CustomerType.WalkUp;

            walkupOrder.CalcTotals();

            return View("WalkupTotals", walkupOrder);

        }

    }
}
