using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyTree.Data;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTree.Models.ViewModels {

    public class CustomCostCreateViewModel {

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public List<CustomProjectCost> CustomCosts { get; set; }

        public List<CustomProjectCost> UpdatedRecords { get; set; }

        public List<CustomProjectCost> RejectedEntries { get; set; }
    }
}
