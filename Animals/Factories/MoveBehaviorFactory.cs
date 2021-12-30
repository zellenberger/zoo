using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a move behavior factory.
    /// </summary>
    public static class MoveBehaviorFactory
    {
        /// <summary>
        /// The create move behavior method.
        /// </summary>
        /// <param name="type">The type parameter.</param>
        /// <returns>Returns behavior.</returns>
        public static IMoveBehavior CreateMoveBehavior(MoveBehaviorType type)
        {
            IMoveBehavior behavior = null;
            switch (type)
            {
                case MoveBehaviorType.Fly:
                     behavior = new FlyBehavior();
                    break;
                case MoveBehaviorType.Pace:
                    behavior = new PaceBehavior();
                    break;
                case MoveBehaviorType.Swim:
                    behavior = new SwimBehavior();
                    break;
                case MoveBehaviorType.NoMove:
                    behavior = new NoMoveBehavior();
                    break;
                case MoveBehaviorType.Climb:
                    behavior = new ClimbBehavior();
                    break;
                case MoveBehaviorType.Hover:
                    behavior = new HoverBehavior();
                    break;
                default:
                    break;
            }

            return behavior;
        }
    }
}
