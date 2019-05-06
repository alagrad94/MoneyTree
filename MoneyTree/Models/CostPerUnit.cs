using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {

    public class CostPerUnit {

        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Cost { get; set; }

        [Required]
        public int CostItemId { get; set; }

        public CostItem CostItem { get; set; }

    }
}
