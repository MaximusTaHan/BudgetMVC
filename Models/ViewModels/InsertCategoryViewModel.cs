namespace BudgetMVC.Models.ViewModels
{
    public class InsertCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
