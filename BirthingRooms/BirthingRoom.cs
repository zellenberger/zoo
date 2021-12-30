using System;
using System.Collections.Generic;
using People;
using Reproducers;

namespace BirthingRooms
{
    /// <summary>
    /// The class which is used to represent a birthing room.
    /// </summary>
    [Serializable]
    public class BirthingRoom
    {
        /// <summary>
        /// The minimum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MinTemperature = 35.0;

        /// <summary>
        /// The maximum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MaxTemperature = 95.0;

        /// <summary>
        /// The initial temperature of the birthing room.
        /// </summary>
        private readonly double initialTemperature = 77.0;

        /// <summary>
        /// The current temperature of the birthing room.
        /// </summary>
        private double temperature;

        /// <summary>
        /// The employee currently assigned to be the vet of the birthing room.
        /// </summary>
        private Employee vet;

        /// <summary>
        /// Initializes a new instance of the BirthingRoom class.
        /// </summary>
        /// <param name="vet">The employee to be the birthing room's vet.</param>
        public BirthingRoom(Employee vet)
        {
            this.Temperature = this.initialTemperature;
            this.vet = vet;
            this.PregnantAnimals = new Queue<IReproducer>();
        }

        /// <summary>
        /// Gets or sets the on temperature change.
        /// </summary>
        public Action<double, double> OnTemperatureChange { get; set; }

        /// <summary>
        /// Gets the pregnant animals.
        /// </summary>
        public Queue<IReproducer> PregnantAnimals { get; private set; }

        /// <summary>
        /// Gets or sets the birthing room's temperature.
        /// </summary>
        public double Temperature
        {
            get
            {
                return this.temperature;
            }

            set
            {
                // If the value is in range...
                if (value < BirthingRoom.MinTemperature)
                {
                    throw new ArgumentOutOfRangeException("temperature", $"The temperature must be above {BirthingRoom.MinTemperature} degrees.");
                }
                else if (value > BirthingRoom.MaxTemperature)
                {
                    throw new ArgumentOutOfRangeException("temperature", $"The temperature must be below {BirthingRoom.MaxTemperature} degrees.");
                }
                else
                {
                    if (value >= BirthingRoom.MinTemperature && value <= BirthingRoom.MaxTemperature)
                    {
                        double previousTemp = this.temperature;

                        this.temperature = value;

                        if (this.OnTemperatureChange != null)
                        {
                            this.OnTemperatureChange(previousTemp, this.temperature);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Births a reproducer.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer BirthAnimal(IReproducer reproducer)
        {
            IReproducer baby = null;

            // If the reproducer is present and is pregnant...
            if (reproducer != null && reproducer.IsPregnant)
            {
                baby = this.vet.DeliverAnimal(reproducer);

                // Increase the temperature due to the heat generated from birthing.
                this.Temperature += 0.5;
            }

            return baby;
        }
    }
}