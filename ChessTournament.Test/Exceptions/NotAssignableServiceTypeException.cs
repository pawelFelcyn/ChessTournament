using System;

namespace ChessTournament.Test.Exceptions
{
    public class NotAssignableServiceTypeException : ArgumentException
    {
        public NotAssignableServiceTypeException(Type serviceType, Type toRegisterType)
            : base($"Service of type {toRegisterType.FullName} canno't be registered as {serviceType.FullName}, becuase it's not assignable from this type.")
        {

        }
    }
}
