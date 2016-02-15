using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Infrastructure
{
    public class AcceptHeaderControllerSelector : IHttpControllerSelector
    {
        public const string AllowedAcceptHeaderMediaType = "application/vnd.anapioficeandfire+json";
        public const string AllowedAcceptHeaderMediaTypeParamter = "version";

        private const string ControllerKey = "controller";

        private readonly HttpConfiguration _configuration;

        private readonly Lazy<IDictionary<string, HttpControllerDescriptor>> _controllers;
        private readonly ICollection<string> _duplicates;

        private IDictionary<string, int> _defaultControllerVersions = new Dictionary<string, int>();

        public AcceptHeaderControllerSelector(HttpConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
            _duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _controllers = new Lazy<IDictionary<string, HttpControllerDescriptor>>(InitialiseControllerDictionary);
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string controllerName = GetRouteVariable<string>(routeData, ControllerKey);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var version = GetVersionFromMediaType(request, controllerName);
            if (!version.HasValue)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Find a matching controller.
            string key = string.Format(CultureInfo.InvariantCulture, "v{0}.{1}", version, controllerName);

            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            if (_duplicates.Contains(key))
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Multiple controllers were found that match this request."));
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }

        private IDictionary<string, HttpControllerDescriptor> InitialiseControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            // Create a lookup table where key is "namespace.controller". The value of "namespace" is the last
            // segment of the full namespace. For example:
            // MyApplication.Controllers.V1.ProductsController => "V1.Products"
            IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type t in controllerTypes)
            {
                var segments = t.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
                var namespaceName = segments[segments.Length - 1];

                //Calculate the default version for each controller, this will be the highest available version
                int defaultVersionForController = CalculateDefaultVersion(controllerName.ToLower(), namespaceName);
                _defaultControllerVersions[controllerName.ToLower()] = defaultVersionForController;

                var key = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

                // Check for duplicate keys.
                if (dictionary.Keys.Contains(key))
                {
                    _duplicates.Add(key);
                }
                else
                {
                    dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);
                }
            }

            // Remove any duplicates from the dictionary, because these create ambiguous matches. 
            // For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
            foreach (string s in _duplicates)
            {
                dictionary.Remove(s);
            }

            return dictionary;
        }

        private int CalculateDefaultVersion(string controllerName, string namespaceName)
        {
            int defaultVersion = -1;
            var namespaceAsInt = int.Parse(namespaceName.Remove(0, 1));

            if (_defaultControllerVersions.TryGetValue(controllerName, out defaultVersion))
            {
                return namespaceAsInt > defaultVersion ? namespaceAsInt : defaultVersion;
            }

            return namespaceAsInt;
        }

        // Get a value from the route data, if present.
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return default(T);
        }

        private int? GetVersionFromMediaType(HttpRequestMessage request, string controllerName)
        {
            var acceptHeader = request.Headers.Accept;

            foreach (var mime in acceptHeader.OrderByDescending(x => x.Quality))
            {
                if (string.Equals(mime.MediaType, AllowedAcceptHeaderMediaType, StringComparison.InvariantCultureIgnoreCase))
                {
                    var version = mime.Parameters.FirstOrDefault(x => x.Name.Equals(AllowedAcceptHeaderMediaTypeParamter, StringComparison.InvariantCultureIgnoreCase));

                    if (version == null)
                    {
                        return GetDefaultControllerVersion(controllerName);
                    }

                    int parsedVersion = -1;
                    if (int.TryParse(version.Value, out parsedVersion))
                    {
                        return parsedVersion;
                    }

                    return null;
                }
            }

            return GetDefaultControllerVersion(controllerName);
        }

        private int? GetDefaultControllerVersion(string controllerName)
        {
            int defaultVersion = -1;

            if (_defaultControllerVersions.TryGetValue(controllerName, out defaultVersion))
            {
                return defaultVersion;
            }

            return null;
        }
    }
}