using System;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent an item.
    /// </summary>
    [Serializable]
    public abstract class Item
    {
        /// <summary>
        /// The weight of the item.
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="weight">The weight of the item.</param>
        public Item(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Gets the item's weight.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
