using Newtonsoft.Json;
using Shop.Services;

namespace Shop.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("img")]
        public string Image { get; set; }

        [JsonProperty("currency")]
        public Currencies Currency { get; set; }
    }
}
