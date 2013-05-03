using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class SaleToAprove : IEntity
    {
        public virtual long Id { get; set; }

        [Required]
        public virtual long IdSale { get; set; }

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

        [Required]
        public virtual Account Account { get; set; }

        [DataType(DataType.ImageUrl)]
        public virtual string Picture { get; set; }

        public virtual bool Status { get; set; }
    }
}
