using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {
    public class CostCategory {

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Markup")]
        public double MarkupPercent { get; set; }
    }
}