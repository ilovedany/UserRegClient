using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace UserRegClient.Services
{
    public partial class DeleteRankService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public DeleteRankService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("deleteRank");
            this.navigationManager = navigationManager;
        }

        partial void OnDeleteRankList(HttpRequestMessage request);
        partial void OnDeleteRankListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<SpecialistRank>> DeleteRankList(string id_rank)
        {
            var uri = new Uri(httpClient.BaseAddress, $"deleteRank");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("Id_rank", $"{id_rank}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRankList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnDeleteRankListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<SpecialistRank>>();
        }
    }
}