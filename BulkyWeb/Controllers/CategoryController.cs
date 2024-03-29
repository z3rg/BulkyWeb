﻿using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "Name Cannot be the same with Display Order.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Data has been created";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0) { return NotFound(); }
            Category? categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb== null) { return NotFound(); }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Data has been Updated";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? obj = _db.Categories.Find(id);
            if(obj== null) { return NotFound(); }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Data has been Deleted";
            return RedirectToAction("Index", "Category");
        }
    }
}
