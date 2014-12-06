﻿/* See the file "LICENSE" for the full license governing this code. */

using System;
using CSJSONBacklog.Communicator.Serializers;
using CSJSONBacklog.Model;
using Newtonsoft.Json;
using RestSharp;

namespace CSJSONBacklog.Communicator
{
    public abstract class AbstractCommunicator : AbstractParameter, ICommunicator
    {
        public string Spacename { get; private set; }
        public string ApiKey { get; private set; }

        protected AbstractCommunicator(string spacename, string apiKey)
        {
            Spacename = spacename;
            ApiKey = apiKey;
        }

        protected string BaseUri
        {
            get { return string.Format("https://{0}.backlog.jp/", Spacename); }
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
            
            var errors = JsonConvert.DeserializeObject<ErrorMessages>(content);
            if (errors.HasError)
            {
                throw new Exception(errors.ToString());
            }

            return JsonConvert.DeserializeObject<T>(content);
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

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
