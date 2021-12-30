using System;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    [Serializable]
    public class Wallet : IMoneyCollector
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private WalletColor color;

        /// <summary>
        /// The money pocket field for wallet.
        /// </summary>
        private IMoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(WalletColor color)
        {
            this.color = color;
            this.moneyPocket = new MoneyPocket();
            this.moneyPocket.OnBalanceChange = () =>
            {
                this.OnBalanceChange?.Invoke();
            };
        }

        /// <summary>
        /// Gets or sets the on balance change.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Gets the money balance of wallet.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyPocket.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets or sets the color of wallet.
        /// </summary>
        public WalletColor Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// The add money method for wallet.
        /// </summary>
        /// <param name="amount">The amount of money added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);
        }

        /// <summary>
        /// The remove money from wallet method.
        /// </summary>
        /// <param name="amount">The amount of money removed.</param>
        /// <returns>Returns the amount of money removed from wallet.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyPocket.RemoveMoney(amount);
        }
    }
}