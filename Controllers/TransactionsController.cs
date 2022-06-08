using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetMVC.Models;
using BudgetMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(BudgetViewModel? model)
        {
            var transactions = await FilterTransactions(model);
            var categories = await _context.Categories.ToListAsync();

            var budgetViewModel = new BudgetViewModel
            {
                Transactions = transactions,
                Categories = new CategoriesViewModel { Categories = categories },
                InsertTransaction = new InsertTransactionViewModel { Categories = categories },
                FilterParameters = new FilterParametersViewModel { Categories = categories },
            };

            return View(budgetViewModel);
        }

        private async Task<List<Transaction>> FilterTransactions(BudgetViewModel? model)
        {
            var transactions = await _context.Transactions.ToListAsync();
            if (model.FilterParameters == null)
            {

            }

            else if (model.FilterParameters.CategoryId != 0 && model.FilterParameters.StartDate == null)
                transactions = transactions.Where(x => x.CategoryID == model.FilterParameters.CategoryId).ToList();

            else if (model.FilterParameters.CategoryId == 0 && model.FilterParameters.StartDate != null && model.FilterParameters.EndDate == null)
                transactions = transactions.Where(x => 
                x.Date >= model.FilterParameters.StartDate).ToList();

            else if (model.FilterParameters.CategoryId == 0 && model.FilterParameters.StartDate != null && model.FilterParameters.EndDate != null)
                transactions = transactions.Where(x =>
                x.Date >= model.FilterParameters.StartDate
                &&
                x.Date <= model.FilterParameters.EndDate).ToList();
            else if(model.FilterParameters.CategoryId != 0 &&
                model.FilterParameters.StartDate != null)
                transactions = transactions
                    .Where(x =>
                    x.Date >= model.FilterParameters.StartDate &&
                    x.CategoryID == model.FilterParameters.CategoryId).ToList();
            return transactions;
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetViewModel transaction)
        {

            Transaction newTransaction = new()
            {
                TransactionID = transaction.InsertTransaction.TransactionID,
                TransactionName = transaction.InsertTransaction.TransactionName,
                Amount = decimal.Parse(transaction.InsertTransaction.Amount),
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
