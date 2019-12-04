namespace WebAPITest
{
    using Microsoft.OData.Core;
    using Microsoft.OData.Core.UriParser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Routing;
    using System.Web.OData.Extensions;
    using System.Web.OData.Routing;
    public class Helper
    {
        public static TKey GetKeyFromUri<TKey>(HttpRequestMessage request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            var urlHelper = request.GetUrlHelper() ?? new UrlHelper(request);

            //Get service root from route name
            string serviceRoot = urlHelper.CreateODataLink(
                request.ODataProperties().RouteName,
                request.ODataProperties().PathHandler, new List<ODataPathSegment>());

            //Get path 
            var odataPath = request.ODataProperties().PathHandler.Parse(
                request.ODataProperties().Model,
                serviceRoot, uri.LocalPath);

            //Get Key segment
            var keySegment = odataPath.Segments.OfType<KeyValuePathSegment>().FirstOrDefault();
            if (keySegment == null)
            {
                throw new InvalidOperationException("The reference link does not contain a key.");
            }

            //Retrieve the value of key
            var value = ODataUriUtils.ConvertFromUriLiteral(keySegment.Value, ODataVersion.V4);

            return (TKey)value;
        }

    }
}
