using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The interface which is used to define the role of a move behavior.
    /// </summary>
    public interface IMoveBehavior
    {
        /// <summary>
        /// Move behavior.
        /// </summary>
        /// <param name="animal">The animal parameter.</param>
        void Move(Animal animal);
    }
}
