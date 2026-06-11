// Author: Shibin shaji
// Date created: October 30, 2025
// Description: This class manages properties of plane
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarList
{
    using VehicleData = Data.JsonManager;
    internal class Plane : Vehicle // Plane inherited from vehicle class
    {

        public Plane() : base() { }
        // variables required
        private int planeEngineCount;
        private decimal planeCargoCapacity;
        private decimal planeWingsSpan;
        /// <summary>
        /// Paramaterized constructor that initialize new plane object
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="price"></param>
        /// <param name="newStatus"></param>
        /// <param name="engineCount"></param>
        /// <param name="cargoCapacity"></param>
        /// <param name="wingsSpan"></param>
        public Plane(string make, string model, int year, decimal price, bool newStatus, int engineCount,decimal cargoCapacity, decimal wingsSpan) : base()
        {
            vehicleMake = make;
            vehicleModel = model;
            vehiclePrice = price;
            vehicleYear = year;
            vehicleNewStatus = newStatus;
            planeEngineCount = engineCount;
            planeCargoCapacity = cargoCapacity;
            planeWingsSpan = wingsSpan;
            VehicleData.Add(this);
        }

        /// <summary>
        /// Property that reads and set the value of cargo capacity
        /// </summary>
        public decimal CargoCapacity
        {
            get { return planeCargoCapacity; }
            set { 
                if (value < 0)
                { throw new ArgumentOutOfRangeException("Cargo capacity cannot be negative"); }
                planeCargoCapacity = value; }
        }
        /// <summary>
        /// Property that reads and set value of wings spas
        /// </summary>
        public decimal WingsSpan
        {
            get { return planeWingsSpan; }
            set {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Wings Span cannot be negative");
                }
                planeWingsSpan = value; }
        }
        /// <summary>
        /// property that reads and set value of engine count
        /// </summary>
        public int EngineCount
        {
            get { return planeEngineCount; }
            set { planeEngineCount = value; }
        }
        public override string Type { get => "Plane"; }
    }
}
