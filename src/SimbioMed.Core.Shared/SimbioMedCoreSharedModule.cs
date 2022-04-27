using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed
{
    public class SimbioMedCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedCoreSharedModule).GetAssembly());
        }
    }
}