using System;
using System.Web.Http;

namespace AnApiOfIceAndFire.Infrastructure
{
    public static class TypeExtensions
    {
        public static string GetControllerSupportedVersion(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!IsValidControllerType(type)) throw new ArgumentException("The only supported type is ApiController"); //It does not make sense to check for versioning support on anything else than ApiControllers.

            string[] namespaceSegments = type.Namespace?.Split(Type.Delimiter);
            var innermostNamespace = namespaceSegments?[namespaceSegments.Length - 1]; //The convention is that the innermost namespace is the version

            return innermostNamespace ?? string.Empty;
        }

        public static bool HasControllerSupportForVersion(this Type type, string version)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (version == null) throw new ArgumentNullException(nameof(version));

            var supportedVersion = type.GetControllerSupportedVersion();

            return string.Equals(version, supportedVersion, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsValidControllerType(Type type)
        {
            return typeof(ApiController).IsAssignableFrom(type);
        }
    }
}