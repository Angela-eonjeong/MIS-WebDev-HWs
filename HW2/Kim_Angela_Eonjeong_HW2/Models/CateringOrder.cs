//add namespace for data annotations
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//add namespace for C# data types
using System;


namespace Kim_Angela_Eonjeong_HW2.Models
{
    public class CateringOrder : Order
    {
        [Display(Name = "Customer Code:")]
        [StringLength(4, MinimumLength = 2, ErrorMessage = "Customer code must be 2-4 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Customer code may only contain letters")]
        public String CustomerCode { get; set; }

        [Display(Name = "Delivery Fee:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal DeliveryFee { get; set; }


        [Display(Name = "Preferred Customer:")]
        public Boolean PreferredCustomer { get; set; }

        public void CalcTotals()
        {
            CalcSubtotals();
            if (PreferredCustomer == true || Subtotal >= 1000)
            {
                DeliveryFee = 0m;
            }
            Total = Subtotal + DeliveryFee;
        }
    }
}