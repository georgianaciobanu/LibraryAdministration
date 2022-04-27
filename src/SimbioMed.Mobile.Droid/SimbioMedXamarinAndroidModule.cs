using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed
{
    [DependsOn(typeof(SimbioMedXamarinSharedModule))]
    public class SimbioMedXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedXamarinAndroidModule).GetAssembly());
        }
    }
}