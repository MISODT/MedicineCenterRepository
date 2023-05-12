using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationPositionPage : Page
    {
        public UserRegistrationPositionPage(UserDataSectionsBinding user)
        {
            InitializeComponent();

            DataContext = user;
        }

        private void NavigationNextButtonState()
        {
            if (SelectPatientRadioButton.IsChecked == false && SelectDoctorRadioButton.IsChecked == false)
            {
                NavigateNextButton.IsEnabled = false;
            }

            else
            {
                NavigateNextButton. IsEnabled = true;
            }
        }

        private void UserRegistrationPositionPage_Loaded(object sender, RoutedEventArgs e) => NavigationNextButtonState();

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new WelcomePage());

        private void SelectPatientRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserPositionIsPatient = true;

            UserDataSectionsInstance.User.UserPositionIsDoctor = false;

            NavigationNextButtonState();
        }

        private void SelectDoctorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserPositionIsDoctor = true;

            UserDataSectionsInstance.User.UserPositionIsPatient = false;

            NavigationNextButtonState();
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationProfilePhotoPage());
    }
}