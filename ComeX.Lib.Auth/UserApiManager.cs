using ComeX.Lib.Common.UserDatabaseAPI;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace ComeX.Lib.Auth
{
    internal class UserApiManager : IUserApiManager
    {
        private readonly RestClient _restClient;
        private readonly IUserApiRequestFactory _requestFactory;

        public UserApiManager(IUserApiRequestFactory requestFactory, Uri baseUserApiUri)
        {
            _requestFactory = requestFactory;
            _restClient = new RestClient(baseUserApiUri);
        }

        public TokenDataModel GetToken(string tokenHash)
        {
            var request = _requestFactory.GetTokenInfo(tokenHash);
            return ExecuteGetModel<TokenDataModel>(request);
        }

        private T ExecuteGetModel<T>(RestRequest request)
        {
            var responseTask = _restClient.ExecuteAsync(request);
            responseTask.Wait();
            var response = responseTask.Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(response.Content) ?? throw new UserApiException("Deserialization to model returned null.");
                }
                catch (Exception ex) when (ex.GetType() != typeof(UserApiException))
                {
                    throw new UserApiException("Could not deserialize http response content.", ex);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UserApiException("Unauthorised", response.StatusCode);
            throw new UserApiException($"User WebAPI returned unsupported status code: {response.StatusCode}");
        }
    }
}
