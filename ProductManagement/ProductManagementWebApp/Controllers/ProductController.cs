using ProductManagementWebApp.Models;
using ProductManagementWebApp.Models.DAL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementWebApp.Controllers
{
    public class ProductController : Controller
    {
        ProductService _productService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            List<ProductViewModel> products = _productService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var product = _productService.GetProduct(id);
            return PartialView(product);
        }
        [HttpPost]

        public ActionResult Update(ProductViewModel product)
        {
            _productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(ProductViewModel product)
        {
            _productService.AddProduct(product);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _productService.DeleteProduct(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}