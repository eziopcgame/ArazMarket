namespace ArazMarket;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

#if ANDROID
        try
        {
            var platformWebView = AppWebView.Handler?.PlatformView as Android.Webkit.WebView;
            
            if (platformWebView != null)
            {
                // ۱. پل C# برای ذخیره فایل
                platformWebView.AddJavascriptInterface(new ArazMarket.Platforms.Android.CSharpBridge(), "CSharpBridge");

                // ۲. DownloadListener
                platformWebView.SetDownloadListener(new ArazMarket.Platforms.Android.MyDownloadListener());

                // ۳. WebChromeClient برای دوربین (با تبدیل نوع صحیح)
                var webViewHandler = AppWebView.Handler as Microsoft.Maui.Handlers.IWebViewHandler;
                if (webViewHandler != null)
                {
                    platformWebView.SetWebChromeClient(new ArazMarket.Platforms.Android.MyWebChromeClient(webViewHandler));
                }
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
