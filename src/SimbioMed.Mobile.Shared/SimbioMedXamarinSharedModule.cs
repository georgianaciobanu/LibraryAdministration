using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed
{
    [DependsOn(typeof(SimbioMedClientModule), typeof(AbpAutoMapperModule))]
    public class SimbioMedXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedXamarinSharedModule).GetAssembly());
        }
    }
}