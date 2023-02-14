using Briefcase.Docfier.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Briefcase.Docfier
{
    public class DocGenerator
    {
        DocGeneratorConfiguration Configuration { get; }
        public DocGenerator(DocGeneratorConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void GenerateAssemblyDoc(Assembly assembly, out IEnumerable<Type> types)
        {
            types = assembly.ExportedTypes;
            GenerateByConfig(assembly, types, Configuration.AsssemblyDoc);
        }
        private void GenerateNamespaceDoc(KeyValuePair<string, IEnumerable<Type>> namespacesTypes)
        {
            GenerateByConfig(namespacesTypes.Key, namespacesTypes.Value, Configuration.NamespaceDoc);
        }
        private void GenerateTypeDoc(Type type)
        {
            IEnumerable<Type> relatedTypes = type.GetInterfaces();
            if(type.BaseType != null && type.BaseType != typeof(object))
                relatedTypes = relatedTypes.Append(type.BaseType);

            GenerateByConfig(type, relatedTypes, Configuration.TypeDoc);
        }
        private void GenerateByConfig<T>(T type, IEnumerable<Type> relatedTypes, DocConfiguration<T> configuration)
        {
            var path = Configuration.BasePath;
            if (configuration.GetPath != null)
                path += configuration.GetPath(type);
            var fileName = configuration.GetFileName(type);
            var content = configuration.GenerateTypeInfo.SelectMany(genenrateFunc => genenrateFunc(type, relatedTypes));

            SaveFile(path, fileName, content);
        }
    
        private void SaveFile(string path, string fileName, IEnumerable<string> content) 
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!fileName.EndsWith(Configuration.Extension))
                fileName += Configuration.Extension;

            var writer = File.CreateText(path + "/" + fileName);
            writer.Write(string.Join("\n", content));
            writer.Dispose();
        }
        public void Generate(Assembly assembly, string basePath)
        {
            Configuration.BasePath ??= basePath;
            GenerateAssemblyDoc(assembly, out IEnumerable<Type> types);

            Dictionary<string, IEnumerable<Type>> namespaces = types.Select(x => x.Namespace)
                                                  .Distinct()
                                                  .Where(x => x != null)
                                                  .ToDictionary(x => x, x => types.Where(y => y.Namespace == x));

            foreach (var item in namespaces)
            {
                GenerateNamespaceDoc(item);
            }

            foreach (var item in types)
            {
                GenerateTypeDoc(item);
            }
        }
    }
}
