// Author: Shibin Shaji
// Date Created: Dec 3, 2025
// Description: This class manages JSON file handling.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace CarList.Data
{
    /// <summary>
    /// Provides functions for storing and loading vehicle data in a JSON file
    /// </summary>
    class JsonManager
    {
        /// <summary>
        /// Adds a new vehicle to JSON file
        /// </summary>
        /// <param name="newVehicle">Vehicle object to be serialized</param>
        /// <returns>True if vehicle is successfully written</returns>
        internal static bool Add(Vehicle newVehicle)
        {
            string jsonString = JsonSerializer.Serialize(newVehicle, newVehicle.GetType());
            File.AppendAllText("vehicle.json", jsonString + Environment.NewLine); //https://learn.microsoft.com/en-us/dotnet/api/system.environment.newline?view=net-10.0
            return true;
        }

        /// <summary>
        /// Nested class used to determine the vehicle type during deserialization
        /// </summary>
        class TypeVehicle
        {
            /// <summary>
            /// Get and set the stored vehicle type
            /// </summary>
            public string Type { get; set; }
        }
        /// <summary>
        /// Read all vehicles from JSON file and display them in a listview
        /// </summary>
        internal static List<Vehicle> AllVehicles
        {
            get
            {
                List<Vehicle> vehicleFromJson = [];
                using (StreamReader reader = new StreamReader("vehicle.json"))
                {
                    while (!reader.EndOfStream)
                    {


                        var vehicleString = reader.ReadLine();
                        var VehicleType = JsonSerializer.Deserialize<TypeVehicle>(vehicleString);
                        if (VehicleType.Type == "Car")
                        {
                            vehicleFromJson.Add(JsonSerializer.Deserialize<Car>(vehicleString));
                        }
                        else if (VehicleType.Type == "Plane")
                        {
                            vehicleFromJson.Add(JsonSerializer.Deserialize<Plane>(vehicleString));
                        }


                    }
                }
                return vehicleFromJson;
            }
        }
        /// <summary>
        /// Removes a vehicle from JSON file using its index
        /// </summary>
        /// <param name="index">index of the vehicle to remove</param>
        /// <returns>Returns true is vehicle was removed successfully</returns>
        internal static bool RemoveIndex(int index)
        {
            var vehicles = AllVehicles;
            if (index < 0 || index >= vehicles.Count)
                return false;
            vehicles.RemoveAt(index);
            using (StreamWriter writer = new StreamWriter("vehicle.json", false))
            {
                foreach (var vehicle in vehicles)
                {
                    var json = JsonSerializer.Serialize(vehicle, vehicle.GetType());
                    writer.WriteLine(json);
                }
            }
            return true;
        }
        /// <summary>
        /// Removes all vehicle saved in JSON file
        /// </summary>
        /// <returns>True if file was successfully cleared</returns>
        internal static bool RemoveAllVehicles()
        {
            try
            {               
                File.WriteAllText("vehicle.json", string.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
