using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kim_Angela_Eonjeong_HW5.Models
{
    public class OrderDetail
    {
        public Int32 OrderDetailID { get; set; }

        [Required(ErrorMessage = "You must specify the quantity of products to order")]
        [Display(Name = "Number of products to order")]
        [Range(1, 1000, ErrorMessage = "Number of products must be between 1 and 1000")]
        public Int32 NumberOfProduct { get; set; }

        [Display(Name = "Product Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ProductPrice { get; set; }

        [Display(Name = "Extended Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ExtendedPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}