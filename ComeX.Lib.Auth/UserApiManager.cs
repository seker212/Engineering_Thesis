using ComeX.Lib.Common.UserDatabaseAPI;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace ComeX.Lib.Auth
{
    internal class UserApiManager : IUserApiManager
    {
        private IRestClient _restClient;
        private IUserApiRequestFactory _requestFactory;

        public UserApiManager(IUserApiRequestFactory requestFactory, Uri baseUserApiUri)
        {
            _requestFactory = requestFactory;
            _restClient = new RestClient(baseUserApiUri);
        }

        public TokenMessage GetToken(string tokenHash)
        {
            var request = _requestFactory.GetTokenInfo(tokenHash);
            return ExecuteGetModel<TokenMessage>(request);
        }

        private T ExecuteGetModel<T>(IRestRequest request)
        {
            var response = _restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(response.Content) ?? throw new UserApiException(); //FIXME: Change exception message
                }
                catch (Exception ex)
                {
                    throw new UserApiException("", ex); //FIXME: Change exception message
                }
            }
            throw new UserApiException(); //FIXME: Change exception message
        }
    }
}
