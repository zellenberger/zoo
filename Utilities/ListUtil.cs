using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    /// <summary>
    /// The list utility class.
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// Flattens the list being used.
        /// </summary>
        /// <param name="list">The list being modified.</param>
        /// <param name="separator">Separate the list.</param>
        /// <returns>Returns the string result.</returns>
        public static string Flatten(this IEnumerable<string> list, string separator)
        {
            string result = null;
            list.ToList().ForEach(s => result += result == null ? s : separator + s);

            return result;
        }
    }
}
