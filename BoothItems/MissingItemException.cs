using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// Class defining the missing item exception.
    /// </summary>
    public class MissingItemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingItemException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MissingItemException(string message)
            : base(message)
        {
        }
    }
}
