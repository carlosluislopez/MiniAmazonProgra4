using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.Web.Models
{
    public class AccountForgotPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}