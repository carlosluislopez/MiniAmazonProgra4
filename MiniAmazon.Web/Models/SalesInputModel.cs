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
        [DataType(DataType.DateTime)]
        public virtual DateTime CreateDateTime { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Title { get; set; }

        [StringLength(250)]
        public virtual string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public virtual decimal Price { get; set; }

        [Required]
        public virtual Category Category { get; set; }
    }
}