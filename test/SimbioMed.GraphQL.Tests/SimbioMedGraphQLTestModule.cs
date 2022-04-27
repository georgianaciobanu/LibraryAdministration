using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SimbioMed.Configure;
using SimbioMed.Startup;
using SimbioMed.Test.Base;

namespace SimbioMed.GraphQL.Tests
{
    [DependsOn(
        typeof(SimbioMedGraphQLModule),
        typeof(SimbioMedTestBaseModule))]
    public class SimbioMedGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedGraphQLTestModule).GetAssembly());
        }
    }
}