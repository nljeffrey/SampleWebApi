using System.Reflection;

namespace Store.Webservice.WebService
{
    /// <summary>
    /// Service layer constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Project name constant.
        /// </summary>
        public static readonly string ProjectName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        /// <summary>
        /// Application settings related constants.
        /// </summary>
        public static class AppSettings
        {
            /// <summary>
            /// Gets or sets the database connection string.
            /// </summary>
            public static readonly string StoreDatabase = "StoreDatabase";

            /// <summary>
            /// Gets or sets the Swagger URL.
            /// </summary>
            public static readonly string SwaggerFilePath = "SwaggerFilePath";
        }

        /// <summary>
        /// CORS settings
        /// </summary>
        public static class CorsSettings
        {
            /// <summary>
            /// Gets allowed URLs to call API (CORS).
            /// </summary>
            public static string CorsOriginAllowed = "localhost";
        }
    }
}