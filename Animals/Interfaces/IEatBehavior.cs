using Foods;

namespace Animals
{
    /// <summary>
    /// The interface which is used to define the role of an eat behavior.
    /// </summary>
    public interface IEatBehavior
    {
        /// <summary>
        /// Eater eats the specified food.
        /// </summary>
        /// <param name="eater">The eater parameter.</param>
        /// <param name="food">The food parameter.</param>
        void Eat(IEater eater, Food food);
    }
}
