using KIBERmobile.Views.StartGroup;

namespace KIBERmobile;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(new StartPage()));
    }
}