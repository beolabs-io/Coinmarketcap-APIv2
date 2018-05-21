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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinmarketcapAPIv2.DataContracts.Ticker
{
    public class Ticker
    {
        [JsonProperty("data")]
        public Dictionary<string, DataItem> Data { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class DataItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("website_slug")]
        public string WebsiteSlug { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("circulating_supply")]
        public float CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public float TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public float? MaxSupply { get; set; }

        [JsonProperty("quotes")]
        public Dictionary<string, TickerQuote> Quotes { get; set; }

        [JsonProperty("last_updated")]
        public long Timestamp { get; set; }

        [JsonIgnore]
        public DateTime LastUpdated
        {
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                long unixTimeStampInTicks = (Timestamp * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
            }
        }
    }

    public class TickerQuote
    {
        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("volume_24h")]
        public float Volume24H { get; set; }

        [JsonProperty("market_cap")]
        public float MarketCap { get; set; }

        [JsonProperty("percent_change_1h")]
        public float PercentChange1h { get; set; }

        [JsonProperty("percent_change_24h")]
        public float PercentChange24h { get; set; }

        [JsonProperty("percent_change_7d")]
        public float PercentChange7d { get; set; }
    }
}
