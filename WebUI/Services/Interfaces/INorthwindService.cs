using WebUI.Models;

namespace WebUI.Services.Interfaces
{
    public interface INorthwindService
    {
        Task<List<Supplier>> GetAllSupplierAsync();
    }
}
