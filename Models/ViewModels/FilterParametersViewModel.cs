using System.ComponentModel;

namespace BudgetMVC.Models.ViewModels
{
    public class FilterParametersViewModel
    {
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        public List<Category>? Categories { get; set; }

    }
}
