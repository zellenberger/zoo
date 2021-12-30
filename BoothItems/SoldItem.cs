using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The sold item class.
    /// </summary>
    [Serializable]
    public abstract class SoldItem : Item
    {
        /// <summary>
        /// The price field.
        /// </summary>
        private decimal price;

        /// <summary>
        /// Initializes a new instance of the SoldItem class.
        /// </summary>
        /// <param name="price">The price of the sold item.</param>
        /// <param name="weight">The weight of the sold item.</param>
        public SoldItem(decimal price, double weight)
            : base(weight)
        {
            this.price = price;
        }

        /// <summary>
        /// Gets the sold item's price.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.price;
            }
        }
    }
}
