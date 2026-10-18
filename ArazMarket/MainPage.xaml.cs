namespace ArazMarket;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (AppWebView.CanGoBack)
            AppWebView.GoBack();
        else
            await AppWebView.EvaluateJavaScriptAsync("if (typeof closeModal === 'function') closeModal();");
    }
}
