using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappings(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappings(Assembly assembly)
        {
            var maps = assembly.GetTypes().Where(t => typeof(IMap).IsAssignableFrom(t) && !t.IsInterface).ToList();

            foreach (var map in maps)
            {
                var instance = Activator.CreateInstance(map);
                var methodInfo = map.GetMethod(nameof(IMap.ConfigureMap));

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
