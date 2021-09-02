using System.Collections.Generic;

namespace Store.Webservice.Application.DTOs.Responses
{
    /// <summary>
    /// Product response DTO
    /// </summary>
    public class GetAllProductsResponse
    {
        public IEnumerable<ProductItemResponse> Product { get; set; }
    }
}