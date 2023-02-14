using Briefcase.System.Builders;
using System;

namespace Briefcase.Docfier.Builder
{
    public class DocfierBuilder : BuilderOf<DocfierGenerator>
    {
        public DocfierBuilder AndFor(string extension, string basePath, Action<DocGeneratorConfigurationBuilder> configurationFunc)
        {
            var generatorConfiguration = new DocGeneratorConfigurationBuilder(extension, basePath);
            configurationFunc(generatorConfiguration);

            var generator = new DocGenerator(generatorConfiguration.Build());

            EditOn(x => x.DocGenerators, generator);
            return this;
        }
        public DocfierBuilder AndFor(string extension, Action<DocGeneratorConfigurationBuilder> configurationFunc)
        {
            return AndFor(extension, null, configurationFunc);
        }
        public static DocfierBuilder For(string extension, string basePath, Action<DocGeneratorConfigurationBuilder> configurationFunc)
        {
            return new DocfierBuilder().AndFor(extension, basePath, configurationFunc);
        }
        public static DocfierBuilder For(string extension, Action<DocGeneratorConfigurationBuilder> configurationFunc)
        {
            return new DocfierBuilder().AndFor(extension, null, configurationFunc);
        }
    }
}
