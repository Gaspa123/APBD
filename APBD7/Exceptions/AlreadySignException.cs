using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Exceptions
{
    public class AlreadySignException :Exception
    {
        public AlreadySignException(string message) : base(message)
        {
        }
    }
}
