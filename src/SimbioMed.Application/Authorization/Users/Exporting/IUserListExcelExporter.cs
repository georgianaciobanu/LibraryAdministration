using System.Collections.Generic;
using SimbioMed.Authorization.Users.Dto;
using SimbioMed.Dto;

namespace SimbioMed.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}