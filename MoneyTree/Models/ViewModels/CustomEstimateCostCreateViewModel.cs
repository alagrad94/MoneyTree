using System.Collections.Generic;

namespace MoneyTree.Models.ViewModels {

    public class CustomEstimateCostCreateViewModel {

        public int EstimateId { get; set; }

        public Estimate Estimate { get; set; }

        public List<CustomEstimateCost> CustomCosts { get; set; }

        public List<CustomEstimateCost> UpdatedRecords { get; set; }

        public List<CustomEstimateCost> RejectedEntries { get; set; }
    }
}
