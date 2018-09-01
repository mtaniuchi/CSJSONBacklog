/* See the file "LICENSE" for the full license governing this code. */

using System;
using CSJSONBacklog.Communicator.Serializers;
using CSJSONBacklog.Model;
using Newtonsoft.Json;
using RestSharp;

namespace CSJSONBacklog.Communicator
{
    public abstract class AbstractCommunicator : AbstractParameter, ICommunicator
    {
        public string SpaceKey { get; private set; }
        public string ApiKey { get; private set; }

        protected AbstractCommunicator(string spaceKey, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(spaceKey)) { throw new ArgumentNullException(spaceKey); }
            if (string.IsNullOrWhiteSpace(apiKey)) { throw new ArgumentNullException(apiKey); }

            SpaceKey = spaceKey;
            ApiKey = apiKey;
        }

        protected string BaseUri
        {
            get { return string.Format("https://{0}.backlog.com/", SpaceKey); }
        }

        protected T GetT<T>(string uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET) 
            {
                RequestFormat = DataFormat.Json,
            };

            var response = client.Execute(request);
            var content = response.Content;

            T result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                var errors = JsonConvert.DeserializeObject<ErrorMessages>(content);
                if (errors.HasError)
                {
                    throw new Exception(errors.ToString());
                }
            }

            return result;
        }

        protected T PatchT<T>(string resource, T value)
        {
            var client = new RestClient(BaseUri);
            var request = new RestRequest(Method.PATCH)
            {
                RequestFormat = DataFormat.Json,
                Resource = resource,
            };
            request.AddQueryParameter("apiKey", ApiKey);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.JsonSerializer = new PatchJsonSerializer { ContentType = @"application/json" };
            request.AddJsonBody(value);

            var response = client.Execute(request);
            var content = response.Content;

            var errors = JsonConvert.DeserializeObject<ErrorMessages>(content);
            if (errors.HasError)
            {
                throw new Exception(errors.ToString());
            }

            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }
}
