using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {

    public class CustomEstimateCost {

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item")]
        public string ItemName { get; set; }

        [Required]
        public int EstimateId { get; set; }

        [Required]
        public Estimate Estimate { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Units")]
        public string UnitOfMeasure { get; set; }

        [Required]
        [Display(Name = "Cost Per Unit")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double CostPerUnit { get; set; }

        [Display(Name = "Markup %")]
        public double MarkupPercent { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Total Cost")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalCost {
            get {
                return Quantity * CostPerUnit * (1 + MarkupPercent);
            }
        }
    }
}
