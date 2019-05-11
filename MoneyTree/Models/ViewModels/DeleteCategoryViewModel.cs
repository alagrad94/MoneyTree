using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTree.Models.ViewModels
{

    public class DeleteCategoryViewModel {

        public int OldCategoryId { get; set; }

        public int NewCategoryId { get; set; }

        public CostCategory CostCategory { get; set; }

        public List<CostCategory> CostCategories { get; set; }

        public List<SelectListItem> CategoryOptions {
            get {
                List<SelectListItem> CategoryOptionsList = CostCategories?.Where(c => c.Id != OldCategoryId)
                .Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

                CategoryOptionsList?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Category", Selected = true });
                return CategoryOptionsList;
            }
        }
    }
}
