using MicroHttp.Interfaces;
using Newtonsoft.Json;

namespace MicroHttp
{
    public class MicroHttp : IMicroHttp
    {
        private readonly IHttpClientFactory _clientFactory;

        public MicroHttp(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAsync<T>(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, url), clientName, headers, cancellationToken);
        }

        public async Task<T> PostAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            return await SendAsync<T>(request, clientName, headers, cancellationToken);
        }

        public async Task<T> PutAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            return await SendAsync<T>(request, clientName, headers, cancellationToken);
        }

        public async Task<T> PatchAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            return await SendAsync<T>(request, clientName, headers, cancellationToken);
        }

        public async Task DeleteAsync(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            await SendAsync<object>(new HttpRequestMessage(HttpMethod.Delete, url), clientName, headers, cancellationToken);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage request, string clientName, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            HttpResponseMessage? response = null;
            try
            {
                AddHeaders(request, headers);
                var client = _clientFactory.CreateClient(clientName);
                response = await client.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                response?.Dispose();
            }
        }
        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
