using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using NHBC_Backend.Models;
using NHBC_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Cosmos;
using NHBC_Backend.Requests;

namespace NHBC_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    { 

        private readonly IRestaurantService _cosmosDbService;
        public RestaurantsController(IRestaurantService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));

        }

        // GET All restauarants
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _cosmosDbService.GetMultipleAsync("SELECT * FROM c");
                return Ok(result);
            }
            catch (CosmosException e)
            {
                return NotFound(e.ResponseBody);
            }


        }
        // GET a restaurant
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {   
            try
            {
                var result = await _cosmosDbService.GetAsync(id);
                return Ok(result);
            }
            catch (CosmosException e)
            {
                return NotFound(e.ResponseBody);
            }

        }
        // POST 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantRequest restaurants)
        {
            try
            {   

                var returnRes = await _cosmosDbService.AddAsync(restaurants);
                return CreatedAtAction(nameof(Get), new { id = returnRes.Id }, returnRes);
            }
            catch(CosmosException e)
            {
                return Conflict(e.ResponseBody);
            }
        }
        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Restaurants restaurants)
        {
                await _cosmosDbService.UpdateAsync("{id}", restaurants);
                return Ok();
           
        }
        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try {
                await _cosmosDbService.DeleteAsync(id);
                return Ok();
            }
            catch (CosmosException e)
            {
                return NotFound(e.ResponseBody);
            }
        }
    }
}
