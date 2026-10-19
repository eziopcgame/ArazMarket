using System.Diagnostics.CodeAnalysis;

namespace ArazMarket;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // 🟢 ویژگی‌ها را از روی کلاس برداشتیم و روی این متد گذاشتیم
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ArazMarket.Platforms.Android.CSharpBridge))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ArazMarket.Platforms.Android.MyWebChromeClient))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ArazMarket.Platforms.Android.MyDownloadListener))]
    protected override void OnAppearing()
    {
        base.OnAppearing();

#if ANDROID
        try
        {
            // گرفتن WebView نیتیو اندروید از کنترل MAUI
            var platformWebView = AppWebView.Handler?.PlatformView as Android.Webkit.WebView;
            
            if (platformWebView != null)
            {
                // ۱. اضافه کردن پل C# برای ذخیره فایل‌ها (JSON, Receipt)
                platformWebView.AddJavascriptInterface(new ArazMarket.Platforms.Android.CSharpBridge(), "CSharpBridge");

                // ۲. تنظیم DownloadListener برای فایل‌های معمولی
                platformWebView.SetDownloadListener(new ArazMarket.Platforms.Android.MyDownloadListener());

                // ۳. تنظیم WebChromeClient برای دسترسی به دوربین (QR Scanner)
                var webViewHandler = AppWebView.Handler as Microsoft.Maui.Handlers.IWebViewHandler;
                if (webViewHandler != null)
                {
                    platformWebView.SetWebChromeClient(new ArazMarket.Platforms.Android.MyWebChromeClient(webViewHandler));
                }

                // ۴. اجازه دادن به WebView برای اجرای جاوااسکریپت و دسترسی به فایل‌های محلی
                platformWebView.Settings.JavaScriptEnabled = true;
                platformWebView.Settings.DomStorageEnabled = true;
                platformWebView.Settings.AllowFileAccess = true;
                platformWebView.Settings.AllowContentAccess = true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error setting up WebView: {ex.Message}");
        }
#endif
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (AppWebView.CanGoBack)
            AppWebView.GoBack();
        else
            await AppWebView.EvaluateJavaScriptAsync("if (typeof closeModal === 'function') closeModal();");
    }
}
