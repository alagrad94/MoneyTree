using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MoneyTree.Data;

namespace MoneyTree.Models {

    public class Estimate {

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Title")]
        public string ProjectTitle { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [Display(Name = "Estimate Preparation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EstimateDate { get; set; }

        [Required]
        [Display(Name = "Estimate Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpirationDate { get; set; }

        public List<EstimateCost> EstimateCosts { get; set; }

        [NotMapped]
        public List<CostCategory> Categories { get; set; }

        public List<CustomEstimateCost> CustomCosts { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Total Cost")]
        public double TotalCost {
            get {
                double Cost = 0;
                foreach (var cost in EstimateCosts) {
                    Cost += cost.Total;
                }

                if (CustomCosts != null) {

                    foreach (var item in CustomCosts) {
                        Cost += item.TotalCost;
                    }
                }
                return Cost;
            }
        }
    }
}
