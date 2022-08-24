namespace NHBC_Backend.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NHBC_Backend.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;
    using NHBC_Backend.Requests;

    public class RestaurantService : IRestaurantService
    {
        private Container _container;

        public RestaurantService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<Restaurants> AddAsync(CreateRestaurantRequest restaurantRequest)
        {
            Restaurants restaurant = new Restaurants(restaurantRequest);

            try
            {
                var response = await _container.CreateItemAsync<Restaurants>(restaurant, new PartitionKey(restaurant.Id));
                return response;
            }
            catch (CosmosException e)
            {
                throw e;
            }
        }
        public async Task DeleteAsync(string id)
        {
            try
            {
                var response = await _container.DeleteItemAsync<Restaurants>(id, new PartitionKey(id));
            }
            catch (CosmosException e)
            {
                throw e;
            }
        }
        public async Task<Restaurants> GetAsync(string id)
        {
            try 
            { 

                var response = await _container.ReadItemAsync<Restaurants>(id, new PartitionKey(id));
                return response.Resource;
            
            }
            catch (CosmosException e) //For handling item not found and other exceptions
            {
                throw e;
            }

        }

        public async Task<IEnumerable<Restaurants>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Restaurants>(new QueryDefinition(queryString));
            var results = new List<Restaurants>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id, Restaurants restaurants)
        {
            await _container.UpsertItemAsync(restaurants, new PartitionKey(id));
        }
    }
}
