using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTree.Models.ViewModels
{

    public class EstimateCostCreateViewModel {

        public int EstimateId { get; set; }

        public Estimate Estimate { get; set; }

        public List<EstimateCost> Costs { get; set; }

        public List<EstimateCost> UpdatedRecords { get; set; }

        public List<EstimateCost> RejectedEntries { get; set; }

        public List<CostItem> CostItems { get; set; }

        public List<SelectListItem> ItemOptions {
            get {
                List<SelectListItem> ItemOptionsList = CostItems?.OrderBy(ci => ci.ItemName)
                    .Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.ItemName + "(" + c.UnitOfMeasure.UnitName + ")"
                }).ToList();

                ItemOptionsList?.Insert(0, new SelectListItem { Value = "", Text = "Select An Item", Selected = true });
                return ItemOptionsList;
            }
        }

    }
}
