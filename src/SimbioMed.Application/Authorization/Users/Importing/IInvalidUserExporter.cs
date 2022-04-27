using System.Collections.Generic;
using SimbioMed.Authorization.Users.Importing.Dto;
using SimbioMed.Dto;

namespace SimbioMed.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
