namespace NHBC_Backend.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NHBC_Backend.Models;
    using NHBC_Backend.Requests;

    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurants>> GetMultipleAsync(string query);
        Task<Restaurants> GetAsync(string id);
        Task<Restaurants> AddAsync(CreateRestaurantRequest restaurant);
        Task UpdateAsync(string id, Restaurants restaurants);
        Task DeleteAsync(string id);
    }
}