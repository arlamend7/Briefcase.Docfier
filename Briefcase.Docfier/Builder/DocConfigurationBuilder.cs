using Briefcase.Docfier.Configurations;
using Briefcase.System.Builders;
using System;
using System.Collections.Generic;

namespace Briefcase.Docfier.Builder
{
    public class DocConfigurationBuilder<T> : BuilderOf<DocConfiguration<T>>
    {

        public DocConfigurationBuilder<T> FileNameResolver(Func<T, string> resolver)
        {
            EditOn(x => x.GetFileName, resolver);

            return this;
        }
        public DocConfigurationBuilder<T> SetFileName(string filename)
        {
            EditOn(x => x.GetFileName, (x) => filename);

            return this;
        }
        public DocConfigurationBuilder<T> PathResolver(Func<T, string> resolver)
        {
            EditOn(x => x.GetPath, resolver);
            return this;
        }
        public DocConfigurationBuilder<T> SetPath(string path)
        {
            EditOn(x => x.GetPath, (x) => path);
            return this;
        }
        public DocConfigurationBuilder<T> AddContentCreator(Func<T, IEnumerable<Type>, IEnumerable<string>> contentCreator)
        {
            EditOn(x => x.GenerateTypeInfo, contentCreator);
            return this;
        }
    }
}
