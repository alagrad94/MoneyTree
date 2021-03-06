﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {
    public class Project {

        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Complete")]
        public bool IsComplete { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CompletionDate { get; set; }

        [Display(Name = "Amount Charged")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double AmountCharged { get; set; }

        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ProjectCost> ProjectCosts { get; set; }

        public List<CustomProjectCost> CustomCosts { get; set; }

        public ApplicationUser User { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Total Cost")]
        public double TotalCost {
            get {
                double Total = 0.00;
                if (ProjectCosts != null) {

                    foreach (var item in ProjectCosts) {
                        Total += item.TotalCost;
                    }
                }

                if (CustomCosts != null) {

                    foreach (var item in CustomCosts) {
                        Total += item.TotalCost;
                    }
                }

                return Total;
            }
        }

        [Display(Name = "Profit")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Profit {
            get {
                return AmountCharged - TotalCost;
            }
        }
    }
}
