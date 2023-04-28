using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction;
using MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages
{
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void WelcomePage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User = new Users();
        }

        private void AuthorizationNavigationButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserAuthorizationPage());

        private void RegistrationNavigationButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserRegistrationPositionPage(UserDataSectionsInstance.User));
    }
}