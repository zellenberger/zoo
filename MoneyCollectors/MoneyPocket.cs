using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money pocket.
    /// </summary>
    [Serializable]
    public class MoneyPocket : MoneyCollector
    {
        /// <summary>
        /// Removes a specified amount of money from the money pocket.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unfold();
            decimal moneyremoved = base.RemoveMoney(amount);
            this.Fold();
            return moneyremoved;
        }

        /// <summary>
        /// Folds the money pocket.
        /// </summary>
        private void Fold()
        {
        }

        /// <summary>
        /// Unfolds the money pocket.
        /// </summary>
        private void Unfold()
        {
        }
    }
}
