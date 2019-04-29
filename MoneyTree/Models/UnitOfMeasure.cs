using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {
    public class UnitOfMeasure {

        [Required]
        public int Id { get; set; }

        [Required]
        public string UnitName { get; set; }
    }
}