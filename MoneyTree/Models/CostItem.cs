using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models
{

    public class CostItem {

        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        public int UnitOfMeasureId { get; set; }

        [Required]
        public int CostCategoryId { get; set; }

        [Display(Name = "Unit of Measure")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [Display(Name = "Cost Category")]
        public CostCategory CostCategory { get; set; }
    }
}