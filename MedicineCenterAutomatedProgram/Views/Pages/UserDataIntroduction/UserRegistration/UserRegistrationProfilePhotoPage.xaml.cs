using MedicineCenterAutomatedProgram.Models.Management.External;
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
        public UserRegistrationProfilePhotoPage(UserDataSectionsBinding user)
        {
            InitializeComponent();

            DataContext = user;
        }

        private void UserRegistrationProfilePhotoPage_Loaded(object sender, RoutedEventArgs e)
        {
            Uri defaultImageUri = new Uri("/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png", UriKind.Relative);

            UserDataProfilePhotoImage.Source = new BitmapImage(defaultImageUri);
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserRegistrationPositionPage(UserDataSectionsInstance.User));

        private void SelectUserDataProfilePhotoImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            Uri selectedImageUri = new Uri(openFileDialog.FileName);

            UserDataProfilePhotoImage.Source = new BitmapImage(selectedImageUri);
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserRegistrationPersonalPage(UserDataSectionsInstance.User));
    }
}