using System;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a Water Bottle.
    /// </summary>
    [Serializable]
    public class WaterBottle : SoldItem
    {
        /// <summary>
        /// The serial number on the water bottle.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the WaterBottle class.
        /// </summary>
        /// <param name="price">The price of the water bottle.</param>
        /// <param name="serialNumber">The serial number on the water bottle.</param>
        /// <param name="weight">The weight of the water bottle.</param>
        public WaterBottle(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets the water bottle's serial number.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }
    }
}
