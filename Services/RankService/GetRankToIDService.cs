using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;
using UserRegClient.Models;
namespace UserRegClient.Services
{
    public partial class GetRankToIDService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public GetRankToIDService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("searchRank");
            this.navigationManager = navigationManager;
        }

        partial void OnGetRankToIDList(HttpRequestMessage request);
        partial void OnGetRankToIDListResponse(HttpResponseMessage response);

        public async Task<List<SpecialistRank>> GetRankToIdList(int id)
        {
           var uri = new Uri(httpClient.BaseAddress, $"searchRank");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("Id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRankToIDList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetRankToIDListResponse(response);


            return await response.Content.ReadFromJsonAsync<List<SpecialistRank>>();
        }
    }
}