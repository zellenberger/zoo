using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent the zoo extensions.
    /// </summary>
    public static class ZooExtensions
    {
        /// <summary>
        /// The find animal method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <param name="match">The type of match to find.</param>
        /// <returns>The matching animal.</returns>
        public static Animal FindAnimal(this Zoo zoo, Predicate<Animal> match)
        {
            return zoo.Animals.ToList().Find(match);
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <param name="match">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo zoo, Predicate<Guest> match)
        {
            // Return the matching guest.
            return zoo.Guests.ToList().Find(match);
        }

        /// <summary>
        /// The get young guests method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns guests.</returns>
        public static IEnumerable<object> GetYoungGuests(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                where g.Age <= 10
                select new { g.Name, g.Age };
        }

        /// <summary>
        /// The get female dingoes method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns female dingoes.</returns>
        public static IEnumerable<object> GetFemaleDingoes(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                where a.Gender == Gender.Female && a.GetType() == typeof(Dingo)
                select new { a.Name, a.Age, a.Gender };
        }

        /// <summary>
        /// The get heavy animals method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns animals.</returns>
        public static IEnumerable<object> GetHeavyAnimals(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                where a.Weight > 200
                select new { Type = a.GetType().Name, a.Name, a.Weight};
        }

        /// <summary>
        /// The get guests by age method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns guests.</returns>
        public static IEnumerable<object> GetGuestsByAge(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                orderby g.Age
                select new { g.Name, g.Age, g.Gender};
        }

        /// <summary>
        /// The get flying animals method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns flying animals.</returns>
        public static IEnumerable<object> GetFlyingAnimals(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                where a.MoveBehavior is FlyBehavior
                select new { AnimalType = a.GetType().Name, a.Name };
        }

        /// <summary>
        /// The get adopted animals method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns guests and adopted animals.</returns>
        public static IEnumerable<object> GetAdoptedAnimals(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                where g.AdoptedAnimal != null
                select new { g.Name, AnimalName = g.AdoptedAnimal.Name, AnimalType = g.AdoptedAnimal.GetType().Name, };
        }

        /// <summary>
        /// The get Total Balance By Wallet Color method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns total wallet balance.</returns>
        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                group g by g.Wallet.Color.ToString() into gr
                orderby gr.Key
                select new { gr.Key, TotalMoneyBalance = gr.Sum(g => g.Wallet.MoneyBalance) };
        }

        /// <summary>
        /// The get Average Animal Weight By AnimalType method.
        /// </summary>
        /// <param name="zoo">The como zoo.</param>
        /// <returns>Returns animals.</returns>
        public static IEnumerable<object> GetAverageAnimalWeightByAnimalType(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                group a by a.GetType().Name.ToString() into gr
                orderby gr.Key
                select new { AnimalType = gr.Key, AverageWeight = gr.Average(a => a.Weight) };
        }
    }
}
