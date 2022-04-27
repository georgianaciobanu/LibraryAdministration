using Microsoft.Extensions.Configuration;

namespace SimbioMed.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
