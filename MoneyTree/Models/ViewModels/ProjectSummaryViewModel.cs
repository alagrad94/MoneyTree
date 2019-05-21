using System;
using System.Collections.Generic;

namespace MoneyTree.Models.ViewModels {

    public class ProjectSummaryViewModel {

        public Project Project { get; set; }

        public List<TotaledCost> TotaledProjectCosts { get; set; }

        public List<TotaledCost> TotaledCustomCosts { get; set; }
    }
}
