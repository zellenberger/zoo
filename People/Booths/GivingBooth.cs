using System;
using BoothItems;

namespace People
{
    /// <summary>
    /// The giving booth class.
    /// </summary>
    [Serializable]
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Initializes a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">The attendant of the giving booth.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new CouponBook(DateTime.Now, DateTime.Now.AddYears(1), 0.8));
            }

            for (int i = 0; i < 10; i++)
            {
                this.Items.Add(new Map(.5, DateTime.Now));
            }
        }

        /// <summary>
        /// The give free coupon book method.
        /// </summary>
        /// <returns>Returns a coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            try
            {
                CouponBook couponBook = this.Attendant.FindItem(this.Items, typeof(CouponBook)) as CouponBook;
                return couponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("CouponBook not found.", ex);
            }
        }

        /// <summary>
        /// The give free map method.
        /// </summary>
        /// <returns>Returns a map.</returns>
        public Map GiveFreeMap()
        {
            try
            {
                Map map = this.Attendant.FindItem(this.Items, typeof(Map)) as Map;
                return map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Map not found.", ex);
            }
        }
    }
}
