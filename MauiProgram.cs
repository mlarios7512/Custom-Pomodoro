using Microsoft.Extensions.Logging;
//using Blazorise;
//using Blazorise.Bootstrap;

namespace CustomPomodoro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
    //        builder.Services
    //.AddBlazorise(options =>
    //{
    //    options.Immediate = true;
    //})
    //.AddBootstrapProviders();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
