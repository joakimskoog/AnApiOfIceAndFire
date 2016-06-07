using System;
using System.Web.Http.Controllers;

namespace AnApiOfIceAndFire.Infrastructure
{
    public static class HttpControllerDescriptorExtensions
    {
        public static string GetSupportedVersion(this HttpControllerDescriptor controller)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));

            return controller.ControllerType.GetControllerSupportedVersion();
        }

        public static bool HasSupportForVersion(this HttpControllerDescriptor controller, string version)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (string.IsNullOrEmpty(version)) throw new ArgumentNullException(nameof(version));

            return controller.ControllerType.HasControllerSupportForVersion(version);
        }
    }
}