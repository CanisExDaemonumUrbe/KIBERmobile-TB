using CommunityToolkit.Maui;

namespace KIBERmobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseSharedMauiApp()
            .UseMauiCommunityToolkit();

        return builder.Build();
    }
}