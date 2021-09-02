namespace Store.Webservice.Domain.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets product ma,e.
        /// </summary>
        public string ProductName { get; set; }
    }
}