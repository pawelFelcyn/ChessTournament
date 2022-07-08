using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidPasswordException : BadRequestException
    {
        public InvalidPasswordException() : base("Invalid password")
        {
        }
    }
}
