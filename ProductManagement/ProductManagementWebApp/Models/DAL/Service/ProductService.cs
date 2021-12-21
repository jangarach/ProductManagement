using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ProductManagementWebApp.Models.DAL.Service
{
    public interface IProductService
    {
        List<ProductViewModel> GetProducts();
        ProductViewModel GetProduct(Guid id);
        ProductViewModel UpdateProduct(ProductViewModel product);
        ProductViewModel AddProduct(ProductViewModel product);
        void DeleteProduct(Guid id);
    }
    public class ProductService : IProductService
    {   
        private static string baseUrl = "https://localhost:44317/api/";

        public ProductViewModel GetProduct(Guid id)
        {
            ProductViewModel product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                //HTTP GET
                var responseTask = client.GetAsync(string.Format("product/{0}", id));
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    product = JsonConvert.DeserializeObject<ProductViewModel>(readTask.Result);
                }
            }
            return product;
        }

        public List<ProductViewModel> GetProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                //HTTP GET
                var responseTask = client.GetAsync("product");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(readTask.Result);
                }
            }


            return products;
        }
        public ProductViewModel AddProduct(ProductViewModel product)
        {
            ProductViewModel updProduct = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var productJson = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(productJson);
                var byteProduct = new ByteArrayContent(buffer);
                byteProduct.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = client.PostAsync("product", byteProduct).Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    updProduct = JsonConvert.DeserializeObject<ProductViewModel>(readTask.Result);
                }
            }
            return updProduct;
        }

        public ProductViewModel UpdateProduct(ProductViewModel product)
        {
            ProductViewModel updProduct = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var productJson = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(productJson);
                var byteProduct = new ByteArrayContent(buffer);
                byteProduct.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = client.PutAsync(String.Format("product/{0}", product.Id), byteProduct).Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    updProduct = JsonConvert.DeserializeObject<ProductViewModel>(readTask.Result);
                }
            }
            return updProduct;
        }

        private static Task<string> GetStringAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync(url);
            }
        }

        public void DeleteProduct(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.DeleteAsync(String.Format("product/{0}", id)).Result;
                if (result.IsSuccessStatusCode)
                {
                    return;
                }
            }
        }
    }
}