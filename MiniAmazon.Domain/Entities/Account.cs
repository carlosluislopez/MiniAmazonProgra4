using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
//using System.Web.Mvc;

namespace MiniAmazon.Domain.Entities
{
    public class Account : IEntity
    {
        private readonly IList<Sale> _sales = new List<Sale>();

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        //[Remote("CheckIfExist", "Account")]
        public virtual string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Range(1, 130)]
        public virtual int Age { get; set; }

        [StringLength(1)]
        public virtual string Genre { get; set; }

        public virtual bool Status { get; set; }

        public virtual IEnumerable<Sale> Sales
        {
            get { return _sales; }
        }

        #region IEntity Members

        public virtual long Id { get; set; }

        #endregion

        public virtual void AddSale(Sale sale)
        {
            if (!_sales.Contains(sale))
            {
                _sales.Add(sale);
            }
        }
    }
}