using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// The random movement.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// The bag list.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// The guest's gender.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The guest's checking account.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// The adopted animal.
        /// </summary>
        private Animal adoptedAnimal;

        /// <summary>
        /// The text change.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onTextChange;

        /// <summary>
        /// The feed timer.
        /// </summary>
        [NonSerialized]
        private Timer feedTimer;

        /// <summary>
        /// The is active field.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="gender">The guest's gender.</param>
        /// <param name="checkingAccount">The checking account.</param>
        public Guest(string name, int age, decimal moneyBalance, WalletColor walletColor, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.checkingAccount = checkingAccount;
            this.name = name;
            this.wallet = new Wallet(walletColor);
            this.wallet.AddMoney(moneyBalance);
            this.bag = new List<Item>();
            this.gender = gender;
            this.XPosition = random.Next(1, 201);
            this.YPosition = random.Next(1, 401);
            checkingAccount.OnBalanceChange += this.HandleBalanceChange;
            this.wallet.OnBalanceChange += this.HandleBalanceChange;
            this.CreateTimers();
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new ArgumentOutOfRangeException("Name must only include letters and spaces");
                }
                else
                {
                    this.name = value;
                    this.OnTextChange?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }

            set
            {
                this.Weight = value;
            }
        }

        /// <summary>
        /// Gets the weight gain percentage of the guest.
        /// </summary>
        public double WeightGainPercentage
        {
            get
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ArgumentOutOfRangeException("An animal can only have an age between 0 and 120");
                }
                else
                {
                    this.age = value;
                    this.OnTextChange?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the gender of the guest.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets the checking account of the guest.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets the wallet of the guest.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets or sets adopted animal.
        /// </summary>
        public Animal AdoptedAnimal
        {
            get
            {
                return this.adoptedAnimal;
            }

            set
            {
                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger = null;
                }

                this.adoptedAnimal = value;

                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger += this.HandleAnimalHungry;
                }

                this.OnTextChange?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets the display size.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Gets the x position.
        /// </summary>
        public int XPosition { get; private set; }

        /// <summary>
        /// Gets the y position.
        /// </summary>
        public int YPosition { get; private set; }

        /// <summary>
        /// Gets the x direction.
        /// </summary>
        public HorizontalDirection XDirection { get; private set; }

        /// <summary>
        /// Gets the y direction.
        /// </summary>
        public VerticalDirection YDirection { get; private set; }

        /// <summary>
        /// Gets or sets the text change.
        /// </summary>
        public Action<Guest> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }

            set
            {
                this.onTextChange = value;
            }
        }

        /// <summary>
        /// Gets the hunger state.
        /// </summary>
        public HungerState HungerState { get; }

        /// <summary>
        /// Gets or sets the get vending machine.
        /// </summary>
        public Func<VendingMachine> GetVendingMachine { get; set; }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive && this.adoptedAnimal != null ? true : false;
            }
            
            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// The Handle animal hungry method.
        /// </summary>
        public void HandleAnimalHungry()
        {
            this.feedTimer.Start();
        }

        /// <summary>
        /// The Handle ready to feed method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        public void HandleReadyToFeed(object sender, ElapsedEventArgs e)
        {
            this.FeedAnimal(this.adoptedAnimal);
            this.feedTimer.Stop();
        }

        /// <summary>
        /// The withdraw money from wallet method.
        /// </summary>
        /// <param name="amount">The amount of money removed.</param>
        public void WithdrawMoney(decimal amount)
        {
            this.Wallet.AddMoney(this.CheckingAccount.RemoveMoney(amount));
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// The to string method.
        /// </summary>
        /// <returns>Returns the name, age and money balance of guest.</returns>
        public override string ToString()
        {
            string line = $"{name}: {age} [${wallet.MoneyBalance} / ${this.checkingAccount.MoneyBalance}]";
            if (this.AdoptedAnimal != null)
            {
                line += this.AdoptedAnimal.Name;
            }

            return line;
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        public void FeedAnimal(IEater eater)
        {
            VendingMachine animalSnackMachine = this.GetVendingMachine?.Invoke();

            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            if (this.Wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price * 5);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// The information booth method.
        /// </summary>
        /// <param name="informationBooth">The booth that gives out information.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            Map map = informationBooth.GiveFreeMap();
            this.bag.Add(map);
            CouponBook couponBook = informationBooth.GiveFreeCouponBook();
            this.bag.Add(couponBook);
        }

        /// <summary>
        /// The visiting ticket booth method.
        /// </summary>
        /// <param name="ticketBooth">The ticket booth at zoo.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            Ticket ticket = null;

            if (this.wallet.MoneyBalance < (ticketBooth.TicketPrice + ticketBooth.WaterBottlePrice))
            {
                this.WithdrawMoney(ticketBooth.TicketPrice * 2);
            }

            decimal payment = this.wallet.RemoveMoney(ticketBooth.TicketPrice);
            ticket = ticketBooth.SellTicket(payment);
            decimal waterBottlePrice = ticketBooth.WaterBottlePrice;
            this.wallet.RemoveMoney(waterBottlePrice);
            WaterBottle waterBottle = ticketBooth.SellWaterBottle(waterBottlePrice);
            this.bag.Add(waterBottle);
            return ticket;
        }

        /// <summary>
        /// The handle balance change method.
        /// </summary>
        private void HandleBalanceChange()
        {
            this.OnTextChange?.Invoke(this);
        }

        /// <summary>
        /// The create timers method.
        /// </summary>
        private void CreateTimers()
        {
            this.feedTimer = new Timer(5);
            this.feedTimer.Elapsed += this.HandleReadyToFeed;
        }

        /// <summary>
        /// The on deserialized method.
        /// </summary>
        /// <param name="context">Returns context.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }
    }
}