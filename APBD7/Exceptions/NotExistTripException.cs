using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Exceptions
{
    public class NotExistTripException : Exception
    {
        public NotExistTripException(string message) : base(message)
        {

        }
    }
}
