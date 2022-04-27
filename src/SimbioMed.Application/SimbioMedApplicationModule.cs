using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SimbioMed.Authorization;

namespace SimbioMed
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(SimbioMedApplicationSharedModule),
        typeof(SimbioMedCoreModule)
        )]
    public class SimbioMedApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedApplicationModule).GetAssembly());
        }
    }
}