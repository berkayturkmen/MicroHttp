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

        public async Task GetAsync(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            await SendAsync<object>(new HttpRequestMessage(HttpMethod.Get, url), clientName, headers, cancellationToken);
        }

        public async Task<T> PostAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendAsync<T>(RequestCreator(url, HttpMethod.Post, data), clientName, headers, cancellationToken);
        }

        public async Task PostAsync(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            await SendAsync<object>(RequestCreator(url, HttpMethod.Post, data), clientName, headers, cancellationToken);
        }

        public async Task<T> PutAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendAsync<T>(RequestCreator(url, HttpMethod.Put, data), clientName, headers, cancellationToken);
        }

        public async Task PutAsync(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            await SendAsync<object>(RequestCreator(url, HttpMethod.Put, data), clientName, headers, cancellationToken);
        }

        public async Task<T> PatchAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendAsync<T>(RequestCreator(url, HttpMethod.Patch, data), clientName, headers, cancellationToken);
        }

        public async Task PatchAsync(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            await SendAsync<object>(RequestCreator(url, HttpMethod.Patch, data), clientName, headers, cancellationToken);
        }

        public async Task<T> DeleteAsync<T>(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            return await SendAsync<T>(new HttpRequestMessage(HttpMethod.Delete, url), clientName, headers, cancellationToken);
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

        private HttpRequestMessage RequestCreator(string url, HttpMethod method, object data)
        {
            using var request = new HttpRequestMessage(method, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            return request;
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
