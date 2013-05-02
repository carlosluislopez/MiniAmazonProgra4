using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.Web.Models
{
    public class CategoryInputModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

    }
}