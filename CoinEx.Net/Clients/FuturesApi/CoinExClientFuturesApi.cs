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
using Newtonsoft.Json.Linq;

namespace CoinEx.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="ICoinExClientFuturesApi" />
    public class CoinExClientFuturesApi : RestApiClient, ICoinExClientFuturesApi
    {
        #region fields
        private readonly CoinExClientOptions _options;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Spot Api");

        #endregion

        /// <inheritdoc />
        public string ExchangeName => "CoinEx";

        
        /// <inheritdoc />
        public ICoinExClientFuturesApiAccount Account { get; }

        

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
            => new CoinExAuthenticationProvider(credentials,null); //TODO: Nonce provider may not be applicable in futures


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


        #region utility_method_implementations


        /// <inheritdoc />
        protected override Task<ServerError?> TryParseErrorAsync(JToken data)
        {
            if (data["code"] != null && data["message"] != null)
            {
                if (data["code"]!.Value<int>() != 0)
                {
                    return Task.FromResult((ServerError?)ParseErrorResponse(data));
                }
            }

            return Task.FromResult((ServerError?)null);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["code"] == null || error["message"] == null)
                return new ServerError(error.ToString());

            return new ServerError((int)error["code"]!, (string)error["message"]!);
        }


        private static WebCallResult<T> GetResult<T>(WebCallResult<CoinExApiResult<T>> result) where T : class
        {
            if (result.Error != null || result.Data == null)
                return result.AsError<T>(result.Error ?? new UnknownError("No data received"));

            return result.As(result.Data.Data);
        }

        private static WebCallResult GetResult(WebCallResult<CoinExApiResult<object>> result)
        {
            if (result.Error != null || result.Data == null)
                return result.AsDatalessError(result.Error ?? new UnknownError("No data received"));

            return result.AsDataless();
        }

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset() => TimeSyncState.TimeOffset;

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo() => new TimeSyncInfo(_log, _options.FuturesApiOptions.AutoTimestamp, _options.FuturesApiOptions.TimestampRecalculationInterval, TimeSyncState);

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync() => Task.FromResult(new WebCallResult<DateTime>(null, null, null, null, null, null, null, null, DateTime.UtcNow, null));

        #endregion

    }
}
