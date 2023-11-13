using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T2210M_MVC.Entities;
using T2210M_MVC.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2210M_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> ls = _context.Categories.ToList();
            return View(ls);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                // save to db
                _context.Categories.Add(new Category { Name = model.Name });
                _context.SaveChanges();

                // redirect to list
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

