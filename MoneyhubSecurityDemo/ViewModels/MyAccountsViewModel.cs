using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Appmilla.Moneyhub.Refit.Identity;
using Appmilla.Moneyhub.Refit.OpenFinance;
using MoneyhubSecurityDemo.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MoneyhubSecurityDemo.ViewModels
{
    public class MyAccountsViewModel : ReactiveObject
    {
        [Reactive]
        public bool IsBusy { get; set; } = false;

        public ObservableCollection<MyAccount> Accounts { get; set; } = new ObservableCollection<MyAccount>();

        private IAccessToken _accessToken;
        private IAccounts _accounts;

        public MyAccountsViewModel(IAccessToken accessToken,
                                   IAccounts accounts)
        {
            _accessToken = accessToken;
            _accounts = accounts;
        }

        public async Task OnAppearing()
        {
            try
            {
                IsBusy = true;

                var response = await _accessToken.GetAccessToken(new AccessTokenRequest() { GrantType = "client_credentials", Scope = "accounts:read savings_goals:read", Sub = "61ac9b75220d4100a72e17a2" });

                var accountsResponse = await _accounts.AccountsGetAllAsync(null, null, response.bearer_token);

                await Task.Delay(1500);

                foreach (var data in accountsResponse.Data)
                {
                    Accounts.Add(new MyAccount() { Account = data });
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
