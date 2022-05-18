using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kim_Angela_Eonjeong_HW5.DAL;
using Kim_Angela_Eonjeong_HW5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Kim_Angela_Eonjeong_HW5.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Include(c => c.Suppliers)
                .ToListAsync());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a product to view! " });

            }

            Product product = await _context.Products
                .Include(c => c.Suppliers)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return View("Error", new String[] { "This product was not found!" });
            }

            return View(product);
        }

        // GET: Products/Create

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.AllSuppliers = GetAllSuppliers();

            return View();
        }

       

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Description,ProductPrice,ProductType")] Product product, int[] SelectedSuppliers)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.AllSuppliers = GetAllSuppliers();
                //go back to the Create view to try again
                return View(product);
            }


            _context.Add(product);
            await _context.SaveChangesAsync();

            foreach (int supplierID in SelectedSuppliers)
            {
                //find the department associated with that id
                Supplier dbSupplier = _context.Suppliers.Find(supplierID);

                //add the department to the course's list of departments and save changes
                product.Suppliers.Add(dbSupplier);
                _context.SaveChanges();
            }

            //Send the user to the page with all the departments
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a product to edit! " });
            }

            Product product = await _context.Products.Include(c => c.Suppliers)
                                           .FirstOrDefaultAsync(c => c.ProductID == id);


            if (product == null)
            {
                return View("Error", new String[] { "This product was not found!" });
            }

            ViewBag.AllSuppliers = GetAllSuppliers(product);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("ProductID,ProductName,Description,ProductPrice,ProductType")] Product product, int[] SelectedSuppliers)
        {
            if (id != product.ProductID)
            {
                return View("Error", new String[] { "Please try again!" });

            }


            if (ModelState.IsValid == false) //there is something wrong
            {
                ViewBag.AllSuppliers = GetAllSuppliers(product);
                return View(product);
            }

            
            try
            {
                
                Product dbProduct = _context.Products
                    .Include(c => c.Suppliers)
                    .FirstOrDefault(c => c.ProductID == product.ProductID);

                //create a list of departments that need to be removed
                List<Supplier> SuppliersToRemove = new List<Supplier>();

                //find the departments that should no longer be selected because the
                //user removed them
                //remember, SelectedDepartments = the list from the HTTP request (listbox)
                foreach (Supplier supplier in dbProduct.Suppliers)
                {
                    //see if the new list contains the department id from the old list
                    if (SelectedSuppliers.Contains(supplier.SupplierID) == false)//this department is not on the new list
                    {
                        SuppliersToRemove.Add(supplier);
                    }
                }

                
                foreach (Supplier supplier in SuppliersToRemove)
                {
                    //remove this course department from the course's list of departments
                    dbProduct.Suppliers.Remove(supplier);
                    _context.SaveChanges();
                }

                //add the departments that aren't already there
                foreach (int supplierID in SelectedSuppliers)
                {
                    if (dbProduct.Suppliers.Any(d => d.SupplierID == supplierID) == false)//this department is NOT already associated with this course
                    {
                        //Find the associated department in the database
                        Supplier dbSupplier = _context.Suppliers.Find(supplierID);

                        //Add the department to the course's list of departments
                        dbProduct.Suppliers.Add(dbSupplier);
                        _context.SaveChanges();
                    }
                }

                //update the course's scalar properties
                
                dbProduct.ProductName = product.ProductName;
                dbProduct.Description = product.Description;
                dbProduct.ProductPrice = product.ProductPrice;
                dbProduct.ProductType = product.ProductType;

                //save the changes
                _context.Products.Update(dbProduct);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return View("Error", new string[] { "There was an error editing this Product.", ex.Message });
            }

            //if code gets this far, everything is okay
            //send the user back to the page with all the courses
            return RedirectToAction(nameof(Index));
        }


        private MultiSelectList GetAllSuppliers()
        {
            //Get the list of months from the database
            List<Supplier> supplierList = _context.Suppliers.ToList();
            //convert the list to a SelectList by calling SelectList constructor
            //MonthID and MonthName are the names of the properties on the Month class
            //MonthID is the primary key
            MultiSelectList supplierSelectList = new MultiSelectList(supplierList.OrderBy(m => m.SupplierName), "SupplierID", "SupplierName");

            //return the MultiSelectList
            return supplierSelectList;
        }

        private MultiSelectList GetAllSuppliers(Product product)
        {
            //Create a new list of departments and get the list of the departments
            //from the database
            List<Supplier> supplierList = _context.Suppliers.ToList();

            //loop through the list of course departments to find a list of department ids
            //create a list to store the department ids
            List<Int32> selectedSupplierIDs = new List<Int32>();

            //Loop through the list to find the DepartmentIDs
            foreach (Supplier associatedSupplier in product.Suppliers)
            {
                selectedSupplierIDs.Add(associatedSupplier.SupplierID);
            }

            //use the MultiSelectList constructor method to get a new MultiSelectList
            MultiSelectList supplierSelectList = new MultiSelectList(supplierList.OrderBy(d => d.SupplierName), "SupplierID", "SupplierName", selectedSupplierIDs);

            //return the MultiSelectList
            return supplierSelectList;
        }

    }
}
