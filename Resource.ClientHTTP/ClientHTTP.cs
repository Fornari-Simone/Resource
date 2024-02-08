using Microsoft.AspNetCore.Mvc;
using Resource.ClientHTTP.Abstraction;
using System.Net.Http.Json;

namespace Resource.ClientHTTP
{
    public class ClientHTTP : IClientHTTP
    {
        private readonly HttpClient _httpClient;
        public ClientHTTP(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult?> ModifyOwn(int ID, int delta, CancellationToken cancellation = default)
        {
            var response = await _httpClient.PatchAsync($"/Resource/ModifyOwn", JsonContent.Create(new { ID, delta }), cancellation);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IActionResult>(cancellationToken: cancellation);
        }
    }
}
