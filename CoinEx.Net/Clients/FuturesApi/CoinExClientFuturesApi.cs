using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CoinEx.Net.Enums;
using CoinEx.Net.Interfaces.Clients.FuturesApi;
using CoinEx.Net.Objects;
using CoinEx.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoinEx.Net.Interfaces.Clients.SpotApi;
using CoinEx.Net.Clients.SpotApi;
using CoinEx.Net.Objects.Models;

namespace CoinEx.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="ICoinExClientFuturesApi" />
    public class CoinExClientFuturesApi : RestApiClient, ICoinExClientFuturesApi, IFuturesClient
    {
        #region fields
        private readonly CoinExClientOptions _options;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Spot Api");

        #endregion

        /// <inheritdoc />
        public string ExchangeName => "CoinEx";

        #region Api clients
        /// <inheritdoc />
        public ICoinExClientFuturesApiAccount Account { get; }

        #endregion


        #region ctor
        internal CoinExClientFuturesApi(Log log, CoinExClientOptions options) :
            base(log, options, options.FuturesApiOptions)
        {
            _options = options;

            Account = new CoinExClientFuturesApiAccount(this);

            manualParseError = true;
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;

        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinExAuthenticationProvider(credentials, _options.NonceProvider ?? new CoinExNonceProvider()); //TODO: Nonce provider may not be applicable in futures


        #region methods
        #region private
        internal async Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false) where T : class
            => GetResult(await SendRequestAsync<CoinExApiResult<T>>(uri, method, ct, parameters, signed).ConfigureAwait(false));
        internal async Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
            => GetResult(await SendRequestAsync<CoinExApiResult<object>>(uri, method, ct, parameters, signed).ConfigureAwait(false));

        internal async Task<WebCallResult<CoinExPagedResult<T>>> ExecutePaged<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false) where T : class
            => GetResult(await SendRequestAsync<CoinExApiResult<CoinExPagedResult<T>>>(uri, method, ct, parameters, signed).ConfigureAwait(false));

        internal Uri GetUrl(string endpoint)
        {
            return new Uri(BaseAddress.AppendPath(endpoint));
        }

        #endregion
        #endregion

    }
}
