using TranslatorWithMSApi.Models;

namespace TranslatorWithMSApi.Interfaces
{
    public interface IAuthentication
    {
        AccessToken GetAccessToken();
    }
}
