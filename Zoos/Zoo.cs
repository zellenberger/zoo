using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The cages field.
        /// </summary>
        private Dictionary<Type, Cage> cages;

        /// <summary>
        /// The on birthing room temperature change field.
        /// </summary>
        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperatureChange;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// The information booth field.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// Add guest field.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onAddGuest;

        /// <summary>
        /// Remove guest field.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onRemoveGuest;

        /// <summary>
        /// Add animal field.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onAddAnimal;

        /// <summary>
        /// Remove animal field.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onRemoveAnimal;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The zoo's water bottle price.</param>
        /// <param name="boothMoneyBalance">The initial money balance of the zoo's ticket booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.cages = new Dictionary<Type, Cage>();

            foreach (AnimalType a in Enum.GetValues(typeof(AnimalType)))
            {
                this.cages.Add(Animal.ConvertAnimalTypeToType(a), new Cage(400, 800));
            }

            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.b168.OnTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.OnBirthingRoomTemperatureChange(previousTemp, currentTemp);
            };
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());
            this.ticketBooth.AddMoney(boothMoneyBalance);
            this.informationBooth = new GivingBooth(attendant);

            this.animals = new List<Animal>();

            ////Animal brutus = new Dingo("Brutus", 3, 36.0, Gender.Male);
            ////Animal coco = new Dingo("Coco", 7, 38.3, Gender.Female);
            ////coco.AddChild(brutus);

            ////Animal toby = new Dingo("Toby", 4, 42.5, Gender.Male);
            ////Animal steve = new Dingo("Steve", 4, 41.1, Gender.Male);
            ////Animal maggie = new Dingo("Maggie", 7, 34.8, Gender.Female);
            ////maggie.AddChild(toby);
            ////maggie.AddChild(steve);

            ////Animal lucy = new Dingo("Lucy", 7, 36.5, Gender.Female);
            ////Animal ted = new Dingo("Ted", 7, 39.7, Gender.Male);
            ////Animal bella = new Dingo("Bella", 10, 40.2, Gender.Female);
            ////bella.AddChild(coco);
            ////bella.AddChild(maggie);
            ////bella.AddChild(lucy);
            ////bella.AddChild(ted);

            ////List<Animal> tempList = new List<Animal>();
            ////tempList.Add(bella);
            ////tempList.Add(new Dingo("Max", 12, 46.9, Gender.Male));

            ////this.AddAnimalsToZoo(tempList);

            // Animals for sorting
            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));
            Shark chum = new Shark("Chum", 14, 377.3, Gender.Male);
            this.AddAnimal(chum);
            Squirrel chip = new Squirrel("Chip", 4, 1.0, Gender.Male);
            this.AddAnimal(chip);
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            // Guests for sorting
            this.guests = new List<Guest>();
            Guest greg = new Guest("Greg", 35, 100.0m, WalletColor.Crimson, Gender.Male, new Account());
            Guest darla = new Guest("Darla", 7, 10.0m, WalletColor.Brown, Gender.Male, new Account());
            greg.AdoptedAnimal = chip;
            darla.AdoptedAnimal = chum;
            this.AddGuest(greg, new Ticket(0m, 0, 0));
            this.AddGuest(darla, new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Anna", 8, 12.56m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Matthew", 42, 10.0m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Doug", 7, 11.10m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Jared", 17, 31.70m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sean", 34, 20.50m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sally", 52, 134.20m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
        }

        /// <summary>
        /// Gets the list of animals.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets the list of guests.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets or sets birthing room temperature change.
        /// </summary>
        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperatureChange;
            }

            set
            {
                this.onBirthingRoomTemperatureChange = value;
            }
        }

        /// <summary>
        /// Gets or sets the on add guest.
        /// </summary>
        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }

            set
            {
                this.onAddGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets the on remove guest.
        /// </summary>
        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }

            set
            {
                this.onRemoveGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets the animal.
        /// </summary>
        public Action<Animal> OnAddAnimal
        {
            get
            {
                return this.onAddAnimal;
            }

            set
            {
                this.onAddAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets the removed guest.
        /// </summary>
        public Action<Animal> OnRemoveAnimal
        {
            get
            {
                return this.onRemoveAnimal;
            }

            set
            {
                this.onRemoveAnimal = value;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                // Define accumulator variable.
                double totalWeight = 0;

                // Loop through the list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Creates an instance of the zoo class and configures it as the Como Zoo.
        /// </summary>
        /// <returns>Returns the new zoo that is created.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo zoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

            // Add money to the animal snack machine.
            zoo.animalSnackMachine.AddMoney(42.75m);

            return zoo;
        }

        /// <summary>
        /// The load from file method.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Returns result.</returns>
        public static Zoo LoadFromFile(string fileName)
        {
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            return result;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);
            this.cages[animal.GetType()].Add(animal);
            animal.OnPregnant = (pregnantAnimal) =>
            {
                this.b168.PregnantAnimals.Enqueue(pregnantAnimal);
            };

            this.b168.PregnantAnimals.Enqueue(animal);
            this.OnAddAnimal?.Invoke(animal);
            animal.IsActive = true;
        }

        /// <summary>
        /// Removes an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to remove.</param>
        public void RemoveAnimal(Animal animal)
        {
            this.OnRemoveAnimal?.Invoke(animal);
            foreach (Guest g in this.guests)
            {
                if (g.AdoptedAnimal == animal)
                {
                    g.AdoptedAnimal = null;
                    this.FindCage(animal.GetType()).Remove(g);
                }
            }

            this.animals.Remove(animal);
            this.cages[animal.GetType()].Remove(animal);
            animal.IsActive = false;
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The ticket used for admission.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket != null && ticket.IsRedeemed == false)
            {
                ticket.Redeem();
                guest.GetVendingMachine += this.ProvideVendingMachine;
                this.guests.Add(guest);
                this.OnAddGuest?.Invoke(guest);
            }
            else
            {
                throw new NullReferenceException("The guest couldn't be admitted because they did not have a ticket.");
            }

            guest.IsActive = true;
        }

        /// <summary>
        /// Removes an animal to the zoo.
        /// </summary>
        /// <param name="guest">The guest to remove.</param>
        public void RemoveGuest(Guest guest)
        {
            this.OnRemoveGuest?.Invoke(guest);
            if (guest.AdoptedAnimal != null)
            {
                Cage cage = this.FindCage(guest.AdoptedAnimal.GetType());
                cage.Remove(guest);
            }

            this.guests.Remove(guest);
            guest.IsActive = false;
        }

        /// <summary>
        /// The sell ticket method.
        /// </summary>
        /// <param name="guest">The guest who is being sold the ticket.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            Ticket ticket = guest.VisitTicketBooth(this.ticketBooth);
            guest.VisitInformationBooth(this.informationBooth);
            return ticket;
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        public void BirthAnimal()
        {
            try
            {
                // Birth animal.
                IReproducer reproducer = this.b168.PregnantAnimals.Dequeue();
                IReproducer baby = this.b168.BirthAnimal(reproducer);

                // If the baby is an animal...
                if (baby is Animal)
                {
                    // Add the baby to the zoo's list of animals.
                    this.AddAnimal(baby as Animal);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("The zoo does not have any pregnant animals.");
            }
        }

        /// <summary>
        /// Finds a cage based on animal type.
        /// </summary>
        /// <param name="animalType">The type of the animal to find.</param>
        /// <returns>The first matching cage.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage result = null;

            this.cages.TryGetValue(animalType, out result);

            return result;
        }

        /// <summary>
        /// Get animals type.
        /// </summary>
        /// <param name="type">The type of animal.</param>
        /// <returns>The animals.</returns>
        public IEnumerable<Animal> GetAnimals(Type type)
        {
            Animal animal = null;
                foreach (Animal a in this.animals)
                {
                    if (a.GetType() == type)
                    {
                        animal = a;
                        this.animals.Add(a);
                    }
                }

            return this.animals;
        }

        /// <summary>
        /// The Sort animals method.
        /// </summary>
        /// <param name="sortType">The sort type.</param>
        /// <param name="sortValue">The sort value.</param>
        /// <returns>Returns the result.</returns>
        public SortResult SortAnimals(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.animals);
        }

        /// <summary>
        /// The Sort guests method.
        /// </summary>
        /// <param name="sortType">The sort type.</param>
        /// <param name="sortValue">The sort value.</param>
        /// <returns>Returns the result.</returns>
        public SortResult SortGuests(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.guests);
        }

        /// <summary>
        /// Sorts the animals in the list.
        /// </summary>
        /// <param name="sortType">The type of sort.</param>
        /// <param name="sortValue">The value of the sort.</param>
        /// <param name="list">The list of objects.</param>
        /// <returns>Returns the sort result.</returns>
        public SortResult SortObjects(string sortType, string sortValue, IList list)
        {
            Func<object, object, int> compareFunc = null;
            SortResult result = null;
            try
            {
                if (sortValue == "animalname")
                {
                    compareFunc = AnimalNameSortCompare;
                }
                else if (sortValue == "guestname")
                {
                    compareFunc = GuestNameSortCompare;
                }
                else if (sortValue == "animalage")
                {
                    compareFunc = AgeSortCompare;
                }
                else if (sortValue == "animalweight")
                {
                    compareFunc = WeightSortCompare;
                }
                else if (sortValue == "moneybalance")
                {
                    compareFunc = MoneyBalanceSortCompare;
                }

                if (sortType == "animals")
                {
                    this.animals = list.Cast<Animal>().ToList();
                }
                else if (sortType == "guest")
                {
                    this.guests = list.Cast<Guest>().ToList();
                }

                switch (sortType)
                {
                    case "bubble":
                        result = list.BubbleSort(compareFunc);
                        break;

                    case "selection":
                        result = list.SelectionSort(compareFunc);
                        break;

                    case "insertion":
                        result = list.InsertionSort(compareFunc);
                        break;

                    case "quick":
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        result = new SortResult();
                        result = list.QuickSort(0, list.Count - 1, result, compareFunc);
                        stopWatch.Stop();
                        result.ElapsedMilliseconds = stopWatch.Elapsed.TotalMilliseconds;

                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid sort value.");
            }
           
            return result;
        }

        /// <summary>
        /// The save to file method.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of the zoo
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// The on deserialized method.
        /// </summary>
        public void OnDeserialized()
        {
            this.OnBirthingRoomTemperatureChange?.Invoke(this.BirthingRoomTemperature, this.BirthingRoomTemperature);

            this.guests.ForEach(g =>
            {
                this.OnAddGuest?.Invoke(g);
                g.GetVendingMachine += this.ProvideVendingMachine;
            });

            this.animals.ForEach(a =>
            {
                this.OnAddAnimal?.Invoke(a);

                a.OnPregnant = (pregnantAnimal) =>
                {
                    this.b168.PregnantAnimals.Enqueue(pregnantAnimal);
                };
            });
        }

        /// <summary>
        /// The Animal Name Sort Compare method.
        /// </summary>
        /// <param name="object1">The first object.</param>
        /// <param name="object2">The second object.</param>
        /// <returns>Returns the compare result.</returns>
        private static int AnimalNameSortCompare(object object1, object object2)
        {
            return string.Compare((object1 as Animal).Name, (object2 as Animal).Name);
        }

        /// <summary>
        /// The Guest Name Sort Compare method.
        /// </summary>
        /// <param name="object1">The first object.</param>
        /// <param name="object2">The second object.</param>
        /// <returns>Returns the compare result.</returns>
        private static int GuestNameSortCompare(object object1, object object2)
        {
            return string.Compare((object1 as Guest).Name, (object2 as Guest).Name);
        }

        /// <summary>
        /// The Weight Sort Compare method.
        /// </summary>
        /// <param name="object1">The first object.</param>
        /// <param name="object2">The second object.</param>
        /// <returns>Returns the compare result.</returns>
        private static int WeightSortCompare(object object1, object object2)
        {
            double weight1 = (object1 as Animal).Weight;
            double weight2 = (object2 as Animal).Weight;

            return weight1 == weight2 ? 0 : weight1 > weight2 ? 1 : -1;
        }

        /// <summary>
        /// The Money Balance Sort Compare method.
        /// </summary>
        /// <param name="object1">The first object.</param>
        /// <param name="object2">The second object.</param>
        /// <returns>Returns the compare result.</returns>
        private static int MoneyBalanceSortCompare(object object1, object object2)
        {
            decimal money1 = (object1 as Guest).Wallet.MoneyBalance + (object1 as Guest).CheckingAccount.MoneyBalance;
            decimal money2 = (object2 as Guest).Wallet.MoneyBalance + (object2 as Guest).CheckingAccount.MoneyBalance;

            return money1 == money2 ? 0 : money1 > money2 ? 1 : -1;
        }

        /// <summary>
        /// The AAge Sort Compare method.
        /// </summary>
        /// <param name="object1">The first object.</param>
        /// <param name="object2">The second object.</param>
        /// <returns>Returns the compare result.</returns>
        private static int AgeSortCompare(object object1, object object2)
        {
            double age1 = (object1 as Animal).Age;
            double age2 = (object2 as Animal).Age;

            return age1 == age2 ? 0 : age1 > age2 ? 1 : -1;
        }

        /// <summary>
        /// The add animals to zoo method.
        /// </summary>
        /// <param name="animals">The animals.</param>
        private void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            this.animals.ForEach(a =>
            {
                this.AddAnimal(a);
                this.AddAnimalsToZoo(a.Children);
            });
        }

        /// <summary>
        /// The Provide Vending Machine method.
        /// </summary>
        /// <returns>Returns animal snack machine.</returns>
        private VendingMachine ProvideVendingMachine()
        {
            return this.animalSnackMachine;
        }
    }
}