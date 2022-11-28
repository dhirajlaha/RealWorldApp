using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
                Preferences.Set("email", EntEmail.Text);
                Preferences.Set("password", EntPassword.Text);

                if (response)
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                }
                else
                {
                    await DisplayAlert("Oops", "Something went wrong", "Ok");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Something is wrong",ex.Message,"Ok");
            }
            
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}