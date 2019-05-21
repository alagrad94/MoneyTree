using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {

    public class CustomProjectCost {

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item")]
        public string ItemName { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }

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

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Date Used")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateUsed { get; set; }

        [Display(Name = "Total Cost")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalCost {
            get {
                return Quantity * CostPerUnit;
            }
        }
    }
}
