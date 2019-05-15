using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTree.Models.ViewModels {

    public class ProjectCostCreateViewModel {

        public int ProjectId { get; set; }

        public List<ProjectCost> Costs { get; set; }

        public List<ProjectCost> UpdatedRecords { get; set; }

        public List<ProjectCost> RejectedEntries { get; set; }

        public List<CostItem> CostItems { get; set; }

        public List<SelectListItem> ItemOptions {
            get {
                List<SelectListItem> ItemOptionsList = CostItems?.Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.ItemName
                }).ToList();

                ItemOptionsList?.Insert(0, new SelectListItem { Value = "0", Text = "Select An Item", Selected = true });
                return ItemOptionsList;
            }
        }

    }
}
