using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.Web.Models
{
    public class AccountInputModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Range(1,130)]
        public int Age { get; set; }

        [StringLength(1)]
        public string Genre { get; set; }
    }
}