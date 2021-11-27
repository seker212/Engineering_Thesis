using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    internal class UserApiManager
    {
        private IRestClient _restClient;
        private UserApiRequestFactory _requestFactory;

        internal TokenData GetToken(string tokenHash) //FIXME: Change model
        {
            var request = _requestFactory.GetTokenInfo(tokenHash);
            return ExecuteGetModel<TokenData>(request);
        }

        private T ExecuteGetModel<T>(IRestRequest request)
        {
            var response = _restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(response.Content) ?? throw new UserApiException();
                }
                catch (Exception ex)
                {
                    throw new UserApiException("", ex);
                }
            }
            throw new UserApiException();
        }
    }
}
