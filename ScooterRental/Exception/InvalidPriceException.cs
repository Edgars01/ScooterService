using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scooters.Exception
{
    public class InvalidPriceException : System.Exception
    {
        public InvalidPriceException() : base("Invalid price provided")
        {
            
        }

        public InvalidPriceException(string message) : base(message)
        {
            
        }

        public InvalidPriceException(string message, System.Exception exception) : base(message, exception)
        {
            
        }
    }
}
