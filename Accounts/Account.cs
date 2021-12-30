using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;

namespace Accounts
{
    /// <summary>
    /// The class which is used to represent an account.
    /// </summary>
    [Serializable]
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// The money balance of the account.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets the money balance of account.
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
        /// The add money method for account.
        /// </summary>
        /// <param name="amount">The amount of money added.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// The remove money from account method.
        /// </summary>
        /// <param name="amount">The amount of money removed.</param>
        /// <returns>Returns the amount of money removed from wallet.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            this.MoneyBalance -= amount;
            return amount;
        }
    }
}
