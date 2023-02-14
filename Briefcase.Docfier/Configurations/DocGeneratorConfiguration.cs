using System;
using System.Reflection;

namespace Briefcase.Docfier.Configurations
{
    public class DocGeneratorConfiguration
    {
        public string BasePath { get; set; }
        public string Extension { get; protected set; }
        public DocConfiguration<Assembly> AsssemblyDoc { get; protected set; }
        public DocConfiguration<string> NamespaceDoc { get; protected set; }
        public DocConfiguration<Type> TypeDoc { get; protected set; }

    }
}
