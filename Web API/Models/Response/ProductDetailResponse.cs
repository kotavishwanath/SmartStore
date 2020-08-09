using Newtonsoft.Json;
using System.Collections.Generic;

namespace smartStoreApi.Models.Response
{
    public class ProductDetailResponse
    {
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

        [JsonProperty("suggestedProductResponse")]
        public List<SuggestedProductResponse> SuggestedProductResponse { get; set; }
    }
}