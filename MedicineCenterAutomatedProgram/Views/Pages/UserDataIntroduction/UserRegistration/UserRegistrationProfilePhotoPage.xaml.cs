using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationProfilePhotoPage : Page
    {
        public UserRegistrationProfilePhotoPage()
        {
            InitializeComponent();
        }

        private void UserRegistrationProfilePhotoPage_Loaded(object sender, RoutedEventArgs e)
        {
            Uri userImageUri = new Uri(SectionsInstance.SectionsBinding.UserProfilePhotoUri, UriKind.RelativeOrAbsolute);

            UserDataProfilePhotoImage.Source = new BitmapImage(userImageUri);
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationPositionPage(SectionsInstance.SectionsBinding));

        private void SelectUserDataProfilePhotoImageButton_Click(object sender, RoutedEventArgs e)
        {
            SectionsInstance.SectionsBinding.UserProfilePhotoUri = InteriorControlsInitializationManager.InitializeProfilePhotoImage(UserDataProfilePhotoImage);
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationPersonalPage(SectionsInstance.SectionsBinding));
    }
}