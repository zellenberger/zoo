using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a shark.
    /// </summary>
    [Serializable]
    public class Shark : Fish
    {
        /// <summary>
        /// Initializes a new instance of the Shark class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal (in pounds).</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 18.0;
        }

        /// <summary>
        /// Gets the display size of the shark.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 1.0 : 1.5;
            }
        }
    }
}
