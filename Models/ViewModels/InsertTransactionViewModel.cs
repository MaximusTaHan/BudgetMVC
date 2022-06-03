using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMVC.Models.ViewModels
{
    public class InsertTransactionViewModel
    {
        public int TransactionID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Transaction Name")]
        public string TransactionName { get; set; }

        [Required]
        public string Amount { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
