using System;
using Animals;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent the bury and eat behavior class.
    /// </summary>
    [Serializable]
    public class BuryAndEatBoneBehavior : IEatBehavior
    {
        /// <summary>
        /// The eat method.
        /// </summary>
        /// <param name="eater">The eater parameter.</param>
        /// <param name="food">The food parameter.</param>
        public void Eat(IEater eater, Food food)
        {
            this.BuryBone(food);
            this.DigUpAndEatBone();

            // Increase animal's weight as a result of eating food.
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);

            this.Bark();
        }

        /// <summary>
        /// The bark method.
        /// </summary>
        private void Bark()
        {
        }

        /// <summary>
        /// The bury bone method.
        /// </summary>
        /// <param name="bone">The bone parameter.</param>
        private void BuryBone(Food bone)
        {
        }

        /// <summary>
        /// The dig up and eat bone method.
        /// </summary>
        private void DigUpAndEatBone()
        {
        }
    }
}
