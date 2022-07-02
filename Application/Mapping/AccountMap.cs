using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    internal class AccountMap : IMap
    {
        public void ConfigureMap(Profile profile)
        {
            profile.CreateMap<RegisterDto, User>();
        }
    }
}
