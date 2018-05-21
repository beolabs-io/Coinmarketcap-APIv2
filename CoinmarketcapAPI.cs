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
using CoinmarketcapAPIv2.Endpoints;
using CoinmarketcapAPIv2.DataContracts.Listings;
using CoinmarketcapAPIv2.DataContracts.Ticker;
using CoinmarketcapAPIv2.DataContracts.Global;

namespace CoinmarketcapAPIv2
{
    public class CoinmarketcapAPI
    {
        private string _BaseUri;
        public string   BaseUri
        {
            get { return _BaseUri; }

            set
            {
                _BaseUri = value;
                this.Listings.BaseUri = value;
                this.Ticker.BaseUri   = value;
            }
        }

        public Endpoint<Listings>       Listings       { get; set; }
        public Endpoint<Ticker>         Ticker         { get; set; }
        public Endpoint<TickerSpecific> TickerSpecific { get; set; }
        public Endpoint<Global>         Global         { get; set; }

        public CoinmarketcapAPI()
        {
            _BaseUri = "https://api.coinmarketcap.com/v2";

            this.Listings        = new Endpoint<Listings>       (this.BaseUri, "listings");
            this.Ticker          = new Endpoint<Ticker>         (this.BaseUri, "ticker");
            this.TickerSpecific  = new Endpoint<TickerSpecific> (this.BaseUri, "ticker");
            this.Global          = new Endpoint<Global>         (this.BaseUri, "global");
        }

        public async Task<Listings> GetListings()
        {
            return await this.Listings.GetDataAsync();
        }


        public async Task<Ticker> GetTicker(int start = 0, int limit = 100, string quoteCurrencySymbol = "")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("start",   start.ToString());
            parameters.Add("limit",   limit.ToString());

            if (quoteCurrencySymbol != null && quoteCurrencySymbol != "") parameters.Add("convert", quoteCurrencySymbol);

            return await this.Ticker.GetDataAsync("", parameters);
        }

        public async Task<TickerSpecific> GetTickerSecific(string id, string quoteCurrencySymbol = "")
        {
            var endpointComplement = id;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (quoteCurrencySymbol != null && quoteCurrencySymbol != "") parameters.Add("convert", quoteCurrencySymbol);

            return await this.TickerSpecific.GetDataAsync(endpointComplement, parameters);
        }

        public async Task<Global> GetGlobal()
        {
            return await this.Global.GetDataAsync("", new Dictionary<string, string>());
        }

        public async Task<Global> GetGlobal(string quoteCurrencySymbol)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (quoteCurrencySymbol != null && quoteCurrencySymbol != "") parameters.Add("convert", quoteCurrencySymbol);

            return await this.Global.GetDataAsync("", parameters);
        }
    }
}
