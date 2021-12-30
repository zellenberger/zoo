using Reproducers;

namespace Animals
{
    /// <summary>
    /// The interface which is used to define the role of a reproduce behavior.
    /// </summary>
    public interface IReproduceBehavior
    {
        /// <summary>
        /// The reproduce behavior.
        /// </summary>
        /// <param name="mother">The mother parameter.</param>
        /// <param name="baby">The baby parameter.</param>
        /// <returns> Returns reproduce.</returns>
        IReproducer Reproduce(Animal mother, Animal baby);
    }
}
