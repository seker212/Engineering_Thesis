using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    internal class UserApiRequestFactory : IUserApiRequestFactory
    {
        public IRestRequest GetTokenInfo(string tokenHash)
        {
            var request = new RestRequest("api/Token/GetTokenInfo");
            request.Method = Method.GET;
            request.AddQueryParameter("tokenHash", tokenHash);
            return request;
        }
    }
}
