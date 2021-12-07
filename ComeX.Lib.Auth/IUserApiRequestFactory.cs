using RestSharp;

namespace ComeX.Lib.Auth
{
    internal interface IUserApiRequestFactory
    {
        IRestRequest GetTokenInfo(string tokenHash);
    }
}