using EarthboundBattle.Services.Implementations;
using EarthboundBattle.Services.Interfaces;
using EarthboundBattle.ViewModels;
using EarthboundBattle.Views;
using Microsoft.Extensions.Logging;

using IGameLogger = EarthboundBattle.Core.Interfaces.ILogger;

namespace EarthboundBattle;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // --- REGISTER DEPENDENCIES ---
        // 1. Core Services (Using our alias IGameLogger)
        builder.Services.AddSingleton<IGameLogger, ConsoleLogger>();
        builder.Services.AddTransient<IBattleService, BattleService>();

        // 2. ViewModels
        builder.Services.AddTransient<BattleViewModel>();

        // 3. Views (Pages)
        builder.Services.AddTransient<BattlePage>();

        return builder.Build();
    }
}