using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Interfaces;
using CoinEx.Net.Objects.Internal;
using Newtonsoft.Json.Linq;
using System.Text;
using CryptoExchange.Net.Logging;
using System.Security.Cryptography;
using System.Net;
using CoinEx.Net.Clients.FuturesApi;

namespace CoinEx.Net
{
    internal class CoinExAuthenticationProvider : AuthenticationProvider
    {
        private readonly INonceProvider _nonceProvider;

        public long GetNonce() => _nonceProvider.GetNonce();

        public CoinExAuthenticationProvider(ApiCredentials credentials, INonceProvider? nonceProvider): base(credentials)
        {
            _nonceProvider = nonceProvider ?? new CoinExNonceProvider();
        }



        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            bool futures = true;
            if (futures)
            {
                uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
                bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
                headers = new Dictionary<string, string>();

                if (!auth)
                    return;

                var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;


                DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
                Console.WriteLine("now: " + now.ToString());
                parameters.Add("timestamp", GetMillisecondTimestamp(apiClient).ToString());
                string parameters_str = ToPrettyString(parameters) + "&secret_key=" + Credentials.Secret!.GetString();


                //Console.WriteLine("parameters_str: " + parameters_str);
                string authorization = ComputeSha256Hash(parameters_str);

                headers.Add("Authorization", authorization);
                headers.Add("AccessId", Credentials.Key!.GetString());

                //Console.WriteLine("SignHMACSHA256 " + authorization);
            }
            else
            {
                uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
                bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
                headers = new Dictionary<string, string>();

                if (!auth)
                    return;

                var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;
                parameters.Add("access_id", Credentials.Key!.GetString());
                parameters.Add("tonce", _nonceProvider.GetNonce());
                headers.Add("Authorization", SignMD5(uri.SetParameters(parameters, arraySerialization).Query.Replace("?", "") + "&secret_key=" + Credentials.Secret!.GetString()));
            }
        }


        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string ToPrettyString<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            var str = new StringBuilder();
            
            foreach (var pair in dict)
            {
                str.Append(String.Format("{0}={1}&", pair.Key, pair.Value));
            }
            str.Remove(str.Length - 1, 1);
            return str.ToString();
        }


        public override string Sign(string toSign)
        {
            return SignMD5(toSign).ToUpper();
        }
    }
}
