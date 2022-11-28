using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        private MediaFile file;
        public MyAccountPage()
        {
            InitializeComponent();
        }
        
        private void TapUploadImage_Tapped(object sender, EventArgs e)
        {
            GetImageFromGallery();
        }

        private async void GetImageFromGallery()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Oops", "Your device does not support this feature.", "OK");
                    return;
                }

                file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                    return;

                ImgProfile.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    AddImageToServer();
                    return stream;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("OOps",ex.Message, "OK");
            }
            
        }

        private async void AddImageToServer()
        {
            try
            {
                var imageArray = FromFile.ToArray(file.GetStream());
                file.Dispose();

                var response = await ApiService.EditUserProfile(imageArray);
                if (response) return;

                await DisplayAlert("Something went wrong", "Please upload the user profile again", "Ok");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Oops",ex.Message, "OK");
            }
            

        }
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var profileImage = await ApiService.GetUserProfileImage();

        //    if (string.IsNullOrEmpty(profileImage.imageUrl))
        //    {
        //        ImgProfile.Source = "userPlaceholder.png";
        //    }
        //    else
        //    {
        //        ImgProfile.Source = profileImage.fullImageUrl;
        //    }
        //}

        private void TapChangePassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}