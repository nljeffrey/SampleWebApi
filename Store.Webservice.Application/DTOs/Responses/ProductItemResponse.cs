namespace Store.Webservice.Application.DTOs.Responses
{
    /// <summary>
    /// A product DTO response
    /// </summary>
    public class ProductItemResponse
    {
        /// <summary>
        /// Gets or sets the id of a product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of a product.
        /// </summary>
        public string Name { get; set; }
    }
}