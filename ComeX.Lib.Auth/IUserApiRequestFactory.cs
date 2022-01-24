using RestSharp;

namespace ComeX.Lib.Auth
{
    internal interface IUserApiRequestFactory
    {
        RestRequest GetTokenInfo(string tokenHash);
    }
}