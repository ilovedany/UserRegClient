using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace UserRegClient.Services
{
    public partial class DeleteUserService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public DeleteUserService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("deleteUser");
            this.navigationManager = navigationManager;
        }

        partial void OnDeleteUserList(HttpRequestMessage request);
        partial void OnDeleteUserListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<User>> DeleteUserList(string id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"deleteUser");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("Id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUserList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnDeleteUserListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        }
    }
}