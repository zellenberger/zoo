using System;
using System.Collections.Generic;
using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The money collecting booth class.
    /// </summary>
    [Serializable]
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The ticket price field.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The water bottle price field.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// The money box field.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// The stack of tickets.
        /// </summary>
        private Stack<Ticket> ticketStack;

        /// <summary>
        /// Initializes a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">The attendant of the booth.</param>
        /// <param name="ticketPrice">The ticket price of the booth.</param>
        /// <param name="waterBottlePrice">The water bottle price of the booth.</param>
        /// <param name="moneyBox">The money box.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.moneyBox = moneyBox;
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.ticketStack = new Stack<Ticket>();
            for (int i = 0; i < 5; i++)
            {
                this.ticketStack.Push(new Ticket(this.ticketPrice, i, 0.01));
            }

            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new WaterBottle(waterBottlePrice, i, 1));
            }
        }

        /// <summary>
        /// Gets the booth's money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets ticket price getter.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets water bottle price.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// The add money method for vending machine.
        /// </summary>
        /// <param name="amount">The amount of money added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// The remove money method for vending machine.
        /// </summary>
        /// <param name="amount">The amount of money removed.</param>
        /// <returns>Returns the amount of money removed from vending machine.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
        }

        /// <summary>
        /// The sell ticket method.
        /// </summary>
        /// <param name="payment">The payment of tickets.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            try
            {
                Ticket ticket = null;

                if (payment >= this.ticketPrice)
                {
                    ticket = this.ticketStack.Pop();

                    if (ticket != null)
                    {
                        this.AddMoney(payment);
                    }
                }

                return ticket;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }
        }

        /// <summary>
        /// The sell water bottle method.
        /// </summary>
        /// <param name="payment">The payment for water bottle.</param>
        /// <returns>Returns water bottle or nothing.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            try
            {
                WaterBottle waterBottle = null;
                if (payment >= this.waterBottlePrice)
                {
                    waterBottle = this.Attendant.FindItem(this.Items, typeof(WaterBottle)) as WaterBottle;
                }

                return waterBottle;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Water bottle not found.", ex);
            }
        }
    }
}
