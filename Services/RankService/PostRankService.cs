using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;
namespace UserRegClient.Services
{
    public partial class PostRankService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public PostRankService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("addRank");
            this.navigationManager = navigationManager;
        }

        partial void OnPostRankList(HttpRequestMessage request);
        partial void OnPostRankListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<SpecialistRank>> PostRank(string rank, int id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"addRank");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add($"Rank", $"{rank}");
            queryString.Add($"Id", $"{id}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            OnPostRankList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnPostRankListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<SpecialistRank>>();
        }
    }
}