using Abp.AspNetCore.Mvc.ViewComponents;

namespace SimbioMed.Web.Public.Views
{
    public abstract class SimbioMedViewComponent : AbpViewComponent
    {
        protected SimbioMedViewComponent()
        {
            LocalizationSourceName = SimbioMedConsts.LocalizationSourceName;
        }
    }
}