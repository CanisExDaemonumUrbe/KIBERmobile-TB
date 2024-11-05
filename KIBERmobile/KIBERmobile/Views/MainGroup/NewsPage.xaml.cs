using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.MainGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NewsPage : ContentPage
{
    private readonly NewsVM _vm;

    public NewsPage(int newsId)
    {
        BindingContext = _vm = new NewsVM(newsId);
        InitializeComponent();
    }
}