using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using UserRegClient.Models;
namespace UserRegClient.Services
{
    public partial class GetRankService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public GetRankService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("getRank");
            this.navigationManager = navigationManager;
        }

        partial void OnGetRankList(HttpRequestMessage request);
        partial void OnGetRankListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<SpecialistRank>> GetWorkshopList()
        {
            var uri = new Uri(httpClient.BaseAddress, $"getRank");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRankList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetRankListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<SpecialistRank>>();
        }
    }
}