using Abp.AspNetCore.Mvc.Views;

namespace SimbioMed.Web.Views
{
    public abstract class SimbioMedRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected SimbioMedRazorPage()
        {
            LocalizationSourceName = SimbioMedConsts.LocalizationSourceName;
        }
    }
}
