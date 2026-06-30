using FloorMAUI.Views;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FloorMAUI.Services
{
    public class DbService
    { 
        HttpClient _client = new HttpClient();
        string url = "http://localhost:5211/api/db";
        public async Task<ObservableCollection<Partner>> GetPartnersAsync()
        {
            var response = await _client.GetAsync($"{url}/partners");
            return await response.Content.ReadFromJsonAsync<ObservableCollection<Partner>>() ?? new();
        }

        public async Task<ObservableCollection<PartnerProduct>> GetPartnerProductsAsync()
        {
            var response = await _client.GetAsync($"{url}/partnerproducts");
            return await response.Content.ReadFromJsonAsync<ObservableCollection<PartnerProduct>>() ?? new();
        }

        public async Task<Partner> CreatePartnerAsync(Partner partner)
        {
            var response = await _client.PostAsJsonAsync(url, partner);
            return await response.Content.ReadFromJsonAsync<Partner>() ?? new();
        }

        public async Task<ObservableCollection<PartnerProduct>> GetHistoryAsync(int id)
        {
            var response = await _client.GetAsync($"{url}/history?id={id}");
            return await response.Content.ReadFromJsonAsync<ObservableCollection<PartnerProduct>>() ?? new();
        }

        public async Task<bool> UpdatePartnerAsync(int id, Partner partner)
        {
            var response = await _client.PutAsJsonAsync(url + $"/{id}", partner);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            var response = await _client.GetAsync($"{url}/products");
            return await response.Content.ReadFromJsonAsync<ObservableCollection<Product>>() ?? new();
        }


        public async Task<int> GetMaterialAmount(int productId, double height, double width)
        {
            if (height > 0 && width > 0)
            {
                try
                {
                    string defectcontent = await _client.GetStringAsync($"{url}/defectrate?id={productId}");
                    double defectrate = double.Parse(defectcontent, System.Globalization.CultureInfo.InvariantCulture);

                    string coefficientcontent = await _client.GetStringAsync($"{url}/productcoefficient?id={productId}");
                    double productCoefficient = double.Parse(coefficientcontent, System.Globalization.CultureInfo.InvariantCulture);

                    double productamount = height * width;

                    productamount *= productCoefficient;
                    productamount /= (1 - defectrate / 100);
                    int response = Convert.ToInt32(Math.Ceiling(productamount));
                    return response;
                }
                catch
                {
                    return -1;
                }
            }
            else return -3;
        }
        //public int Id { get; set; }
        //public string? Type { get; set; }
        //public string? Name { get; set; }
        //public string? Article { get; set; }
        //public decimal? MinPrice { get; set; }
        //public decimal? Coefficient { get; set; }
        //public decimal? DefectPercentage { get; set; }
    }
}
    