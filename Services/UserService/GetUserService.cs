using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using UserRegClient.Models;
namespace UserRegClient.Services
{
    public partial class GetUserService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public GetUserService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("getUsers");
            this.navigationManager = navigationManager;
        }

        partial void OnGetUserList(HttpRequestMessage request);
        partial void OnGetUserListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<User>> GetUserList()
        {
            var uri = new Uri(httpClient.BaseAddress, $"getUsers");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetUserListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        }
    }
}