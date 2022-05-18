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
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
