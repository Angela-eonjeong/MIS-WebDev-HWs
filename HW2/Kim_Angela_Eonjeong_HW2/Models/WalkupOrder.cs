using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System;

namespace Kim_Angela_Eonjeong_HW2.Models
{
    public class WalkupOrder : Order
    {
        public const Decimal SALES_TAX_RATE = 0.0825m;

        [Display(Name = "Customer Name:")]
        public String CustomerName { get; set; }


        [Display(Name = "Sales Tax:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal SalesTax { get; private set; }

        public void CalcTotals()
        {
            CalcSubtotals();
            SalesTax = Subtotal * SALES_TAX_RATE;
            Total = SalesTax + Subtotal;
        }

    }

}