using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed.Startup
{
    [DependsOn(typeof(SimbioMedCoreModule))]
    public class SimbioMedGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}