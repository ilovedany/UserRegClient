using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Web;
using UserRegClient.Models;
namespace UserRegClient.Services
{
    public partial class XamlUserService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public XamlUserService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("getXml");
            this.navigationManager = navigationManager;
        }

        partial void OnGetXamlUserList(HttpRequestMessage request);
        partial void OnGetXamlUserListResponse(HttpResponseMessage response);

        public async Task<string> GetXml(int id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"getXml");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("Id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetXamlUserList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetXamlUserListResponse(response);

            return await response.Content.ReadAsStringAsync();
        }
    }
}