using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using CagedItems;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a cage.
    /// </summary>
    [Serializable]
    public class Cage
    {
        /// <summary>
        /// A list of all animals currently residing within the cage.
        /// </summary>
        private List<ICageable> cagedItems;

        /// <summary>
        /// The on image update.
        /// </summary>
        [NonSerialized]
        private Action<ICageable> onImageUpdate;

        /// <summary>
        /// Initializes a new instance of the Cage class.
        /// </summary>
        /// <param name="height">The height of the item.</param>
        /// <param name="width">The weight of the item.</param>
        public Cage(int height, int width)
        {
            this.cagedItems = new List<ICageable>();
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Gets the animal height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the animal width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the caged items.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// The add method.
        /// </summary>
        /// <param name="cagedItem">The caged item.</param>
        public void Add(ICageable cagedItem)
        {
            this.cagedItems.Add(cagedItem);
            cagedItem.OnImageUpdate += this.HandleImageUpdate;
            this.OnImageUpdate?.Invoke(cagedItem);
        }

        /// <summary>
        /// The remove method.
        /// </summary>
        /// <param name="cagedItem">The caged item.</param>
        public void Remove(ICageable cagedItem)
        {
            this.cagedItems.Remove(cagedItem);
            cagedItem.OnImageUpdate -= this.HandleImageUpdate;
            this.OnImageUpdate?.Invoke(cagedItem);
        }

        /// <summary>
        /// The to string method.
        /// </summary>
        /// <returns>Returns result.</returns>
        public override string ToString()
        {
            string result = $"{this.cagedItems[0].GetType()} cage ({Width}x{Height})";
            foreach (Animal a in this.cagedItems)
            {
                result += $"\n {a.ToString()} ({a.XPosition}x{a.YPosition})";
            }

            return result;
        }

        /// <summary>
        /// The Handle Image Update method.
        /// </summary>
        /// <param name="item">Returns item.</param>
        private void HandleImageUpdate(ICageable item)
        {
            this.OnImageUpdate?.Invoke(item);
        }
    }
}
