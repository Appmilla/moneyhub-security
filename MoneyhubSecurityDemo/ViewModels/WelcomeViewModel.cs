using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using MoneyhubSecurityDemo.Services;
using MoneyhubSecurityDemo.Shell;
using ReactiveUI;

namespace MoneyhubSecurityDemo.ViewModels
{
    public class WelcomeViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Login { get; }

        public WelcomeViewModel()
        {
            Login = ReactiveCommand.CreateFromTask(OnLogin);
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        private async Task OnLogin()
        {
            try
            {
                var options = new OidcClientOptions
                {
                    Authority = Constants.AuthorityUri,
                    ClientId = Constants.ClientId,
                    Scope = Constants.Scope,
                    RedirectUri = Constants.RedirectUri,
                    Browser = new Browser()
                };

                var _oidcClient = new OidcClient(options);

                var _loginResult = await _oidcClient.LoginAsync(new LoginRequest());

                if (_loginResult.IsError)
                {
                    Console.WriteLine("ERROR: {0}", _loginResult.Error);
                    return;
                }
                else
                {
                    SecureStorage.AccessToken = _loginResult.AccessToken;
                    App.Current.MainPage = new ApplicationShell();
                }
            }
            catch
            {
                return;
            }
        }
    }
}
