using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMVC.Models.ViewModels
{
    public class InsertTransactionViewModel
    {
        public int TransactionID { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10)")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
