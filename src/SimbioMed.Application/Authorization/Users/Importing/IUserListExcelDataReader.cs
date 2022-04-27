using System.Collections.Generic;
using SimbioMed.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace SimbioMed.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
