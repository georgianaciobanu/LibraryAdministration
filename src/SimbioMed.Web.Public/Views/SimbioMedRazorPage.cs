using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace SimbioMed.Web.Public.Views
{
    public abstract class SimbioMedRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected SimbioMedRazorPage()
        {
            LocalizationSourceName = SimbioMedConsts.LocalizationSourceName;
        }
    }
}
