using Android.Webkit;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform; // این خط برای پیدا کردن MauiWebChromeClient ضروری است
using System;

namespace ArazMarket.Platforms.Android
{
    internal class MyWebChromeClient : MauiWebChromeClient
    {
        public MyWebChromeClient(IWebViewHandler handler) : base(handler) { }

        public override void OnPermissionRequest(PermissionRequest? request)
        {
            if (request == null) return;
            
            foreach (var resource in request.GetResources())
            {
                if (resource.Equals(PermissionRequest.ResourceVideoCapture, StringComparison.OrdinalIgnoreCase))
                {
                    request.Grant(request.GetResources());
                    return;
                }
            }
            base.OnPermissionRequest(request);
        }
    }
}
