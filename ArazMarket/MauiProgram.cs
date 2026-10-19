public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        
        // اضافه کردن WebChromeClient سفارشی برای همه WebViewها
#if ANDROID
        Microsoft.Maui.Handlers.WebViewHandler.Mapper.ModifyMapping(
            nameof(Android.Webkit.WebView.WebChromeClient),
            (handler, view, args) => handler.PlatformView.SetWebChromeClient(new ArazMarket.Platforms.Android.MyWebChromeClient(handler)));
#endif
        return builder.Build();
    }
}
