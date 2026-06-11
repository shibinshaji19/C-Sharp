// Author: Shibin shaji
// Date: October 29, 2025
// Description: This class manages property of a vehice base class
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
    internal abstract class Vehicle
    {

        /// <summary>
        /// Variables required for the car class
        /// </summary>
        protected static int vehicleCount = 0;

        protected string vehicleMake = string.Empty;
        protected string vehicleModel = string.Empty;
        protected int vehicleYear = 0;
        protected bool vehicleNewStatus = false;
        protected decimal vehiclePrice = 0.0M;
        protected int identificationNumber;


        /// <summary>
        /// Default constructor initializes new car object and gives auto-incremental ID
        /// </summary>
        public Vehicle()
        {
            vehicleCount++;
            identificationNumber = vehicleCount;
        }
        /// <summary>
        /// Property reads and set the value of make
        /// </summary>
        public string Make
        {
            get { return vehicleMake; }
            set { vehicleMake = value; }
        }
        /// <summary>
        /// Property reads and set the value of model
        /// </summary>
        public string Model
        {
            get { return vehicleModel; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Model cannot be empty!");
                vehicleModel = value; }
        }
        /// <summary>
        /// Propertty reads and set the value of year
        /// </summary>
        public int Year
        {
            get { return vehicleYear; }
            set { vehicleYear = value; }
        }
        /// <summary>
        /// Property reads and set the value of price
        /// </summary>

        public decimal Price
        {
            get { return vehiclePrice; }
            set { 
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Price cannot be negative!");
                vehiclePrice = value; }
        }
        /// <summary>
        /// Property reads and set value of condition
        /// </summary>
        public bool NewStatus
        {
            get { return vehicleNewStatus; }
            set { vehicleNewStatus = value; }
        }
        /// <summary>
        /// Property reads the identification number
        /// </summary>
        public int Identification { get { return identificationNumber; } }

        public static int Count { get { return vehicleCount; } }
        /// <summary>
        /// Return type of vehicle
        /// </summary>
        public abstract string Type { get; }

        public static List<Vehicle> List { get => VehicleData.AllVehicles; }

        /// <summary>
        /// Returns a string containing year make and model of the car.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            return Year + " " + Make + " " + Model;
        }
    }
}
