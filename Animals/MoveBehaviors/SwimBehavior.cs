using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which represents swim behavior.
    /// </summary>
    [Serializable]
    public class SwimBehavior : IMoveBehavior
    {
        /// <summary>
        /// The move method.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            MoveHelper.MoveVertically(animal, animal.MoveDistance / 2);
        }
    }
}
