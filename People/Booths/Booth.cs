﻿using System;
using System.Collections.Generic;
using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth.
    /// </summary>
    [Serializable]
    public abstract class Booth
    {
        /// <summary>
        /// The employee currently assigned to be the attendant of the booth.
        /// </summary>
        private Employee attendant;

        /// <summary>
        /// The Booth's items list.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// Initializes a new instance of the Booth class.
        /// </summary>
        /// <param name="attendant">The attendant for the booth.</param>
        public Booth(Employee attendant)
        {
            this.attendant = attendant;
            this.items = new List<Item>();
        }

        /// <summary>
        /// Gets the booth's attendant.
        /// </summary>
        protected Employee Attendant
        {
            get
            {
                return this.attendant;
            }
        }

        /// <summary>
        /// Gets the booths item's in the list.
        /// </summary>
        protected List<Item> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}