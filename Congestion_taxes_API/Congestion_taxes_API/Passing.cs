using System;
using System.Collections.Generic;
using System.Text;

namespace Congestion_taxes_API
{
    public class Passing
    {
        public DateTime time
        {
            get;
            set;
        }
        
        public Passing(DateTime time)
        {
            this.time = time; 
        }
    }
}
