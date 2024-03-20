using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productContext;
            this.productCategories = productCategoryContext;
        }

        public ActionResult Details(string id)
        {
            Product product = context.Find(id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Index(string category=null)
        {

            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
             
            if(category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == category).ToList();
            }

            ProductListViewModel viewModel = new ProductListViewModel();
            viewModel.Products = products;
            viewModel.ProductCategories = categories;

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}