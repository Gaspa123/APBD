using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Exceptions
{
    public class DeleteClientException : Exception
    {
        public DeleteClientException(string message) : base(message)
        {

        }
    }
}