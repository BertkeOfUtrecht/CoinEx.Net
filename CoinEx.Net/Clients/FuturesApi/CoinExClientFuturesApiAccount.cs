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
using CoinEx.Net.Objects.Internal;
using CoinEx.Net.Objects.Models;
using CoinEx.Net.Objects.Models.Futures;
using CryptoExchange.Net.Converters;
using CoinEx.Net.Interfaces.Clients.FuturesApi;

namespace CoinEx.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class CoinExClientFuturesApiAccount : ICoinExClientFuturesApiAccount
    {
        private readonly CoinExClientFuturesApi _baseClient;

        internal CoinExClientFuturesApiAccount(CoinExClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Account
        /// <inheritdoc />
        public async Task<WebCallResult<CoinExAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await _baseClient.Execute<CoinExAccountOverview>(_baseClient.GetUri("account-overview"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExPaginatedSlider<CoinExAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new TransactionTypeConverter(false)));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<CoinExPaginatedSlider<CoinExAccountTransaction>>(_baseClient.GetUri("transaction-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }*/
        #endregion

        #region Deposit
        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await _baseClient.Execute<CoinExDepositAddress>(_baseClient.GetUri("deposit-address"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPaginated<CoinExDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new DepositStatusConverter(false)));
            return await _baseClient.Execute<CoinExPaginated<CoinExDeposit>>(_baseClient.GetUri("deposit-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }*/
        #endregion

        #region Withdrawal

        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExFuturesWithdrawalQuota>> GetWithdrawalLimitAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            return await _baseClient.Execute<CoinExFuturesWithdrawalQuota>(_baseClient.GetUri("withdrawals/quotas"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExNewWithdrawal>> WithdrawAsync(string asset, string address, decimal quantity, bool? isInner = null, string? remark = null, string? chain = null, string? memo = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddParameter("address", address);
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isInner", isInner?.ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", chain);
            parameters.AddOptionalParameter("memo", memo);
            return await _baseClient.Execute<CoinExNewWithdrawal>(_baseClient.GetUri("withdrawals"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPaginated<CoinExWithdrawal>>> GetWithdrawHistoryAsync(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)));
            return await _baseClient.Execute<CoinExPaginated<CoinExWithdrawal>>(_baseClient.GetUri("withdrawal-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            return await _baseClient.Execute(_baseClient.GetUri($"withdrawals/{withdrawalId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }*/
        #endregion

        #region Transfer
        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExTransferResult>> TransferToMainAccountAsync(string asset, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.Execute<CoinExTransferResult>(_baseClient.GetUri("transfer-out", 2), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CoinExPaginated<CoinExTransfer>>> GetTransferToMainAccountHistoryAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<CoinExPaginated<CoinExTransfer>>(_baseClient.GetUri("transfer-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTransferToMainAccountAsync(string applyId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("applyId", applyId);
            return await _baseClient.Execute(_baseClient.GetUri("cancel/transfer-out", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }*/
        #endregion

        #region Positions

        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExPosition>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<CoinExPosition>(_baseClient.GetUri("position"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinExPosition>>> GetPositionsAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<CoinExPosition>>(_baseClient.GetUri("positions"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("status", enabled.ToString());
            return await _baseClient.Execute(_baseClient.GetUri("position/margin/auto-deposit-status"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("margin", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.Execute(_baseClient.GetUri("position/margin/deposit-margin"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }*/

        #endregion

        #region Funding fees

        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExPaginatedSlider<CoinExFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<CoinExPaginatedSlider<CoinExFundingItem>>(_baseClient.GetUri("funding-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }*/

        #endregion

        #region Open order value
        /// <inheritdoc />
        /*public async Task<WebCallResult<CoinExOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<CoinExOrderValuation>(_baseClient.GetUri("openOrderStatistics"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }*/

        #endregion

        #region Websocket token

        /*internal async Task<WebCallResult<CoinExToken>> GetWebsocketToken(bool authenticated, CancellationToken ct = default)
        {
            return await _baseClient.Execute<CoinExToken>(_baseClient.GetUri(authenticated ? "bullet-private" : "bullet-public"), method: HttpMethod.Post, ct, signed: authenticated).ConfigureAwait(false);
        }*/

        #endregion
    }
}
