using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyTree.Data;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTree.Models.ViewModels {

    public class ProjectCostCreateViewModel {

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public List<ProjectCost> Costs { get; set; }

        public List<ProjectCost> UpdatedRecords { get; set; }

        public List<ProjectCost> RejectedEntries { get; set; }

        public List<CostItem> CostItems { get; set; }

        public List<SelectListItem> ItemOptions {
            get {
                List<SelectListItem> ItemOptionsList = CostItems?.OrderBy(ci => ci.ItemName)
                    .Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.ItemName + "(" + c.UnitOfMeasure.UnitName + ")"
                }).ToList();

                ItemOptionsList?.Insert(0, new SelectListItem { Value = "", Text = "Select An Item", Selected = true });
                //ItemOptionsList?.Insert(0, new SelectListItem { Value = "", Text = ""});
                return ItemOptionsList;
            }
        }

    }
}
