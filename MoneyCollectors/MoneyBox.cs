using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money box.
    /// </summary>
    [Serializable]
    public class MoneyBox : MoneyCollector
    {
        /// <summary>
        /// Removes a specified amount of money from the money pocket.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unlock();
           decimal moneyremoved = base.RemoveMoney(amount);
            this.Lock();
            return moneyremoved;
        }

        /// <summary>
        ///  Locks the money box.
        /// </summary>
        private void Lock()
        {
        }

        /// <summary>
        ///  Locks the money box.
        /// </summary>
        private void Unlock()
        {
        }
    }
}
