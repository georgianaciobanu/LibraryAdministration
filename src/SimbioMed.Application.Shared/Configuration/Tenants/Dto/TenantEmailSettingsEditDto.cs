using Abp.Auditing;
using SimbioMed.Configuration.Dto;

namespace SimbioMed.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}