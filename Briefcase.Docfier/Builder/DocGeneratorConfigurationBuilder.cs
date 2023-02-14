using Briefcase.Docfier.Configurations;
using Briefcase.System.Builders;
using System;
using System.Reflection;

namespace Briefcase.Docfier.Builder
{
    public class DocGeneratorConfigurationBuilder : BuilderOf<DocGeneratorConfiguration>
    {
        public DocGeneratorConfigurationBuilder(string extension, string basePath)
        {
            EditOn(x => x.Extension, extension);
            EditOn(x => x.BasePath, basePath);
        }

        public DocGeneratorConfigurationBuilder ForAssemblyDoc(Action<DocConfigurationBuilder<Assembly>> action)
        {
            var docConfiguration = new DocConfigurationBuilder<Assembly>();
            action(docConfiguration);
            EditOn(x => x.AsssemblyDoc, docConfiguration.Build());
            return this;
        }
        public DocGeneratorConfigurationBuilder ForNamespaceDoc(Action<DocConfigurationBuilder<string>> action)
        {
            var docConfiguration = new DocConfigurationBuilder<string>();
            action(docConfiguration);
            EditOn(x => x.NamespaceDoc, docConfiguration.Build());
            return this;
        }
        public DocGeneratorConfigurationBuilder ForTypeDoc(Action<DocConfigurationBuilder<Type>> action)
        {
            var docConfiguration = new DocConfigurationBuilder<Type>();
            action(docConfiguration);
            EditOn(x => x.TypeDoc, docConfiguration.Build());
            return this;
        }
    }
}
