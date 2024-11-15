namespace KIBERmobile.ViewModels;

internal class MenuVM : BaseViewModel
{
    public MenuVM()
    {
        PageTitle = "Меню";

        OpenPrivatePolicyCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.PrivacyPolicy));

        OpenClubTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.ClubTerms));

        OpenFeedTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.FeedTerms));

        OpenShopTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.ShopTerms));
    }

    public Command OpenPrivatePolicyCommand { get; }
    public Command OpenClubTermsCommand { get; }
    public Command OpenFeedTermsCommand { get; }
    public Command OpenShopTermsCommand { get; }

    public async Task LogOut()
    {
        AuthorizationController.Clear();
        ProfileController.Clear();
        ShopController.Clear();
        FeedController.Clear();
        LogsController.Clear();
        Preferences.Clear();
        //await Shell.Current.GoToAsync($"//LaunchTabBar/LaunchTab/{nameof(LaunchScreen)}/{nameof(StartPage)}");
    }
}