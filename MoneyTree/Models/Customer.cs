using System.ComponentModel.DataAnnotations;

namespace MoneyTrree.Models {
    public class Customer {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhohneNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}