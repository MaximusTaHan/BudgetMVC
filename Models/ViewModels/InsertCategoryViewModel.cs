using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMVC.Models.ViewModels
{
    public class InsertCategoryViewModel
    {
        public int CategoryId { get; set; }


        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category has to be atleast two characters long")]
        [Remote("IsUnique", "Categories")]
        [Required]
        public string CategoryName { get; set; }
    }
}
