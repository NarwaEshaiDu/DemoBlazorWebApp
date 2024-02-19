using Blazor2App.Application.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace Blazor2App.Services
{
    public class Blazor2AppApiService
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;

        public Blazor2AppApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        private async Task<HttpClient> GetHttpClient(string url)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri(url);
            client.Timeout = TimeSpan.FromSeconds(60);

            return client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken) where T : class, new()
        {
            using var client = await GetHttpClient(url);
            try
            {
                var httpResponse = await client.GetAsync(url, cancellationToken);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    Log.Logger.Error("Http request (url: {0}) failed with an unsuccessful statuscode (Code: {1}). Response: {2}", url, httpResponse.StatusCode, responseContent);
                    return default!;
                }

                if (string.IsNullOrEmpty(responseContent)) return new T();

                return JsonConvert.DeserializeObject<T>(responseContent)!;
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"An exception ocurred while fetching data in HttpClient: {ex.Message}", ex);
                throw new Exception(JsonConvert.SerializeObject(new { Url = url, Message = "An exception ocurred while fetching data." }), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        protected async Task<TResult> PostAsync<T, TResult>(string url, T body, CancellationToken cancellationToken)
           where T : class, new()
           where TResult : class, new()
        {
            using var client = await GetHttpClient(url);
            try
            {
                var httpResponse = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    Log.Logger.Error("Http POST request (url: {0}) failed with an unsuccessful statuscode (Code: {1}). Response: {2}", url, httpResponse.StatusCode, responseContent);
                    throw new HttpRequestException(string.Format("Http POST request (url: {0}) failed with an unsuccessful statuscode (Code: {1}). Response: {2}", url, httpResponse.StatusCode, responseContent));
                }

                if (string.IsNullOrEmpty(responseContent)) return null!;

                return JsonConvert.DeserializeObject<TResult>(responseContent)!;
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"An exception ocurred while posting data in HttpClient: {ex.Message}", ex);
                throw new Exception(JsonConvert.SerializeObject(new { Url = url, Message = "An exception ocurred while fetching data." }), ex);
            }
        }
    }
}
