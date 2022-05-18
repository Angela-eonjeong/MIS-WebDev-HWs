using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kim_Angela_Eonjeong_HW5.Models
{
    public class Order
    {
        private const Decimal TAX_RATE = 0.0825m;

        [Key]
        public Int32 OrderID { get; set; }

        [Display(Name = "Order Number:")]
        public Int32 OrderNumber { get; set; }

        [Display(Name = "Order Date:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Notes:")]
        public String OrderNotes { get; set; }

        [Display(Name = "Order Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public Decimal OrderSubtotal
        {
            get { return OrderDetails.Sum(rd => rd.ExtendedPrice); }
        }

        [Display(Name = "Tax Fee (8.25%)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public Decimal TaxFee
        {
            get { return OrderSubtotal * TAX_RATE; }
        }

        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public Decimal OrderTotal
        {
            get { return OrderSubtotal + TaxFee; }
        }

        public List<OrderDetail> OrderDetails { get; set; }

        public AppUser Customer { get; set; }

        public Order()
        {
            if (OrderDetails == null)
            {
                OrderDetails = new List<OrderDetail>();
            }
        }
    }
}