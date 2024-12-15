using Bullky.DataAccount.Repository;
using Microsoft.AspNetCore.Mvc;
using BullyBook.Models;
using System.Diagnostics;
using BullkyBook.DataAccount.Repository.IRepository;


namespace BullyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Category> ojcate = _unitofwork.Category.GetAll().ToList();
            return View(ojcate);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "the DisplayOrder cannot exactly match the Name");
            }
            //if(obj.Name!=null && obj.Name.ToLower() == "text")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.save();
                TempData["success"] = "Category CREATE successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _unitofwork.Category.Get(u => u.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(category);

                _unitofwork.save();
                TempData["success"] = "Category Edit successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitofwork.Category.Get(u => u.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteP(int? id)
        {
            Category? obj = _unitofwork.Category.Get(u => u.CategoryId== id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.Category.Remove(obj);
            _unitofwork.save();
            TempData["success"] = "Category Delete successfully";
            return RedirectToAction("Index");

        }
        //public IActionResult Delete(Category obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Remove(obj);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}'


    }
}
