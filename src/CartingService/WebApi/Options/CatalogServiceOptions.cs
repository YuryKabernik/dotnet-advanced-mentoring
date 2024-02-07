namespace CartingService.WebApi.Options
{
    public class CatalogServiceOptions
    {
        /// <summary>
        /// Section key name in the app settings file.
        /// </summary>
        public static readonly string Key = "CatalogService";

        /// <summary>
        /// Domain address of the service.
        /// </summary>
        public required string BaseAddress { get; set; }
        
        /// <summary>
        /// Url path to the service.
        /// </summary>
        public required string Path { get; set; }
    }
}
