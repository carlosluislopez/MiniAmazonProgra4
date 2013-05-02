using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MiniAmazon.Domain.Entities
{
    public class Category : IEntity
    {
        #region IEntity Members

        public virtual long Id { get; set; }

        #endregion

        [Required]
        [StringLength(25)]
        public virtual string Name { get; set; }

        public virtual bool Status { get; set; }

        private readonly IList<Sale> _sales = new List<Sale>();

        public virtual IEnumerable<Sale> Sales
        {
            get { return _sales; }
        }

        public virtual void AddSale(Sale sale)
        {
            if (!_sales.Contains(sale))
            {
                _sales.Add(sale);
            }
        }
    }
}
