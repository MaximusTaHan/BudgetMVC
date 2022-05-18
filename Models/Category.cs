namespace BudgetMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Title { get; set; }

        public List<Transaction>? Transactions { get; set; }
    }
}
