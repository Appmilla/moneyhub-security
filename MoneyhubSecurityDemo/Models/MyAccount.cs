using System;
using Appmilla.Moneyhub.Refit.OpenFinance;

namespace MoneyhubSecurityDemo.Models
{
    public class MyAccount
    {
        public Account Account { get; set; }

        public string FormattedAmount { get => string.Format("£ {0:0.00}", Account.Balance.Amount.Value / 100m); }

        public string FormattedAccountType { get => $"{Account.AccountType} {Account.Type}"; }
    }
}
