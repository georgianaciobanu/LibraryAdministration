using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed
{
    [DependsOn(typeof(SimbioMedCoreSharedModule))]
    public class SimbioMedApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedApplicationSharedModule).GetAssembly());
        }
    }
}