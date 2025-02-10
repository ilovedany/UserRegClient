using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace UserRegClient.Services
{
    public partial class PutUserService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public PutUserService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("putUser");
            this.navigationManager = navigationManager;
        }

        partial void OnPutUserList(HttpRequestMessage request);
        partial void OnPutUserListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<User>> PutUserList(string name,string surname,int age,string email,string id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"putUser");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add($"Name", $"{name}");
            queryString.Add($"Surname", $"{surname}");
            queryString.Add($"Age", $"{age}");
            queryString.Add($"Email", $"{email}");
            queryString.Add($"Id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Put, uri);

            OnPutUserList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnPutUserListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        }
    }
}