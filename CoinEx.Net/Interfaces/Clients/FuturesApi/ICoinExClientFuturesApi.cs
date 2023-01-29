using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;

namespace CoinEx.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Client for accessing the CoinEx Futures API. 
    /// </summary>
    public interface ICoinExClientFuturesApi : IDisposable
    {
        /// <summary>
        /// The factory for creating requests. Used for unit testing
        /// </summary>
        IRequestFactory RequestFactory { get; set; }

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        ICoinExClientFuturesApiAccount Account { get; }

        /*
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        ICoinExClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        //ICoinExClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        //public IFuturesClient CommonFuturesClient { get; }
        */
    }
}
