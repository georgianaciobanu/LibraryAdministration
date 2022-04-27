using Abp.Domain.Services;

namespace SimbioMed
{
    public abstract class SimbioMedDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected SimbioMedDomainServiceBase()
        {
            LocalizationSourceName = SimbioMedConsts.LocalizationSourceName;
        }
    }
}
