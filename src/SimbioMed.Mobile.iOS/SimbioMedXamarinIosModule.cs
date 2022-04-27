using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed
{
    [DependsOn(typeof(SimbioMedXamarinSharedModule))]
    public class SimbioMedXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedXamarinIosModule).GetAssembly());
        }
    }
}