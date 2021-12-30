using System;
using Foods;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which represents give birth behavior.
    /// </summary>
    [Serializable]
    public class GiveBirthBehavior : IReproduceBehavior
    {
        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <param name="mother">The mother parameter.</param>
        /// <param name="baby">The baby parameter.</param>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer Reproduce(Animal mother, Animal baby)
        {
            mother.Weight -= baby.Weight;

            // Feed the baby.
            this.FeedNewborn(baby as IEater, mother);

            return baby;
        }

        /// <summary>
        /// Feeds a baby eater.
        /// </summary>
        /// <param name="newborn">The eater to feed.</param>
        /// <param name="mother">The mother who feeds.</param>
        private void FeedNewborn(IEater newborn, Animal mother)
        {
            // Determine milk weight.
            double milkWeight = mother.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            newborn.Eat(milk);

            // Reduce parent's weight.
            mother.Weight -= milkWeight;
        }
    }
}
