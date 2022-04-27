using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using SimbioMed.Dto;

namespace SimbioMed.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
