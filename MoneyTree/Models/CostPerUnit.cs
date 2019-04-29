using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrree.Models {
    public class CostPerUnit {

        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int CostItemId { get; set; }

        public CostItem CostItem { get; set; }

    }
}
