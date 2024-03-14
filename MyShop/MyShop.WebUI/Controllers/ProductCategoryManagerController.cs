using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productCategoryContext;
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }


        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    context.Insert(productCategory);
                    context.Commit();

                    return RedirectToAction("Index");
                }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = context.Find(id);

            if(productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            ProductCategory productCategoryToEdit = context.Find(productCategory.Id);

            if(productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    productCategoryToEdit.Category = productCategory.Category;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }


        public ActionResult Delete(string id)
        {
            ProductCategory productCategory = context.Find(id);

            if(productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategory = context.Find(id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}