using System;
using Animals;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent the show affection behavior class.
    /// </summary>
    [Serializable]
    public class ShowAffectionBehavior : IEatBehavior
    {
        /// <summary>
        /// The eat method.
        /// </summary>
        /// <param name="eater">The eater parameter.</param>
        /// <param name="food">The food parameter.</param>
        public void Eat(IEater eater, Food food)
        {
            // Increase animal's weight as a result of eating food.
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
            this.ShowAffection();
        }

        /// <summary>
        /// The show affection method.
        /// </summary>
        private void ShowAffection()
        {
        }
    }
}
