using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetMVC.Models;
using BudgetMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BudgetMVC.Controllers
{
    
    public class CategoriesController : Controller
    {
        private readonly TransactionContext _context;

        public CategoriesController(TransactionContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Transactions");
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetViewModel category)
        {
            Category newCategory = new()
            {
                CategoryId = category.InsertCategory.CategoryId,
                Title = category.InsertCategory.CategoryName,
            };

            if(category.InsertCategory.CategoryId > 0)
            {
                var test = await _context.Categories.FindAsync(category.InsertCategory.CategoryId);
                test.Title = category.InsertCategory.CategoryName;

                if (ModelState.IsValid)
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", test.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                _context.Add(newCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult IsUnique([Bind(Prefix = "InsertCategory.CategoryName")] string name)
        {
            var categories = _context.Categories;

            if (categories.Any(x => x.Title == name))
                return Json("Category already exists");

            return Json(true);

        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'TransactionContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
