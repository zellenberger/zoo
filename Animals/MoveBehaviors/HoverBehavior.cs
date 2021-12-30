using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The hover behavior class.
    /// </summary>
    [Serializable]
    public class HoverBehavior : IMoveBehavior
    {
        /// <summary>
        /// The random time.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The hover process.
        /// </summary>
        private HoverProcess process;

        /// <summary>
        /// The step count.
        /// </summary>
        private int stepCount;

        /// <summary>
        /// Moves the animal to do a hover behavior.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        public void Move(Animal animal)
        {
            if (this.stepCount == 0)
            {
                this.NextProcess(animal);
            }

            this.stepCount--;
            int moveDistance;

            if (this.process == HoverProcess.Hovering)
            {
                moveDistance = animal.MoveDistance;
                animal.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                moveDistance = animal.MoveDistance * 4;                
            }

            MoveHelper.MoveHorizontally(animal, moveDistance);
            MoveHelper.MoveVertically(animal, moveDistance);
        }

        /// <summary>
        /// The next process method.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void NextProcess(Animal animal)
        {
            if (this.process == HoverProcess.Hovering)
            {
                this.process = HoverProcess.Zooming;
                this.stepCount = random.Next(5, 8);
               animal.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                this.process = HoverProcess.Hovering;
                this.stepCount = random.Next(9, 11);
            }
        }
    }
}
