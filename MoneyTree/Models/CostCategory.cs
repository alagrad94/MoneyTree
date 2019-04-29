using System.ComponentModel.DataAnnotations;

namespace MoneyTrree.Models {
    public class CostCategory {

        [Required]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}