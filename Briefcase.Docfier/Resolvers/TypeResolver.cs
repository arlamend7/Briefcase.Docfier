using System;
using System.Linq;

namespace Briefcase.Docfier.Resolvers
{
    public class TypeResolver
    {
        public static string ReadableName(Type type)
        {
            if(type is null)
                return null;
            if(!type.GenericTypeArguments.Any()) return type.Name;

            var name = type.Name[..^2];

            var readleNames = type.GenericTypeArguments.Select(x => ReadableName(x));
            return name + "<" + string.Join(", ", readleNames) + ">";
        }
        public static string ReadableName<T>() 
        { 
            return ReadableName(typeof(T));
        }
    }
}
