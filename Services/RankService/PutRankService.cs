using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace UserRegClient.Services
{
    public partial class PutRankService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public PutRankService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("putRank");
            this.navigationManager = navigationManager;
        }

        partial void OnPutRankList(HttpRequestMessage request);
        partial void OnPutRankListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<SpecialistRank>> PutRankList(string rank,int id,int id_rank)
        {
            var uri = new Uri(httpClient.BaseAddress, $"putRank");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add($"Rank", $"{rank}");
            queryString.Add($"Id", $"{id}");
            queryString.Add($"Id_Rank", $"{id_rank}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Put, uri);

            OnPutRankList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnPutRankListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<SpecialistRank>>();
        }
    }
}