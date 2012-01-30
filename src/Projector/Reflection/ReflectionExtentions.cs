using System;
using System.Collections.Generic;
using System.Linq;

namespace Projector.Reflection
{
    public static class ReflectionExtentions
    {
        public static IEnumerable<TReturn> CreateInstancesOfNestedTypes<TReturn>(this Type instance)
        {
            return instance.GetNestedTypes()
                .Where(x => typeof(TReturn).IsAssignableFrom(x))
                .Select(x => x.CreateInstanceOfType()).Cast<TReturn>();
        }

        public static T CreateInstanceOf<T>()
        {
            return (T)CreateInstanceOfType(typeof(T));
        }

        public static object CreateInstanceOfType(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}