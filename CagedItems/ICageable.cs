using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CagedItems
{
    /// <summary>
    /// The interface which is used to define the role of a cage item.
    /// </summary>
    public interface ICageable
    {
        /// <summary>
        /// Gets the display size.
        /// </summary>
        double DisplaySize { get; }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        string ResourceKey { get; }

        /// <summary>
        /// Gets the x position.
        /// </summary>
        int XPosition { get; }

        /// <summary>
        /// Gets the y position.
        /// </summary>
        int YPosition { get; }

        /// <summary>
        /// Gets the x direction.
        /// </summary>
        HorizontalDirection XDirection { get; }

        /// <summary>
        /// Gets the y direction.
        /// </summary>
        VerticalDirection YDirection { get; }

        /// <summary>
        /// Gets the hunger state.
        /// </summary>
        HungerState HungerState { get; }

        /// <summary>
        /// Gets a value indicating whether the is active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        Action<ICageable> OnImageUpdate { get; set; }
    }
}
