using System;
using System.Collections.Generic;
using Animals;

namespace Zoos
{
    /// <summary>
    /// The sort result of the list.
    /// </summary>
    [Serializable]
    public class SortResult
    {
        /// <summary>
        /// Gets or sets the animals in the list.
        /// </summary>
        public List<object> Objects { get; set; }

        /// <summary>
        /// Gets or sets the compare the count.
        /// </summary>
        public int CompareCount { get; set; }

        /// <summary>
        /// Gets or sets the elapsed milliseconds.
        /// </summary>
        public double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the swap count of the animals.
        /// </summary>
        public int SwapCount { get; set; }
    }
}
