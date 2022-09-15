using WebUI.Models;
using WebUI.Services.Interfaces;

namespace WebUI.Services
{
    public class NorthwindService : INorthwindService
    {
        private readonly HttpClient _httpClient;

        public NorthwindService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Supplier>> GetAllSupplierAsync()
        {
            var suppliers = await _httpClient.GetFromJsonAsync<List<Supplier>>("Suppliers");
            return suppliers;
        }
    }
}
