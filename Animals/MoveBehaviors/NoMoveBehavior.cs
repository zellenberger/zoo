using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which represents move behavior.
    /// </summary>
    [Serializable]
    public class NoMoveBehavior : IMoveBehavior
    {
        /// <summary>
        /// The move animal method.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        public void Move(Animal animal)
        {
            // Animal is standing still.
        }
    }
}
