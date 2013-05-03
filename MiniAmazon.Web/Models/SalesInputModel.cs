using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MiniAmazon.Domain.Entities;

namespace MiniAmazon.Web.Models
{
    public class SalesInputModel
    {
        public virtual long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int IdCategory { get; set; }

        [Required]
        public int IdAccount { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }
    }
}