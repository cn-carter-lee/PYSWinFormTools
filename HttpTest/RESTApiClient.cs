using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web;

namespace HttpTest
{
    public class RESTApiClient
    {

        public virtual T Post<T>(string url, T data)
        {
            using (var client = new HttpClient())
            {
                var requestContent = CreateHttpContent<T>(data);
                var response = client.PostAsync(url, requestContent).Result;
                return this.ResolveDataOrThrow<T>(response);
            }
        }

        public virtual TReturn Post<TPost, TReturn>(string url, TPost data)
        {
            using (var client = new HttpClient())
            {
                var requestContent = CreateHttpContent<TPost>(data);
                var response = client.PostAsync(url, requestContent).Result;
                return this.ResolveDataOrThrow<TReturn>(response);
            }
        }


        private HttpContent CreateHttpContent<T>(T data)
        {
            JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver { IgnoreSerializableAttribute = true };
            return new ObjectContent<T>(data, jsonFormatter);
        }

        private T ResolveDataOrThrow<T>(HttpResponseMessage response)
        {
            HttpStatusCode statusCode = response.StatusCode;

            string source = response.Content.ReadAsStringAsync().Result;
            if (statusCode >= HttpStatusCode.OK && statusCode < HttpStatusCode.MultipleChoices)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(source);
                }
                catch (Exception e)
                {
                    try
                    {
                        // LoggerProvider.Current.Error(e, source);
                    }
                    catch
                    { }

                    return default(T);
                }
            }
            else if (statusCode >= HttpStatusCode.BadRequest)
            {
                // TODO: When 404, we don't throw exception;
                if (statusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }

                throw new HttpException((int)statusCode, source);
            }

            return default(T);
        }

    }
}
