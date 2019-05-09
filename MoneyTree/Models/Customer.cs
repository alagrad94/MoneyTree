using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyTree.Models {
    public class Customer {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Customer Name")]
        public string FullName {
            get {
                return $"{FirstName} {LastName}";
            }
        }
    }
}