using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinEx.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures Asset Item
    /// </summary>
    public class CoinExFuturesAssetItem
    {
        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("available")]
        public string Available_balance { get; set; } = string.Empty;

        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("frozen")]
        public string Frozen_balance { get; set; } = string.Empty;

        /// <summary>
        /// Transfer amount
        /// </summary>
        [JsonProperty("transfer")]
        public string Transfer_balance { get; set; } = string.Empty;

        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("balance_total")]
        public string Total_balance { get; set; } = string.Empty;

        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("margin")]
        public string Margin_balance { get; set; } = string.Empty;

        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("profit_unreal")]
        public string Unrealized_profit { get; set; } = string.Empty;

    }
}
