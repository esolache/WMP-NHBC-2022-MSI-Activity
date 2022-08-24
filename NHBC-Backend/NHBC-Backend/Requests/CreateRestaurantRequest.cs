using Newtonsoft.Json;

namespace NHBC_Backend.Requests
{
    public class CreateRestaurantRequest
    {

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

        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }
    }
}
