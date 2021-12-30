using System;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money collector.
    /// </summary>
    [Serializable]
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The amount of money currently in the money collector.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets the money collector's money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }

            set
            {
                this.moneyBalance = value;

                this.OnBalanceChange?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the balance change.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds a specified amount of money to the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the money collector...
            if (this.moneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.moneyBalance;
            }

            // Subtract the amount removed from the money collector's money balance.
            this.MoneyBalance -= amountRemoved;

            return amountRemoved;
        }
    }
}
