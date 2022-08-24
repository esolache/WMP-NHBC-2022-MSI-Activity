namespace NHBC_Backend.Models
{
    using Newtonsoft.Json;
    using NHBC_Backend.Requests;

    public class Restaurants
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public double Rating { get; set; }

        [JsonProperty(PropertyName = "yelpid")]
        public string YelpID { get; set; }

        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }

     //   [JsonProperty(PropertyName = "price")]
     //   public string Price { get; set; }
        
        public Restaurants()
        {
            Id = Guid.NewGuid().ToString();
            
        }

        public Restaurants(CreateRestaurantRequest restaurantRequest):this()
        {
            Name = restaurantRequest.Name;
            Category = restaurantRequest.Category;
            Rating = restaurantRequest.Rating;
            YelpID = restaurantRequest.YelpID;
            ImageUrl = restaurantRequest.ImageUrl;
           // Price = restaurantRequest.Price;

        }
    
    }


}
