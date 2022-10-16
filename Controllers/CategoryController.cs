using Final_assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_assignment.Repository;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace Final_assignment.Controllers
{
    public class CategoryController : Controller
    {

        private IFoodRepository<Category> _foodRepository;
        private IFoodRepository<Product> foodRepository;


//////////////////////////////////////////////////////////////////////////////////////////

       //Constructor
        public CategoryController(FoodRepository<Category> _foodRepository, FoodRepository<Product> foodRepository)
        {
            this._foodRepository = _foodRepository;
            this.foodRepository = foodRepository;
        }

//////////////////////////////////////////////////////////////////////////////////////////
       
        //INDEX
        public ActionResult Index(string sortOrder, string searchString, int? page, int? PageSize)
        {
            // ViewBag.CurrentSort = sortOrder;
            FoodStoreContext _context = new FoodStoreContext();



            IQueryable<Category> CategoryD = _foodRepository.search(c => c.IsDeleted != true); 
            if (!String.IsNullOrEmpty(searchString))
            {
                CategoryD=_foodRepository.search(c=>c.Name.ToLower().Contains(searchString.ToLower()) && c.IsDeleted != true);
                //CategoryD = CategoryD.Where(s => s.Name.Contains(searchString) || s.IsDeleted != true);
            }
            else
            {
                CategoryD = _foodRepository.search(c => c.IsDeleted != true);
            }
                        ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    ViewBag.SortByDate = sortOrder == "Date" ? "date_desc" : "Date";
                    switch (sortOrder)
                    {
                        case "name_desc":
                            CategoryD = CategoryD.OrderByDescending(s => s.Name);
                            break;
                        case "Date":
                            CategoryD = CategoryD.OrderBy(s => s.CreatedDate);
                            break;
                        case "date_desc":
                            CategoryD = CategoryD.OrderByDescending(s => s.CreatedDate);
                            break;
                        default:
                            CategoryD = CategoryD.OrderBy(s => s.Name);
                            break;
}
            //    var CategoryData = CategoryD.ToPagedList(page ?? 1, 5);
            //    ViewBag.Category = CategoryD.ToPagedList(page ?? 1, 5);
            //    return View(CategoryData);
            // //   return View(CatPList);

            //}


               int count = _context.categories.OrderBy(e => e.Id).Count(); // jitne bhi elements h saare
            
            // dropdown m data daal rahe h
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
            return View(CategoryD.ToPagedList(pageNumber, pageSize));
            }




//////////////////////////////////////////////////////////////////////////////////////////


        //CREATE
        [HttpPost]
        public HttpStatusCode Create(string name, int id = 0)
        {

            if (id == 0)
            {

                var AddCatagory = new Category();
                AddCatagory.Name = name;
                AddCatagory.CreatedDate = DateTime.Now;
                AddCatagory.IsDeleted = false;

                _foodRepository.Insert(AddCatagory);
                return HttpStatusCode.OK;
            }
            else
            {
                Category CategoryDB = _foodRepository.GetById(id);
                CategoryDB.Name = name;
                CategoryDB.UpdatedDate = DateTime.Now;
                UpdateModel(CategoryDB, "Catagory");
                _foodRepository.Update(CategoryDB);
                return HttpStatusCode.OK;
            }
        }

 //////////////////////////////////////////////////////////////////////////////////////////

        //Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var getCategory = _foodRepository.GetById(id);
            var category = new Category();
            category.Id = getCategory.Id;
            category.Name = getCategory.Name;
            category.UpdatedDate = DateTime.Now;
            category.CreatedDate = getCategory.CreatedDate;
            return Json(category, JsonRequestBehavior.AllowGet);
        }

 //////////////////////////////////////////////////////////////////////////////////////////
       
        //Details

        [HttpGet]
        public ActionResult Details(int id)
        {
            var getCategory = _foodRepository.GetById(id);
            var category = new Category();
            category.Id = getCategory.Id;
            category.Name = getCategory.Name;
            category.UpdatedDate = DateTime.Now;
            category.CreatedDate = getCategory.CreatedDate;
            return Json(category, JsonRequestBehavior.AllowGet);
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        
        //Deleting
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category category = _foodRepository.GetById(id);
            var list = foodRepository.search(x => x.Category.Id == id);
            ViewBag.DeleteList = String.Join(" ", list.ToList().Select(x=>x.Name));
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _foodRepository.GetById(id);
            category.IsDeleted = true;
             //   _foodRepository.Update(category);
            var list = foodRepository.search(x =>x.IsDeleted==false&& x.Category.Id == id);
            foreach (var item in list)
            {
                item.IsDeleted = true;
                //foodRepository.Update(item);
            }
                foodRepository.Save();
            _foodRepository.Save();
            return RedirectToAction("Index");
           
        }
    }
}




























