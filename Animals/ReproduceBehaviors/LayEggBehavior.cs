using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which represents reproduce behavior.
    /// </summary>
    [Serializable]
    public class LayEggBehavior : IReproduceBehavior
    {
        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <param name="mother">The mother parameter.</param>
        /// <param name="baby">The baby parameter.</param>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer Reproduce(Animal mother, Animal baby)
        {
            // If the baby is hatchable...
            if (baby is IHatchable)
            {
                // Lay an egg.
                this.LayEgg(mother, baby);

                // Hatch the baby out of its egg.
                this.HatchEgg(baby as IHatchable);
            }

            // Return the (hatched) baby.
            return baby;
        }

        /// <summary>
        /// Hatches an egg.
        /// </summary>
        /// <param name="egg">The egg to hatch.</param>
        private void HatchEgg(IHatchable egg)
        {
            // Hatch the egg.
            egg.Hatch();
        }

        /// <summary>
        /// Lays an egg.
        /// </summary>
        /// <param name="mother">The mother parameter.</param>
        /// <param name="baby">The baby parameter.</param>
        private void LayEgg(Animal mother, Animal baby)
        {
            mother.Weight -= baby.Weight * 1.25;
        }
    }
}
