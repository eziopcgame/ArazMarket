namespace ArazMarket;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // این متد وقتی صفحه ساخته و نمایش داده می‌شود اجرا می‌شود
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
                // ۱. اضافه کردن پل C# برای ذخیره فایل‌های JSON و PDF
                platformWebView.AddJavascriptInterface(new ArazMarket.Platforms.Android.CSharpBridge(), "CSharpBridge");

                // ۲. تنظیم DownloadListener برای فایل‌های معمولی
                platformWebView.SetDownloadListener(new ArazMarket.Platforms.Android.MyDownloadListener());

                // ۳. تنظیم WebChromeClient برای دسترسی به دوربین (QR Scanner)
                // نکته: این مورد قبلاً در MauiProgram.cs تنظیم شده بود، ولی برای اطمینان اینجا هم ست می‌کنیم
                platformWebView.SetWebChromeClient(new ArazMarket.Platforms.Android.MyWebChromeClient(AppWebView.Handler));
            }
        }
        catch (Exception ex)
        {
            // در صورت بروز خطا (مثلاً در محیط‌های غیراندرویدی)
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
