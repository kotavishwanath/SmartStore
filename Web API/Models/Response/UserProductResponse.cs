using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartStoreApi.Models.Response
{
    public class UserProductResponse
    {
        public List<ProductResponse> ProductResponse { get; set; }
        public List<UserViewedProduct> UserViewedProduct { get; set; }
    }
}
