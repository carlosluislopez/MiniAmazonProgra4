using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.Web.Models
{
    public class AccountResetInModel
    {
        public long Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordNew { get; set; }
    }
}