using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal factory.
    /// </summary>
    public static class AnimalFactory
    {
        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="type">The type of the animal.</param>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        /// <returns>Returns an animal.</returns>
        public static Animal CreateAnimal(AnimalType type, string name, int age, double weight, Gender gender)
        {
            Animal result = null;

            switch (type)
            {
                case AnimalType.Chimpanzee:
                    result = new Chimpanzee(name, age, weight, gender);
                    break;

                case AnimalType.Dingo:
                    result = new Dingo(name, age, weight, gender);
                    break;

                case AnimalType.Eagle:
                    result = new Eagle(name, age, weight, gender);
                    break;

                case AnimalType.Hummingbird:
                    result = new Hummingbird(name, age, weight, gender);
                    break;

                case AnimalType.Kangaroo:
                    result = new Kangaroo(name, age, weight, gender);
                    break;

                case AnimalType.Ostrich:
                    result = new Ostrich(name, age, weight, gender);
                    break;

                case AnimalType.Platypus:
                    result = new Platypus(name, age, weight, gender);
                    break;

                case AnimalType.Shark:
                    result = new Shark(name, age, weight, gender);
                    break;

                case AnimalType.Squirrel:
                    result = new Squirrel(name, age, weight, gender);
                    break;

                default:
                    break;
            }

            return result;
        }
    }
}
