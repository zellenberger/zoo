using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent the fly behavior class.
    /// </summary>
    [Serializable]
    public class FlyBehavior : IMoveBehavior
    {
        /// <summary>
        /// The move method.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            if (animal.YDirection == VerticalDirection.Up)
            {
                if (animal.YPosition + 10 > animal.YPositionMax)
                {
                    animal.YPosition = animal.YPositionMax;
                    animal.YDirection = VerticalDirection.Down;
                }
                else
                {
                    animal.YPosition += 10;
                    animal.YDirection = VerticalDirection.Down;
                }
            }
            else
            {
                if (animal.YPosition - 10 < 0)
                {
                    animal.YPosition = 0;
                    animal.YDirection = VerticalDirection.Up;
                }
                else
                {
                    animal.YPosition -= 10;
                    animal.YDirection = VerticalDirection.Up;
                }
            }
        }
    }
}
