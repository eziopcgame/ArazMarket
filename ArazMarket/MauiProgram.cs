using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace ArazMarket;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>(); // حالا App رو می‌شناسه

        // این بخش برای WebChromeClient در سطح کل برنامه (اختیاری ولی مفید)
#if ANDROID
        Microsoft.Maui.Handlers.WebViewHandler.Mapper.ModifyMapping(
            nameof(Android.Webkit.WebView.WebChromeClient),
            (handler, view, args) => handler.PlatformView.SetWebChromeClient(new ArazMarket.Platforms.Android.MyWebChromeClient(handler)));
#endif

        return builder.Build();
    }
}
