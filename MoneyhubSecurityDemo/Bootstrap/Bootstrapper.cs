using System;
using System.Net.Http.Headers;
using Appmilla.Moneyhub.Refit.Identity;
using Appmilla.Moneyhub.Refit.OpenFinance;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using Moneyhub.ApiClient.Config;
using MoneyhubSecurityDemo.ViewModels;
using Refit;

namespace MoneyhubSecurityDemo.Bootstrap
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            var builder = new ContainerBuilder();

            var services = new ServiceCollection();

            var refitSettings = new RefitSettings(new NewtonsoftJsonContentSerializer());

            services.AddRefitClient<IAccessToken>(refitSettings)
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri("https://identity.moneyhub.co.uk");

                    var configuration = new MoneyhubConfiguration
                    {
                        ClientId = "",
                        ClientSecret = ""
                    };

                    var authToken = configuration.GetAuthorization();

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
                });

            services.AddRefitClient<IAccounts>(refitSettings)
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri("https://api.moneyhub.co.uk/v2.0");
                });

            builder.Populate(services);

            builder.RegisterType<MyAccountsViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<WelcomeViewModel>().AsSelf().SingleInstance();

            var container = builder.Build();

            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }
    }
}
