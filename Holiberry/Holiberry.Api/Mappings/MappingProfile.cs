using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Holiberry.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssemblies(Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly());
        }

        private void ApplyMappingsFromAssemblies(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                    .ToList();

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type);

                    // najbezpieczniejszy sposob -> w projekcie clean architecture wyszukowali najpierw metody w typie, a potem w interfejsie,
                    // latwo o blad jesli bedzie gdzies indziej w ktorejs klasie metoda mapping, 
                    // tu wystarczy zadbac, aby nie bylo przeladowan metody Mapping w tym interface, implementacja jest brana z interfejsu, 
                    // chyba ze podany typ tez implementuje ta metode, wtedy bierze z typu
                    var methodInfo = type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                    methodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }

    }
}