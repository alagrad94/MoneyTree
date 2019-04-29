using System.ComponentModel.DataAnnotations;

namespace MoneyTrree.Models {
    public class ApplicationUser {

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }
    }
}