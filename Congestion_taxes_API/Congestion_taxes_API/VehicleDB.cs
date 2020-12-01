using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Congestion_taxes_API
{
    public class VehicleDB
    {

        private static VehicleDB instance;
        static List<Vehicle> vechicals = new List<Vehicle>();

        public void AddVechical(Vehicle vehicle)
        {
            vechicals.Add(vehicle); 
        }

        public Vehicle findVehical(string registrationNumber)
        {
            Vehicle vehicle = vechicals.First(v => v.registrationNumber.Equals(registrationNumber)); 
            return vehicle; 

        }

        public static VehicleDB getInstance()
        {
          if (instance == null)
          {
             instance = new VehicleDB();
          }
          return instance;
        }
    }
}
