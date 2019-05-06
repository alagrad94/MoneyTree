using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyTree.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTree.Models.ViewModels {

    public class DeleteCategoryViewModel {

        public int OldCategoryId { get; set; }

        public int NewCategoryId { get; set; }

        public CostCategory CostCategory { get; set; }

        public List<CostCategory> CostCategories { get; set; }

        public List<SelectListItem> CategoryOptions {
            get {
                List<SelectListItem> CategoryOptions = CostCategories?.Where(c => c.Id != OldCategoryId)
                .Select(c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

                CategoryOptions?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Category", Selected = true });
                return CategoryOptions;
            }
        }
    }
}
