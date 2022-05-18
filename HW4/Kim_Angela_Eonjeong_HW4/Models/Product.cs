using System;
using System.ComponentModel.DataAnnotations;

namespace Kim_Angela_Eonjeong_HW4.Models
{
    public enum ProductType { 
        [Display(Name = "hot")] hot,
        [Display(Name = "cold")] cold,
        [Display(Name = "packaged")] packaged,
        [Display(Name = "drink")] drink,
        [Display(Name = "other")] other }

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

    }
}
