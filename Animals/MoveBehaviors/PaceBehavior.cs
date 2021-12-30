using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which represents the pace behavior.
    /// </summary>
    [Serializable]
    public class PaceBehavior : IMoveBehavior
    {
        /// <summary>
        /// The move method.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        public void Move(Animal animal)
        {
               MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
        }
    }
}
