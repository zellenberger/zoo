using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Animals;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// The random movement.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// The gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The move timer.
        /// </summary>
        [NonSerialized]
        private Timer moveTimer;

        /// <summary>
        /// List of children.
        /// </summary>
        private List<Animal> children;

        /// <summary>
        /// The text change.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onTextChange;

        /// <summary>
        /// The hunger timer.
        /// </summary>
        [NonSerialized]
        private Timer hungerTimer;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;
            this.XPositionMax = 800;
            this.YPositionMax = 400;
            this.MoveDistance = 20;
            this.MoveDistance = random.Next(5, 16);
            this.XPosition = random.Next(1, this.XPositionMax + 1);
            this.YPosition = random.Next(1, this.YPositionMax + 1);
            this.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            this.CreateTimers();
            this.children = new List<Animal>();
            this.HungerState = HungerState.Satisfied;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }

            set
            {
                this.isPregnant = value;
                this.OnTextChange?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
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
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException("An animal can only have a weight between 0 and 1000");
                }
                else
                {
                    this.weight = value;

                    this.OnTextChange?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("An animal can only have an age between 0 and 100");
                }
                else
                {
                    this.age = value;

                    this.OnTextChange?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the gender of the animal.
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
        /// Gets or sets the move distance of the animal.
        /// </summary>
        public int MoveDistance { get; set; }

        /// <summary>
        /// Gets or sets the x direction of the animal.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the x position of the animal.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the  max x position of the animal.
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the y direction of the animal.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets the y position of the animal.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the  max y position of the animal.
        /// </summary>
        public int YPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the movement behavior of the animal.
        /// </summary>
        public IMoveBehavior MoveBehavior { get; set; }

        /// <summary>
        /// Gets or sets the eating behavior of the animal.
        /// </summary>
        public IEatBehavior EatBehavior { get; set; }

        /// <summary>
        /// Gets or sets the reproducing behavior of the animal.
        /// </summary>
        public IReproduceBehavior ReproduceBehavior { get; set; }

        /// <summary>
        /// Gets the display size.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                return this.age == 0 ? 0.5 : 1.0;
            }
        }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return this.GetType().Name + (this.Age == 0 ? "Baby" : "Adult");
            }
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Gets the list of children.
        /// </summary>
        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets the text change delegate.
        /// </summary>
        public Action<Animal> OnTextChange
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
        /// Gets or sets the on pregnant.
        /// </summary>
        public Action<IReproducer> OnPregnant { get; set; }

    /// <summary>
    /// Gets or sets the hunger state.
    /// </summary>
    public HungerState HungerState { get; set; }

        /// <summary>
        /// Gets or sets the on hunger.
        /// </summary>
        public Action OnHunger { get; set; }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Converts the animal type method.
        /// </summary>
        /// <param name="animalType">The type of animal.</param>
        /// <returns>Animal Type.</returns>
        public static Type ConvertAnimalTypeToType(AnimalType animalType)
        {
            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    return typeof(Chimpanzee);
                case AnimalType.Dingo:
                    return typeof(Dingo);
                case AnimalType.Eagle:
                    return typeof(Eagle);
                case AnimalType.Hummingbird:
                    return typeof(Hummingbird);
                case AnimalType.Kangaroo:
                    return typeof(Kangaroo);
                case AnimalType.Ostrich:
                    return typeof(Ostrich);
                case AnimalType.Platypus:
                    return typeof(Platypus);
                case AnimalType.Shark:
                    return typeof(Shark);
                case AnimalType.Squirrel:
                    return typeof(Squirrel);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            this.EatBehavior.Eat(this, food);
            this.hungerTimer.Stop();
            this.HungerState = HungerState.Satisfied;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.OnPregnant?.Invoke(this);
            this.IsPregnant = true;
            this.MoveBehavior = new NoMoveBehavior();
        }

        /// <summary>
        /// Moves about.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);
            this.OnImageUpdate?.Invoke(this);
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer Reproduce()
        {
            // Create a baby reproducer.
            Animal baby = Activator.CreateInstance(this.GetType(), string.Empty, 0, this.Weight * (this.BabyWeightPercentage / 100), this.Gender) as Animal;

            baby = (Animal)this.ReproduceBehavior.Reproduce(this, baby);
            baby.Gender = random.Next(0, 2) == 0 ? Gender.Female : Gender.Male;
            this.AddChild(baby);

            // Make mother not pregnant after giving birth.
            this.IsPregnant = false;

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            return this.name + ": " + this.GetType().Name + " (" + this.age + ", " + this.Weight + ")" + (this.IsPregnant ? "P" : string.Empty);
        }

        /// <summary>
        /// The add child method.
        /// </summary>
        /// <param name="animal">The animal.</param>
        public void AddChild(Animal animal)
        {
            if (animal != null)
            {
                this.children.Add(animal);
            }
        }

        /// <summary>
        /// The create times method.
        /// </summary>
        private void CreateTimers()
        {
            this.moveTimer = new Timer(100);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();

            this.hungerTimer = new Timer(random.Next(1, 21) * 1000);
            this.hungerTimer.Elapsed += this.HandleHungerStateChange;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// The on deserialized method.
        /// </summary>
        /// <param name="streaming">The streaming parameter.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext streaming)
        {
            this.CreateTimers();
        }

        /// <summary>
        /// The animal move timer.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
#if DEBUG
            this.moveTimer.Stop();
#endif
            this.Move();
#if DEBUG
            this.moveTimer.Start();
#endif
        }

        /// <summary>
        /// The Handle Hunger State Change method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void HandleHungerStateChange(object sender, ElapsedEventArgs e)
        {
            switch (this.HungerState)
            {
                case HungerState.Hungry:
                    this.HungerState = HungerState.Starving;
                    break;

                case HungerState.Satisfied:
                    this.HungerState = HungerState.Hungry;
                    break;

                case HungerState.Starving:
                    this.HungerState = HungerState.Tired;
                    this.OnHunger?.Invoke();
                    break;
            }
        }
    }
}