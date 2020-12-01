using System;
using System.Collections.Generic;
using System.Text;

namespace Congestion_taxes_API
{
    public class Vehicle
    {
        public Vehicle(string registrationNumber, VehicleType type)
        {
            this.registrationNumber = registrationNumber;
            this.type = type; 

        }
        public string registrationNumber
        {
            get;
            set;
        }

        public VehicleType type
        {
            get;
            set;

        }

        internal List<Passing> passings = new List<Passing>(); 

        internal void AddPassing(DateTime dateTime)
        {
            passings.Add(new Passing(dateTime)); 

        }


    }
    public enum VehicleType
    {
        motorbike,
        personalCar,
        lightTruck, 
        truck, 
        lightBuss, // less than 8 tons
        heavyBuss,// more than 8 tons
        militaryVehical,
        bluelightVehical, 
        diplomaticVehical

    }

}
