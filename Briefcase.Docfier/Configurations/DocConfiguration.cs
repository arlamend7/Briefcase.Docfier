using System;
using System.Collections.Generic;

namespace Briefcase.Docfier.Configurations
{
    public class DocConfiguration<T>
    {
        public Func<T, string> GetFileName { get; protected set; }
        public Func<T , string> GetPath { get; protected set; }
        public IEnumerable<Func<T, IEnumerable<Type>, IEnumerable<string>>> GenerateTypeInfo { get; protected set; }
        public DocConfiguration()
        {
            GenerateTypeInfo = new Func<T, IEnumerable<Type>, IEnumerable<string>>[0];
        }
    }
}
