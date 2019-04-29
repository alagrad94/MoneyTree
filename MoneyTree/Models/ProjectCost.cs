using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrree.Models {
    public class ProjectCost {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostItemId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public DateTime DateUsed { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Project Project { get; set; }

        public CostItem CostItem { get; set; }
    }
}
