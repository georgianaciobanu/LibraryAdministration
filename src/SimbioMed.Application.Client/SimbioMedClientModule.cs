using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimbioMed
{
    public class SimbioMedClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimbioMedClientModule).GetAssembly());
        }
    }
}
