/*
 * A C#.NET library to consume www.coinmarketcap.com public web API
 * 
 * Author : beolabs.io / Benjamin Oms
 * Update : 21/05/2018
 * Github : https://github.com/beolabs-io/Coinmarketcap-APIv2
 * 
 * --- MIT LICENCE ---
 * 
 * Copyright (c) 2018 beolabs.io
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 */


using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace CoinmarketcapAPIv2.Endpoints
{
    public class Endpoint<T>
    {
        public string BaseUri     { get; set; }
        public string EndpointUri { get; set; }

        private JsonSerializerSettings _JsonSerializerSettings;

        public Endpoint(string baseUri, string endpointUri)
        {
            this.BaseUri     = baseUri;
            this.EndpointUri = endpointUri;

            _JsonSerializerSettings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling        = DateParseHandling.None,
                Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } }
            };
        }

        internal async Task<T> GetDataAsync()
        {
            return await GetDataAsync("", new Dictionary<string, string>());
        }

        internal async Task<T> GetDataAsync(string endpointComplement, Dictionary<string, string> endpointParameters)
        {
            var completeEndpointUri = (endpointComplement != string.Empty) ? string.Format("{0}/{1}", this.EndpointUri, endpointComplement) : this.EndpointUri;

            var parameterUri = string.Empty;
            if (endpointParameters.Keys.Count > 0)
            {
                parameterUri = "?";
                foreach (var paramKey in endpointParameters.Keys)
                {
                    var paramValue = endpointParameters[paramKey];
                    parameterUri += string.Format("{0}={1}&", paramKey, paramValue);
                }
                parameterUri = parameterUri.Remove(parameterUri.Length - 1);
            }

            var http     = new HttpClient();
            var uri      = string.Format("{0}/{1}/{2}", this.BaseUri, completeEndpointUri, parameterUri);
            var response = await http.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return default(T);

            var result   = await response.Content.ReadAsStringAsync();
            var data     = JsonConvert.DeserializeObject<T>(result, _JsonSerializerSettings);

            return data;
        }
    }
}

