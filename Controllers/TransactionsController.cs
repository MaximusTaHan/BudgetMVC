using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetMVC.Models;
using BudgetMVC.Models.ViewModels;

namespace BudgetMVC.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionContext _context;

        public TransactionsController(TransactionContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.ToListAsync();
            var categories = await _context.Categories.ToListAsync();

            var budgetViewModel = new BudgetViewModel
            {
                Transactions = transactions,
                Categories = new CategoriesViewModel { Categories = categories },
                InsertTransaction = new InsertTransactionViewModel { Categories = categories }
            };

            ModelState.Clear();

            return View(budgetViewModel);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetViewModel transaction)
        {

            Transaction newTransaction = new()
            {
                TransactionID = transaction.InsertTransaction.TransactionID,
                TransactionName = transaction.InsertTransaction.TransactionName,
                Amount = transaction.InsertTransaction.Amount,
                CategoryID = transaction.InsertTransaction.CategoryID,
                Date = transaction.InsertTransaction.Date,
            };

            if (newTransaction.TransactionID > 0)
            {
                if(ModelState.IsValid)
                {
                    _context.Update(newTransaction);
                    await _context.SaveChangesAsync();
                }
                ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newTransaction.CategoryID);
                return RedirectToAction(nameof(Index));
            }

            else if (ModelState.IsValid)
            {
                _context.Add(newTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newTransaction.CategoryID);
            return RedirectToAction("Index");
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", transaction.CategoryID);
            return RedirectToAction("Index");
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'TransactionContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return (_context.Transactions?.Any(e => e.TransactionID == id)).GetValueOrDefault();
        }
    }
}
