using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models
{

    public class ProjectCost {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostItemId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int CostPerUnitId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Used")]
        public DateTime DateUsed { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Project Name")]
        public Project Project { get; set; }

        [Display(Name = "Cost Item")]
        public CostItem CostItem { get; set; }

        public CostPerUnit CostPerUnit { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double TotalCost {
            get {
                return Quantity * (CostPerUnit?.Cost??0);
            }
        }
    }
}
