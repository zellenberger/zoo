using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The climb behavior class.
    /// </summary>
    [Serializable]
    public class ClimbBehavior : IMoveBehavior
    {
        /// <summary>
        /// The random time.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The climb process.
        /// </summary>
        private ClimbProcess process = ClimbProcess.Scurrying;

        /// <summary>
        /// The max height.
        /// </summary>
        private int maxHeight;

        /// <summary>
        /// Moves the animal to do a climb behavior.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        public void Move(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    animal.YDirection = VerticalDirection.Down;

                    MoveHelper.MoveVertically(animal, animal.MoveDistance);

                    if (animal.YPosition - animal.MoveDistance <= this.maxHeight)
                    {
                        animal.YDirection = VerticalDirection.Up;

                        if (animal.XDirection == HorizontalDirection.Left)
                        {
                            animal.XDirection = HorizontalDirection.Right;
                        }
                        else
                        {
                            animal.XDirection = HorizontalDirection.Left;
                        }

                        this.NextProcess(animal);
                    }

                    break;

                case ClimbProcess.Falling:
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

                    MoveHelper.MoveVertically(animal, animal.MoveDistance * 2);

                    if (animal.YPosition + animal.MoveDistance >= animal.YPositionMax)
                    {
                        this.NextProcess(animal);
                    }

                    break;

                case ClimbProcess.Scurrying:
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

                    if (animal.XPosition - animal.MoveDistance <= 0)
                    {
                        animal.XPosition = 0;
                        this.NextProcess(animal);
                    }
                    else if (animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                    {
                        animal.XPosition = animal.XPositionMax;
                        this.NextProcess(animal);
                    }

                    break;
            }
        }

        /// <summary>
        /// The next process method.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void NextProcess(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    this.process = ClimbProcess.Falling;
                    break;

                case ClimbProcess.Falling:
                    this.process = ClimbProcess.Scurrying;
                    break;

                case ClimbProcess.Scurrying:
                    //// set the random height
                    int higherMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.15));
                    int lowerMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.85));

                    this.maxHeight = ClimbBehavior.random.Next(higherMax, lowerMax + 1);
                    this.process = ClimbProcess.Climbing;
                    break;
            }
        }
    }
}
