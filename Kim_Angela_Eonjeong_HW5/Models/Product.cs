using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kim_Angela_Eonjeong_HW5.Models
{

    public enum ProductType
    {
        [Display(Name = "hat")] hat,
        [Display(Name = "pants")] pants,
        [Display(Name = "sweatshirt")] sweatshirt,
        [Display(Name = "tank")] tank,
        [Display(Name = "t-shirt")] tshirt,
        [Display(Name = "other")] other


    }

    public class Product
    {
        [Display(Name = "Product ID")]
        public Int32 ProductID { get; set; }

        // billno = billno + 1;

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Product name is required")]
        public String ProductName { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product price is required")]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public Decimal ProductPrice { get; set; }

        [Display(Name = "Product Type")]
        public ProductType ProductType { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            if (Suppliers == null)
            {
                Suppliers = new List<Supplier>();
            }

            if (OrderDetails == null)
            {
                OrderDetails = new List<OrderDetail>();
            }

        }
    
    }
};