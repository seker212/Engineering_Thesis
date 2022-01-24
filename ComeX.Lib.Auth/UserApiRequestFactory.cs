using RestSharp;

namespace ComeX.Lib.Auth
{
    internal class UserApiRequestFactory : IUserApiRequestFactory
    {
        public RestRequest GetTokenInfo(string tokenHash)
        {
            var request = new RestRequest("api/Auth", Method.Get);
            request.AddQueryParameter("tokenHash", tokenHash);
            return request;
        }
    }
}
