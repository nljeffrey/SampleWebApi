using System.Globalization;

namespace Store.Webservice.Application.Resources
{
    /// <summary>
    /// Collection of methods to manage the resource files of the application.
    /// </summary>
    public static class ResourceUtils
    {
        private static readonly CultureInfo DefaultCultureInfo = new ("nl-NL");

        /// <summary>
        /// Gets the specified key from the resources file.
        /// </summary>
        /// <param name="key">Requested resource key.</param>
        /// <returns>The key's value.</returns>
        public static string Get(string key)
        {
            return GenericMessage.ResourceManager.GetString(key, DefaultCultureInfo);
        }
    }
}