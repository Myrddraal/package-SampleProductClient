using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace SampleProductClient
{
    public class ProductClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly Uri _apiUri;

        public ProductClient(Uri apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<List<string>> GetAllProducts()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(_apiUri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<string>>();
        }

        public async Task<string> GetProduct(Guid id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(new Uri(_apiUri, id.ToString()));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<string>();
        }

        public async Task<string> AddProduct(string product)
        {
            HttpResponseMessage response = await HttpClient.PostAsync(_apiUri, new ObjectContent<string>(product, new JsonMediaTypeFormatter()));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<string>();
        }

        public async Task DeleteProduct(Guid id)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(new Uri(_apiUri, id.ToString()));

            response.EnsureSuccessStatusCode();
        }
    }
}
