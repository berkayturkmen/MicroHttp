namespace MicroHttp.Interfaces
{
    public interface IMicroHttp
    {
        Task<T> GetAsync<T>(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task GetAsync(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<T> PostAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task PostAsync(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<T> PutAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task PutAsync(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<T> PatchAsync<T>(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task PatchAsync(string url, object data, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync<T>(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task DeleteAsync(string url, string clientName = null, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
    }
}
