using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category has to be atleast two characters long")]
        public string? Title { get; set; }

        public List<Transaction>? Transactions { get; set; }
    }
}
