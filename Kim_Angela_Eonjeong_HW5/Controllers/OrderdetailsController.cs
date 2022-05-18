using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Kim_Angela_Eonjeong_HW5.DAL;
using Kim_Angela_Eonjeong_HW5.Models;

namespace Kim_Angela_Eonjeong_HW5.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orderdetails
        public IActionResult Index(int? orderID)
        {
            if (orderID == null)
            {
                return View("Error", new String[] { "Please specify a order to view!" });
            }

            //limit the list to only the registration details that belong to this registration
            List<OrderDetail> rds = _context.OrderDetails
                                          .Include(rd => rd.Product)
                                          .Where(rd => rd.Order.OrderID == orderID)
                                          .ToList();

            return View(rds);
        }


        // GET: Orderdetails/Create
        public IActionResult Create(int orderID)
        {
            OrderDetail rd = new OrderDetail();

            Order dbOrder = _context.Orders.Find(orderID);

            rd.Order = dbOrder;

            ViewBag.AllProducts = GetAllProducts();

            return View(rd);
        }


        // POST: Orderdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Order, OrderDetailID, NumberOfProduct")] OrderDetail orderDetail, int SelectedProduct)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.AllProducts = GetAllProducts();
                return View(orderDetail);
            }

            Product dbProduct = _context.Products.Find(SelectedProduct);

            orderDetail.Product = dbProduct;

            Order dbOrder = _context.Orders.Find(orderDetail.Order.OrderID);

            orderDetail.Order = dbOrder;
            orderDetail.ProductPrice = dbProduct.ProductPrice;
            orderDetail.ExtendedPrice = orderDetail.NumberOfProduct * orderDetail.ProductPrice;

            _context.Add(orderDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Orders", new { id = orderDetail.Order.OrderID });


        }

        // GET: Orderdetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a order detail to edit!" });
            }

            OrderDetail orderDetail = await _context.OrderDetails
                                                   .Include(rd => rd.Product)
                                                   .Include(rd => rd.Order)
                                                   .FirstOrDefaultAsync(rd => rd.OrderDetailID == id);
            if (orderDetail == null)
            {
                return View("Error", new String[] { "This order detail was not found" });
            }
            return View(orderDetail);
        }

        // POST: Orderdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailID,NumberOfProduct")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailID)
            {
                return View("Error", new String[] { "There was a problem editing this record. Try again!" });
            }


            if (ModelState.IsValid == false)
            {
                return View(orderDetail);
            }


     
            OrderDetail dbOD;

            try
            { 
                dbOD = _context.OrderDetails
                      .Include(rd => rd.Product)
                      .Include(rd => rd.Order)
                      .FirstOrDefault(rd => rd.OrderDetailID == orderDetail.OrderDetailID);

                dbOD.NumberOfProduct = orderDetail.NumberOfProduct;
                dbOD.ProductPrice = dbOD.Product.ProductPrice;
                dbOD.ExtendedPrice = dbOD.NumberOfProduct * dbOD.ProductPrice;

                //save changes
                _context.Update(dbOD);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was a problem editing this record", ex.Message });
            }

            return RedirectToAction("Details", "Orders", new { id = dbOD.Order.OrderID });
        }

        // GET: Orderdetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a order detail to delete!" });
            }

            OrderDetail orderDetail = await _context.OrderDetails
                .Include(r => r.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailID == id);

            if (orderDetail == null)
            {
                return View("Error", new String[] { "This order detail was not in the database!" });
            }

            return View(orderDetail);
        }

        // POST: Orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = await _context.OrderDetails
                                                   .Include(r => r.Order)
                                                   .FirstOrDefaultAsync(r => r.OrderDetailID == id);


            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Orders", new { id = orderDetail.Order.OrderID });
        }

        private SelectList GetAllProducts()
        {

            List<Product> allProducts = _context.Products.ToList();

           SelectList slAllProducts = new SelectList(allProducts, nameof(Product.ProductID), nameof(Product.ProductName));

            return slAllProducts;
        }
    }
}
