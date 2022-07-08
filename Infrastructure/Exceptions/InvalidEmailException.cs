using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class InvalidEmailException : BadRequestException
    {
        public InvalidEmailException() : base("Invalid email")
        {
        }
    }
}
