using System.ComponentModel.DataAnnotations;

namespace MoneyTrree.Models {
    public class CostItem {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int UnitOfMeasureId { get; set; }

        [Required]
        public int CostCategoryId { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; }

        public CostCategory CostCategory { get; set; }
    }
}