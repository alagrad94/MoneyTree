using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTree.Models.ViewModels {

    public class CostItemIndexViewModel {

        public int CostCategoryId { get; set; }

        public List<CostCategory> CostCategories { get; set; }

        public List<CostItem> CategoryItems { get; set; }

        public List<SelectListItem> CategoryOptions {

            get {
                List<SelectListItem> CategoryOptionsList = CostCategories.Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

                CategoryOptionsList?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Category", Selected = true });

                return CategoryOptionsList;
            }
        }
    }
}
