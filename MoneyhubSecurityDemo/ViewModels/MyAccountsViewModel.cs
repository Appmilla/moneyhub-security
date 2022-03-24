using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Appmilla.Moneyhub.Refit.Identity;
using Appmilla.Moneyhub.Refit.OpenFinance;
using MoneyhubSecurityDemo.Models;
using MoneyhubSecurityDemo.Services;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MoneyhubSecurityDemo.ViewModels
{
    public class MyAccountsViewModel : ReactiveObject
    {
        [Reactive]
        public bool IsBusy { get; set; } = false;

        public ObservableCollection<MyAccount> Accounts { get; set; } = new ObservableCollection<MyAccount>();

        private readonly IHttpClientFactory _httpFactory;
        private IAccounts _accounts;

        public MyAccountsViewModel(IHttpClientFactory httpFactory,
                                   IAccounts accounts)
        {
            _httpFactory = httpFactory;
            _accounts = accounts;
        }

        public async Task OnAppearing()
        {
            try
            {
                IsBusy = true;

                var _httpClient = _httpFactory.CreateClient();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SecureStorage.AccessToken ?? string.Empty);

                HttpResponseMessage response = await _httpClient.GetAsync($"{Constants.ApiUri}moneyhubaccess");

                string jsonAccessTokenResponse = await response.Content.ReadAsStringAsync();

                var accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(jsonAccessTokenResponse);

                var accountsResponse = await _accounts.AccountsGetAllAsync(null, null, accessTokenResponse.bearer_token);

                foreach (var data in accountsResponse.Data)
                {
                    Accounts.Add(new MyAccount() { Account = data });
                }
            }
            catch
            {
                // An error has occured
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
