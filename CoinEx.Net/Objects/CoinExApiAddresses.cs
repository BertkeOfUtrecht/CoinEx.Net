﻿namespace CoinEx.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the CoinEx clients
    /// </summary>
    public class CoinExApiAddresses
    {
        /// <summary>
        /// The address used by the CoinExClient for the rest API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the CoinExSocketClient for the socket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The address used by the KucoinClient for the futures API
        /// </summary>
        public string FuturesAddress { get; set; } = "";


        /// <summary>
        /// The default addresses to connect to the CoinEx.com API
        /// </summary>
        public static CoinExApiAddresses Default = new CoinExApiAddresses
        {
            RestClientAddress = "https://api.coinex.com/v1",
            SocketClientAddress = "wss://socket.coinex.com/",
            FuturesAddress = "https://api.coinex.com/perpetual/v1"
        };
    }
}
