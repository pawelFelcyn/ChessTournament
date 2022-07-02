using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    internal interface IMap
    {
        void ConfigureMap(Profile profile);
    }
}
