using MedicineCenterAutomatedProgram.Models.Management;
using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using Microsoft.Win32;
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
            if (SectionsInstance.SectionsBinding.UserProfilePhotoUri != "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png")
            {
                UserDataProfilePhotoImage.Source = ByteImageValuesManager.GetImageFromBytes(SectionsInstance.SectionsBinding.UserProfilePhotoUri);
            }

            else
            {
                Uri userImageUri = new Uri(SectionsInstance.SectionsBinding.UserProfilePhotoUri, UriKind.RelativeOrAbsolute);

                UserDataProfilePhotoImage.Source = new BitmapImage(userImageUri);
            }
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationPositionPage(SectionsInstance.SectionsBinding));

        private void SelectUserDataProfilePhotoImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

            openFileDialog.ShowDialog();

            if (InteriorControlsInitializationManager.InitializeProfilePhotoImage(openFileDialog) != null)
            {
                UserDataProfilePhotoImage.Source = ByteImageValuesManager.GetImageFromBytes(SectionsInstance.SectionsBinding.UserProfilePhotoUri = ByteImageValuesManager.GetImageByteStringBuilder(InteriorControlsInitializationManager.InitializeProfilePhotoImage(openFileDialog)));
            }
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationPersonalPage(SectionsInstance.SectionsBinding));
    }
}