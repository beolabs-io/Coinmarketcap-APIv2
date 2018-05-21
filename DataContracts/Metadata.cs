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

using Newtonsoft.Json;
using System;

namespace CoinmarketcapAPIv2.DataContracts
{
    public class Metadata
    {
        [JsonProperty("timestamp")]
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

        [JsonProperty("num_cryptocurrencies")]
        public int NumCryptocurrencies { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
    }
}
