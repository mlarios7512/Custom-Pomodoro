using CustomPomodoro.Models.UserSettings.Abstract;
using CustomPomodoro.Models.UserSettings.Concrete;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
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
            builder.AddAudio();
            //        builder.Services
            //.AddBlazorise(options =>
            //{
            //    options.Immediate = true;
            //})
            //.AddBootstrapProviders();
            builder.Services.AddSingleton<IMasterUserSettings,MasterUserSettings>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
