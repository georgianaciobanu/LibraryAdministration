using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using SimbioMed.Queries.Container;
using System;

namespace SimbioMed.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}