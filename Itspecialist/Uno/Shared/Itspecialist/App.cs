using Prism.DryIoc;

namespace Itspecialist
{
    public partial class App : PrismApplication
    {
        protected Window? MainWindow { get; private set; }
        protected IHost? Host { get; private set; }

        protected override void ConfigureHost(IHostBuilder builder)
        {
            base.ConfigureHost(builder);
            builder
#if DEBUG
                    // Switch to Development environment when running in DEBUG
                    .UseToolkitNavigation()
                    .UseEnvironment(Environments.Development)
#endif
                    .UseLogging(configure: (context, logBuilder) =>
                    {
                        // Configure log levels for different categories of logging
                        logBuilder
                            .SetMinimumLevel(
                                context.HostingEnvironment.IsDevelopment() ?
                                    LogLevel.Information :
                                    LogLevel.Warning)

                            // Default filters for core Uno Platform namespaces
                            .CoreLogLevel(LogLevel.Warning);

                        // Uno Platform namespace filter groups
                        // Uncomment individual methods to see more detailed logging
                        //// Generic Xaml events
                        //logBuilder.XamlLogLevel(LogLevel.Debug);
                        //// Layout specific messages
                        //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                        //// Storage messages
                        //logBuilder.StorageLogLevel(LogLevel.Debug);
                        //// Binding related messages
                        //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                        //// Binder memory references tracking
                        //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                        //// DevServer and HotReload related
                        //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                        //// Debug JS interop
                        //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);

                    }, enableUnoLogging: true)
                    .UseConfiguration(configure: configBuilder =>
                        configBuilder
                            .EmbeddedSource<App>()
                            .Section<AppConfig>()
                    )
                    // Enable localization (see appsettings.json for supported languages)
                    .UseLocalization()
                    // Register Json serializers (ISerializer and ISerializer)
                    .UseSerialization((context, services) => services
                        .AddContentSerializer(context))
                    .UseHttp((context, services) => services
                        // Register HttpClient
#if DEBUG
                        // DelegatingHandler will be automatically injected into Refit Client
                        .AddTransient<DelegatingHandler, DebugHttpHandler>()
#endif
                        )
                        //.AddSingleton<IWeatherCache, WeatherCache>()
                        //.AddRefitClient<IApiClient>(context))
                    .UseAuthentication(auth =>
        auth.AddCustom(custom =>
                custom
                    .Login((sp, dispatcher, credentials, cancellationToken) =>
                    {
                        // TODO: Write code to process credentials that are passed into the LoginAsync method
                        //if (credentials?.TryGetValue(nameof(LoginViewModel.Username), out var username) ?? false &&
                        //    !username.IsNullOrEmpty())
                        //{
                        //    // Return IDictionary containing any tokens used by service calls or in the app
                        //    credentials ??= new Dictionary<string, string>();
                        //    credentials[TokenCacheExtensions.AccessTokenKey] = "SampleToken";
                        //    credentials[TokenCacheExtensions.RefreshTokenKey] = "RefreshToken";
                        //    credentials["Expiry"] = DateTime.Now.AddMinutes(5).ToString("g");
                        //    return ValueTask.FromResult<IDictionary<string, string>?>(credentials);
                        //}

                        // Return null/default to fail the LoginAsync method
                        return ValueTask.FromResult<IDictionary<string, string>?>(default);
                    })
                    .Refresh((sp, tokenDictionary, cancellationToken) =>
                    {
                        // TODO: Write code to refresh tokens using the currently stored tokens
                        if ((tokenDictionary?.TryGetValue(TokenCacheExtensions.RefreshTokenKey, out var refreshToken) ?? false) &&
                            !refreshToken.IsNullOrEmpty() &&
                            (tokenDictionary?.TryGetValue("Expiry", out var expiry) ?? false) &&
                            DateTime.TryParse(expiry, out var tokenExpiry) &&
                            tokenExpiry > DateTime.Now)
                        {
                            // Return IDictionary containing any tokens used by service calls or in the app
                            tokenDictionary ??= new Dictionary<string, string>();
                            tokenDictionary[TokenCacheExtensions.AccessTokenKey] = "NewSampleToken";
                            tokenDictionary["Expiry"] = DateTime.Now.AddMinutes(5).ToString("g");
                            return ValueTask.FromResult<IDictionary<string, string>?>(tokenDictionary);
                        }

                        // Return null/default to fail the Refresh method
                        return ValueTask.FromResult<IDictionary<string, string>?>(default);
                    }), name: "CustomAuth")
                    )
                    .ConfigureServices((context, services) =>
                    {
                        // TODO: Register your services
                        //services.AddSingleton<IMyService, MyService>();
                    })
                    .UseNavigation();
        }

        protected override UIElement CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleAccounts.Views.Uno.ModuleInitializer>();
        }
    }
}
