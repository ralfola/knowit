using System;
using System.Collections.Generic;
using System.Text;

namespace Congestion_taxes_API
{
    public interface Congestion_Taxes_API_interface 
    {
        public abstract void AddPassing(string registerNumber, DateTime dateTime);

        public abstract int GetBillingInfoForVehicalAndDay(string registerNumber, DateTime dateTime); 
    }
}
