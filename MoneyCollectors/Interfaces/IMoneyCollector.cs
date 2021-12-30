using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The interface which is used to define the role of a money collector.
    /// </summary>
    public interface IMoneyCollector
    {
        /// <summary>
        /// Gets or sets the balance change.
        /// </summary>
        Action OnBalanceChange { get; set; }

        /// <summary>
        /// Gets the money balance of the money collector.
        /// </summary>
        decimal MoneyBalance { get; }

        /// <summary>
        /// The add money method for money collector.
        /// </summary>
        /// <param name="amount">The amount of money added.</param>
        void AddMoney(decimal amount);

        /// <summary>
        /// The remove money method for money collector.
        /// </summary>
        /// <param name="amount">The amount of money removed.</param>
        /// <returns>Returns an amount.</returns>
        decimal RemoveMoney(decimal amount);
    }
}
