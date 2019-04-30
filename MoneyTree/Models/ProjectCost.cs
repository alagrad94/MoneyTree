using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTree.Models {
    public class ProjectCost {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostItemId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Date Used")]
        public DateTime DateUsed { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Project Name")]
        public Project Project { get; set; }

        [Display(Name = "Cost Item")]
        public CostItem CostItem { get; set; }
    }
}
