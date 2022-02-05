using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scooters.Exception
{
    public class InvalidCompanyNameException : System.Exception
    {
        public InvalidCompanyNameException() : base("Company name is required.")
        {
            
        }
    }
}
