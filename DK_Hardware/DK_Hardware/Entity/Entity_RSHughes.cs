using Newtonsoft.Json;

namespace DK_Hardware.Entity
{
    public class Entity_RSHughes
    {
        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public List<Products> products { get; set; }
    }

    public class Products
    {
        [JsonProperty("itemCode", NullValueHandling = NullValueHandling.Ignore)]
        public string itemCode { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public string manufacturer { get; set; }

        [JsonProperty("mpn", NullValueHandling = NullValueHandling.Ignore)]
        public string mpn { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public double price { get; set; }

        [JsonProperty("brand", NullValueHandling = NullValueHandling.Ignore)]
        public string brand { get; set; }

        [JsonProperty("upc", NullValueHandling = NullValueHandling.Ignore)]
        public double upc { get; set; }
    }


}

