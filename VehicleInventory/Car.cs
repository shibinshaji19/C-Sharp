// Author: Shibin shaji
// Date created: October 30, 2025
// Description: This class manages properties of car
using CarList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarList
{
    using VehicleData = Data.JsonManager;


    /// <summary>
    /// Car class has properties of cars with make, model, year, price and condition
    /// </summary>
    internal class Car : Vehicle // Car class inherited from vehicle
    {
       


        public Car() : base() { }


        /// <summary>
        /// Parameterized constructor initializes new car object
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="price"></param>
        /// <param name="newStatus"></param>
        public Car(string make, string model, int year, decimal price, bool newStatus) : base()
        {

            Make = make;
            Model = model;
            Year = year;
            NewStatus = newStatus;
            Price = price;

            VehicleData.Add(this);

        }

        public override string Type { get => "Car"; }


        /// <summary>
        /// Returns a string containing year make and model of the car.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            return Year + " " + Make + " " + Model + ("Car");
        }
    }
}

