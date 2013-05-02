using System;
using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.Domain.Entities
{
    public class Sale : IEntity
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