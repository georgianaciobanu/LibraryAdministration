using Abp.AutoMapper;
using SimbioMed.Organizations.Dto;

namespace SimbioMed.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}