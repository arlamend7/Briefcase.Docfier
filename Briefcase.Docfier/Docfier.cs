using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Briefcase.Docfier
{
    public class DocfierGenerator
    {
        public IEnumerable<DocGenerator> DocGenerators { get; protected set; }
        public DocfierGenerator()
        {
            DocGenerators = new DocGenerator[0]; 
        }
        public void Generate(Assembly assembly, string basePath)
        {
            DocGenerators.ToList()
                .ForEach(generator => generator.Generate(assembly, basePath));
        }
    }
}
