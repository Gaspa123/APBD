using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Exceptions
{
    public class NoDoctorWithIdException : Exception
    {
        public NoDoctorWithIdException(string message) : base(message)
        {
        }
    }
}
