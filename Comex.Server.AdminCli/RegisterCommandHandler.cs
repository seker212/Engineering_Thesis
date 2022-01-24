using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comex.Server.AdminCli
{
    class RegisterCommandHandler
    {

        public static void ChechUriSchema(string uri)
        {
            var ur = new Uri(uri);
            if (!(ur.Scheme == "http" || ur.Scheme == "https"))
                throw new Exception("Uri scheme is wrong");
            if (string.IsNullOrWhiteSpace(ur.Host))
                throw new Exception("Uri host is empty");
            if (ur.Port < 1024)
                throw new Exception("Uri port is wrong");
        }

        public void HandleRegisterCommand(string name, string uri)
        {
            var task = Task.Run(() => RegisterServer(name, uri));
            task.Wait();
        }

        private async void RegisterServer(string name, string uri)
        {
            var request = new RestRequest()
            {
                Resource = "/Server",
                Method = Method.Post
            }
            .AddQueryParameter("name", name)
            .AddQueryParameter("url", uri);

            using var client = new RestClient("https://comx.molly.ovh:40443/api");
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new Exception(response.Content);
        }
    }
}
