using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrree.Models {
    public class Project {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public int AmountCharged { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public int UserId { get; set; }

        public Customer Customer { get; set; }

        public ApplicationUser User { get; set; }
    }
}
