using System;
using Final_assignment.Base;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_assignment.Models;
using Final_assignment.Repository;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using PagedList;
using PagedList.Mvc;
using System.Drawing.Printing;

namespace Final_assignment.Controllers
{
    public class ProductController : Controller
    {
        private IFoodRepository<Product> foodRepository;
        private IFoodRepository<Category> _foodRepository;

//////////////////////////////////////////////////////////////////////////////////////////

        public ProductController(FoodRepository<Category> _foodRepository, FoodRepository<Product> foodRepository)
        {
            this.foodRepository = foodRepository;
            this._foodRepository = _foodRepository;
        }


 //////////////////////////////////////////////////////////////////////////////////////////

        //creating and updating
        [HttpPost]
        public HttpStatusCode Create(Product product)
        {
            var Categoryd = foodRepository.search(x => x.Name == product.Category.Name);
            //foodRepository.GetAll().Where(x => x.CategoryId == product.CategoryId);

            //var CategoryI = Categoryd.FirstOrDefault().Id;
            if (product.Id == 0)
            {
                var AddProduct = new Product();
                AddProduct.Name = product.Name;
                AddProduct.Id = product.Id;
                //AddProduct.Category = product.Category;
                AddProduct.Expiry = product.Expiry;
                AddProduct.CreatedDate = DateTime.Now;
                AddProduct.IsDeleted = false;
                AddProduct.CategoryId=product.CategoryId;
                foodRepository.Insert(AddProduct);
                return HttpStatusCode.OK;
            }
            else{
                Product productD = foodRepository.GetById(product.Id);
                productD.Name = product.Name;
                productD.Category = product.Category;
                productD.Expiry = product.Expiry;
                productD.CategoryId = product.CategoryId;
                productD.UpdatedDate = DateTime.Now;
                UpdateModel(productD, "product");
                foodRepository.Update(productD);
            }
            return HttpStatusCode.InternalServerError;
        }

//////////////////////////////////////////////////////////////////////////////////////////

        //Editing data

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var ProductData = foodRepository.GetById(id);
            var product = new Product();
            product.Id = ProductData.Id;
            product.Name = ProductData.Name;
            product.Expiry = product.Expiry;


            return Json(product, JsonRequestBehavior.AllowGet);

        }

 //////////////////////////////////////////////////////////////////////////////////////////       
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = foodRepository.GetById(id);
            product.IsDeleted = true;
            foodRepository.Save();
            return Content("ok");
        }

 //////////////////////////////////////////////////////////////////////////////////////////
     
        //Index
        public ActionResult Index(string sortOrder, string searchString, int? page, int? PageSize)
        {
            
            IQueryable<Product> productD = foodRepository.search(c => c.IsDeleted != true); ;
            if (!String.IsNullOrEmpty(searchString))
            {
                productD = foodRepository.search(c => c.Name.ToLower().Contains(searchString.ToLower()) && c.IsDeleted != true);
                //CategoryD = CategoryD.Where(s => s.Name.Contains(searchString) || s.IsDeleted != true);
            }
            else
            {
                productD = foodRepository.search(c => c.IsDeleted != true);
            }


            var CategoryList = _foodRepository.search(c => c.IsDeleted != true).ToList();
            ViewBag.ListCat = CategoryList;
            var Products = foodRepository.search(c => c.IsDeleted != true).ToList();

            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortByDate = sortOrder == "Date" ? "date_desc" : "Date";
          

            switch (sortOrder)
            {
                case "name_desc":
                    productD = productD.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    productD = productD.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    productD = productD.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    productD = productD.OrderBy(s => s.Name);
                    break;
            }
            //var ProductData = productD.ToPagedList(page ?? 1, 5);
            ////ViewBag.Product = foodRepository.GetAll().Where(c => c.IsDeleted != true).ToList();
            //ViewBag.Product = productD.ToPagedList(page ?? 1, 5);

            //return View(ProductData);
            FoodStoreContext _context = new FoodStoreContext();

            int count = _context.products.OrderBy(e => e.Id).Count();

            ViewBag.PageSize = new List<SelectListItem>() {
                   new SelectListItem { Text = "5", Selected = true },
                   new SelectListItem { Text = "10", Value = "10" },
                   new SelectListItem { Text = "20", Value = "20" },
                   new SelectListItem { Text = "30", Value = "30" },
                   new SelectListItem { Text = "All", Value = count.ToString() }
                   };

            int pageNumber = (page ?? 1);
            int pageSize = (PageSize ?? 5);
            ViewBag.psize = pageSize;
            return View(productD.ToPagedList(pageNumber, pageSize));

        }

    }
}
