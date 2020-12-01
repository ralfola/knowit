using System;
using System.Collections.Generic;
using System.Text;
using System.Linq; 

namespace Congestion_taxes_API
{
    public class Congestion_Taxes_API : Congestion_Taxes_API_interface
    {
        public void AddPassing(string registerNumber, DateTime dateTime)
        {
            //1. Get car
            Vehicle vehicle = VehicleDB.getInstance().findVehical(registerNumber);

            //2. Add passing if debitable vehical and debital day, otherwise data shall not be stored
            if (vehicle != null && TaxRules.IsVehicleTypeDebitable(vehicle) && TaxRules.isDayDebitable(dateTime))
            {
                vehicle.AddPassing(dateTime);
            }

        }

        public int GetBillingInfoForVehicalAndDay(string registerNumber, DateTime dateTime)
        {
            int debit = 0; 
            //1. Get car
            Vehicle vehicle = VehicleDB.getInstance().findVehical(registerNumber);
            List<Passing> passings = vehicle.passings;

            //Filtrera
            List<Passing> passingsOnDay = passings.Where( p => p.time.Date == dateTime.Date ).ToList();
            passingsOnDay = passingsOnDay.OrderBy(x => x.time).ToList();

            TimeSpan lastHourStart = new TimeSpan();
            int lastHourCost = 0; 
            foreach (Passing passing in passingsOnDay)
            {
                int currentPassingCost = TaxRules.GetCost(passing.time); 
               if (passing.time.TimeOfDay.Add(new TimeSpan(-1,0,0)) < lastHourStart)
                {
                    if (currentPassingCost > lastHourCost)
                    {
                        debit -= lastHourCost;
                        debit += currentPassingCost;
                        lastHourCost = currentPassingCost; 
                    }
                } else
                {
                    lastHourStart = passing.time.TimeOfDay;
                    debit += currentPassingCost;
                    lastHourCost = currentPassingCost; 
                }


            }

            if (debit > 60)
            {
                debit = 60; 
            }



            return debit; 
        }
    }
}
