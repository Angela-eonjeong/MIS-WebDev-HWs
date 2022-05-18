using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kim_Angela_Eonjeong_HW5.Models
{
    public class Supplier
    {
        [Key]
        public Int32 SupplierID { get; set; }

        [Display(Name = "Supplier Name")]
        public String SupplierName { get; set; }

        [Display(Name = "Supplier Email")]
        public String SupplierEmail { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public String SupplierPhoneNumber { get; set; }

        public List<Product> Products { get; set; }

        public Supplier()
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
        }

    }
}