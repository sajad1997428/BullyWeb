using Bullky.DataAccount.Repository;
using Microsoft.AspNetCore.Mvc;
using BullyBook.Models;
using System.Diagnostics;
using BullkyBook.DataAccount.Repository.IRepository;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BullyBook.Models.ViewModels;



namespace BullyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitofwork ,IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ojcate = _unitofwork.Product.GetAll(includeProperies:"Category").ToList();
           
            return View(ojcate);
        }
        public IActionResult Upsert(int? id)
        {
            
            // ViewBag.CategoryList = CategoryList;
            
            ProductVM productVM = new()
            {

            CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
            
                Product = new Product()
            }; 
            
            if(id==null|| id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product=_unitofwork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj ,IFormFile? file)
        {

            //if(obj.Name!=null && obj.Name.ToLower() == "text")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; // إصلاح الخطأ الإملائي
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filepath = Path.Combine(wwwRootPath, "images\\product", filename); // تحديد مجلد الحفظ

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // إصلاح التكرار
                    using (var filestream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    obj.Product.ImageUrl = @"\images\product\" + filename;
                }

                if (obj.Product.Id == 0)
                {
                    _unitofwork.Product.Add(obj.Product);
                }
                else
                {
                    _unitofwork.Product.Update(obj.Product);
                }

                _unitofwork.save();
                TempData["success"] = "Product CREATED successfully";
                return RedirectToAction("Index");
            }
            else
            {
                obj.CategoryList = _unitofwork.Category.GetAll(includeProperies:"Category").Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });

                return View(obj);
            }


        }
        public IActionResult Edit(int? id)
        {
           if(id == null||id==0)
            {
                return NotFound();
            }
           Product? productFrom=_unitofwork.Product.Get
                (x => x.Id == id);
            if(productFrom == null)
            {
                return NotFound();
            }
            return View(productFrom);

        }

        [HttpPost]
        public IActionResult Edit(Product Product)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Update(Product);

                _unitofwork.save();
                TempData["success"] = "Product Edit successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? Product = _unitofwork.Product.Get(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
        [HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int? id)

        //{
        //    Product? obj = _unitofwork.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }



        //    _unitofwork.Product.Remove(obj);
        //    _unitofwork.save();
        //    TempData["success"] = "Product Delete successfully";
        //    return RedirectToAction("Index");

        //}
        public IActionResult Delete(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Product.Remove(obj);
                _unitofwork.save();
                return RedirectToAction("Index");
            }
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objproductList = _unitofwork.Product.GetAll(includeProperies: "Category").ToList();
            return Json(new { data = objproductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)

        {
            var productToBeDelete = _unitofwork.Product.Get(u => u.Id == id);
            if (productToBeDelete == null)
            {
                return Json(new { success = false, masseage = "Error while deleting" });
            }
            var oldImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDelete.ImageUrl.TrimStart('\\'));
            if (System.IO.Path.Exists(oldImagesPath))
            {
                System.IO.File.Delete(oldImagesPath);
            }
            _unitofwork.Product.Remove(productToBeDelete);
            _unitofwork.save();

            return Json(new { success = true, masseage = "Delete Successful" });

        }
        #endregion
    }
}
