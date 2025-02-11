using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;
namespace UserRegClient.Services
{
    public partial class PostUserService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public PostUserService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("addUser");
            this.navigationManager = navigationManager;
        }

        partial void OnPostUserList(HttpRequestMessage request);
        partial void OnPostUserListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<User>> PostUserList(string name, string surname,int age,string email)
        {
            var uri = new Uri(httpClient.BaseAddress, $"addUser");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add($"Name", $"{name}");
            queryString.Add($"Surname", $"{surname}");
            queryString.Add($"Age", $"{age}");
            queryString.Add($"Email", $"{email}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            OnPostUserList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnPostUserListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        }
    }
}