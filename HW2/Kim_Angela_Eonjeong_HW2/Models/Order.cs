using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System;

namespace Kim_Angela_Eonjeong_HW2.Models
{
    public enum CustomerType { WalkUp, Catering }

    public abstract class Order
    {
        public const Decimal TACO_PRICE = 2.75m;
        public const Decimal BURGER_PRICE = 4.50m;

        [Display(Name = "Customer Type:")]
        public CustomerType OrderCustomerType { get; set; }

        [Display(Name = "Number Of Burgers:")]
        [Required(ErrorMessage = "Please enter number for order")]
        public Int32 NumberOFBurgers { get; set; }


        [Display(Name = "Number Of Tacos:")]
        [Required(ErrorMessage = "Please enter number for order")]
        public Int32 NumberOFTacos { get; set; }


        [Display(Name = "Burger Subtotal:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal BurgerSubtotal { get; private set; }



        [Display(Name = "Taco Subtotal:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TacoSubtotal { get; private set; }



        [Display(Name = "Subtotal:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Subtotal { get; set; }



        [Display(Name = "Total Items:")]
        public Int32 TotalItems { get; private set; }


        [Display(Name = "Total :")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Total { get; set; }



        public void CalcSubtotals()
        {
            BurgerSubtotal = NumberOFBurgers * BURGER_PRICE;
            TacoSubtotal = NumberOFTacos * TACO_PRICE;
            Subtotal = BurgerSubtotal + TacoSubtotal;
            TotalItems = NumberOFBurgers + NumberOFTacos;
            if (TotalItems == 0)
            {
                throw new Exception("You must purchase at least 1 item");
            }

        }

    }

}

