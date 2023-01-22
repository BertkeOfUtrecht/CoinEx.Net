using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CoinEx.Net.Converters;
using CoinEx.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoinEx.Net.Objects.Models;
using CoinEx.Net.Objects.Models.Futures;
using CoinEx.Net.Objects.Models.Spot;
using CryptoExchange.Net.Converters;
using CoinEx.Net.Interfaces.Clients.FuturesApi;

namespace CoinEx.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class CoinExClientFuturesApiTrading : ICoinExClientFuturesApiTrading
    {
        private readonly CoinExClientFuturesApi _baseClient;

        internal CoinExClientFuturesApiTrading(CoinExClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExNewOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal leverage,
            decimal quantity,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            StopType? stopType = null,
            StopPriceType? stopPriceType = null,
            decimal? stopPrice = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)));
            parameters.AddParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stop", stopType != null ? JsonConvert.SerializeObject(stopType, new StopTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPriceType", stopPriceType != null ? JsonConvert.SerializeObject(stopPriceType, new StopPriceTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce != null ? JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)): null);
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());

            return await _baseClient.Execute<CoinExNewOrder>(_baseClient.GetUri("orders", 1), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute<CoinExCanceledOrders>(_baseClient.GetUri("orders/" + orderId, 1), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<CoinExCanceledOrders>(_baseClient.GetUri("orders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<CoinExCanceledOrders>> CancelAllStopOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<CoinExCanceledOrders>(_baseClient.GetUri("stopOrders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPaginated<CoinExFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new OrderStatusConverter(false)));
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<CoinExPaginated<CoinExFuturesOrder>>(_baseClient.GetUri("orders"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPaginated<CoinExFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<CoinExPaginated<CoinExFuturesOrder>>(_baseClient.GetUri("stopOrders"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinExFuturesOrder>>> GetClosedOrdersAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<CoinExFuturesOrder>>(_baseClient.GetUri("recentDoneOrders"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute<CoinExFuturesOrder>(_baseClient.GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            return await _baseClient.Execute<CoinExFuturesOrder>(_baseClient.GetUri("orders/byClientOid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPaginated<CoinExFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<CoinExPaginated<CoinExFuturesUserTrade>>(_baseClient.GetUri("fills"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinExFuturesUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<CoinExFuturesUserTrade>>(_baseClient.GetUri("recentFills"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        #endregion

    }
}
