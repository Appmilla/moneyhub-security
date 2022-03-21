using System;
using System.Collections.Generic;
using CommonServiceLocator;
using MoneyhubSecurityDemo.ViewModels;
using Xamarin.Forms;

namespace MoneyhubSecurityDemo.Views
{
    public partial class MyAccountsView : ContentPage
    {
        MyAccountsViewModel ViewModel = ServiceLocator.Current.GetInstance<MyAccountsViewModel>();

        public MyAccountsView()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.InvokeOnMainThreadAsync(ViewModel.OnAppearing);
        }
    }
}
