using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {

    public class TotaledCost {

        public string ItemName { get; set; }

        public string Category { get; set; }

        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalCost { get; set; }
    }
}
