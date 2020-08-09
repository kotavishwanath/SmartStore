using Newtonsoft.Json;

namespace smartStoreApi.Models.Response
{
    public class SuggestedProductResponse
    {

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productPrice")]
        public double ProductPrice { get; set; }

        [JsonProperty("productImagePath")]
        public string ProductImagePath { get; set; }

        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }
    }
}