using System;
using System.Collections.Generic;
using CommonServiceLocator;
using MoneyhubSecurityDemo.ViewModels;
using Xamarin.Forms;

namespace MoneyhubSecurityDemo.Views
{
    public partial class WelcomeView : ContentPage
    {
        WelcomeViewModel ViewModel = ServiceLocator.Current.GetInstance<WelcomeViewModel>();

        public WelcomeView()
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
