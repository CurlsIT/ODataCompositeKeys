using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using ODataCompositeKeys.Models;

namespace ODataCompositeKeys
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<BookAuthor>("BookAuthors");
            builder.EntitySet<Author>("Authors");
            builder.EntitySet<Book>("Books");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
