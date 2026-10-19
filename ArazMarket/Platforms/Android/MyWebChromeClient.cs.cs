using Android.Webkit;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;

namespace ArazMarket.Platforms.Android
{
    internal class MyWebChromeClient : MauiWebChromeClient
    {
        public MyWebChromeClient(IWebViewHandler handler) : base(handler) { }

        public override void OnPermissionRequest(PermissionRequest request)
        {
            foreach (var resource in request.GetResources())
            {
                if (resource.Equals(PermissionRequest.ResourceVideoCapture, StringComparison.OrdinalIgnoreCase))
                {
                    // اجازه دادن به WebView برای دسترسی به دوربین
                    request.Grant(request.GetResources());
                    return;
                }
            }
            base.OnPermissionRequest(request);
        }
    }
}