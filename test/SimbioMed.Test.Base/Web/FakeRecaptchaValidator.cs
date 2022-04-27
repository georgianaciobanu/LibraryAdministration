using System.Threading.Tasks;
using SimbioMed.Security.Recaptcha;

namespace SimbioMed.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
