using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MoneyTree.Data;

namespace MoneyTree.Models {

    public class EstimateCost {

        [Required]
        public int Id { get; set; }

        [Required]
        public int EstimateId { get; set; }

        public Estimate Estimate { get; set; }

        [Required]
        public int CostItemId { get; set; }

        public CostItem CostItem { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double CostPerUnit { get; set; }

        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Total {
            get {
                return Quantity * CostPerUnit * (1 + CostItem?.CostCategory.MarkupPercent??0);
            }
        }
    }
}
