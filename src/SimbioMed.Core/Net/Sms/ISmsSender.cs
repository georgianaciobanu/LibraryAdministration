using System.Threading.Tasks;

namespace SimbioMed.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}