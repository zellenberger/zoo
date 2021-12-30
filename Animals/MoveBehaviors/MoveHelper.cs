using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which represents move helper.
    /// </summary>
    public static class MoveHelper
    {
        /// <summary>
        /// Move animal horizontally method.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        /// <param name="moveDistance">The move distance parameter.</param>
        public static void MoveHorizontally(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case CagedItems.HungerState.Satisfied:                    
                    break;

                case CagedItems.HungerState.Hungry:
                    moveDistance = (int)(moveDistance * 0.25);
                    break;

                case CagedItems.HungerState.Starving:
                    moveDistance = 0;
                    break;

                case CagedItems.HungerState.Tired:
                    moveDistance = 0;
                    break;
            }

            if (animal.XDirection == HorizontalDirection.Right)
            {
                if (animal.XPosition + moveDistance > animal.XPositionMax)
                {
                    animal.XPosition = animal.XPositionMax;
                    animal.XDirection = HorizontalDirection.Left;
                }
                else
                {
                    animal.XPosition += moveDistance;
                }
            }
            else
            {
                if (animal.XPosition - moveDistance < 0)
                {
                    animal.XPosition = 0;
                    animal.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    animal.XPosition -= moveDistance;
                }
            }
        }

        /// <summary>
        /// Move animal vertically method.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        /// <param name="moveDistance">The move distance parameter.</param>
        public static void MoveVertically(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case CagedItems.HungerState.Satisfied:
                    break;

                case CagedItems.HungerState.Hungry:
                    moveDistance = (int)(moveDistance * 0.25);
                    break;

                case CagedItems.HungerState.Starving:
                    moveDistance = 0;
                    break;

                case CagedItems.HungerState.Tired:
                    moveDistance = 0;
                    break;
            }

            if (animal.YDirection == VerticalDirection.Up)
            {
                if (animal.YPosition + moveDistance > animal.YPositionMax)
                {
                    animal.YPosition = animal.YPositionMax;
                    animal.YDirection = VerticalDirection.Down;
                }
                else
                {
                    animal.YPosition += moveDistance;
                }
            }
            else
            {
                if (animal.YPosition - moveDistance < 0)
                {
                    animal.YPosition = 0;
                    animal.YDirection = VerticalDirection.Up;
                }
                else
                {
                    animal.YPosition -= moveDistance;
                }
            }
        }
    }
}
