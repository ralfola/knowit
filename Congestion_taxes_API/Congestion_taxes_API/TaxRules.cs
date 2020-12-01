using System;
using System.Collections.Generic;
using System.Text;

namespace Congestion_taxes_API
{
    class TaxRules 
    {
        internal static Boolean IsVehicleTypeDebitable(Vehicle vehicle)
        {

            if (vehicle.type == VehicleType.bluelightVehical ||
                vehicle.type == VehicleType.heavyBuss ||
                vehicle.type == VehicleType.diplomaticVehical ||
                vehicle.type == VehicleType.militaryVehical ||
                vehicle.type == VehicleType.motorbike)
            {
                return false; 
            }

            return true; 
        }

        internal static Boolean isDayDebitable(DateTime dateTime)
        {
            //The tax is not charged on public holidays, days before a public holiday and during the month of July.
            if (dateTime.Month == 7)
            {
                return false; 
            }
            
            if (dateTime.DayOfWeek == DayOfWeek.Saturday ||
                dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            /*Public holidays 2020. 
            Needs better implementation
            1 januari
            6 januari
            9 - 10 april
            13 april
            30 april
            1 maj
            20 - 21 maj
            5 juni
            19 juni
            30 oktober
            24 - 25 december
            31 december */
            if ((dateTime.Month == 1 && (dateTime.Day == 1 || dateTime.Day == 6)) ||
                (dateTime.Month == 4 && (dateTime.Day == 9 || dateTime.Day == 10 || dateTime.Day == 13|| dateTime.Day == 30)) ||
                (dateTime.Month == 5 && (dateTime.Day == 1 || dateTime.Day == 20 || dateTime.Day == 21)) ||
                (dateTime.Month == 6 && (dateTime.Day == 5 || dateTime.Day == 19)) ||
                (dateTime.Month == 10 && (dateTime.Day == 30)) ||
                (dateTime.Month == 12 && (dateTime.Day == 24 || dateTime.Day == 25 || dateTime.Day == 31)))
            {
                return false; 
            }

            return true; 

        }

        internal static int GetCost(DateTime dateTime)
        {
            int cost = 0; 
            TimeSpan time = dateTime.TimeOfDay; 

            if (time >= new TimeSpan(6,0,0) && time < new TimeSpan(6,30,0))
            {
                cost = 9; 
            }
            else if (time >= new TimeSpan(6, 30, 0) && time < new TimeSpan(7, 00, 0))
            {
                cost = 16; 
            }
            else if (time >= new TimeSpan(7, 00, 0) && time < new TimeSpan(8, 00, 0))
            {
                cost = 22;
            }
            else if (time >= new TimeSpan(8, 00, 0) && time < new TimeSpan(8, 30, 0))
            {
                cost = 16;
            }
            else if (time >= new TimeSpan(8, 30, 0) && time < new TimeSpan(15, 00, 0))
            {
                cost = 9;
            }
            else if (time >= new TimeSpan(15, 00, 0) && time < new TimeSpan(15, 30, 0))
            {
                cost = 16;
            }
            else if (time >= new TimeSpan(15, 30, 0) && time < new TimeSpan(17, 0, 0))
            {
                cost = 22;
            }
            else if (time >= new TimeSpan(17, 00, 0) && time < new TimeSpan(18, 0, 0))
            {
                cost = 16;
            }
            else if (time >= new TimeSpan(18, 00, 0) && time < new TimeSpan(18, 30, 0))
            {
                cost = 9;
            }

            return cost; 
        }
    }
}
