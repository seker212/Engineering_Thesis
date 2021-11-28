using RestSharp;

namespace ComeX.Lib.Auth
{
    internal class UserApiRequestFactory : IUserApiRequestFactory
    {
        public IRestRequest GetTokenInfo(string tokenHash)
        {
            var request = new RestRequest("api/Token/GetTokenInfo", Method.GET);
            request.AddQueryParameter("tokenHash", tokenHash);
            return request;
        }
    }
}
