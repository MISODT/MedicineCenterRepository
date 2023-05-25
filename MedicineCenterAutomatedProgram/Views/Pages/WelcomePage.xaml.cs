using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction;
using MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration;
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
            UserDataSectionsInstance.User = new UserDataSectionsBinding();

            UserDataSectionsInstance.Patient = null;

            UserDataSectionsInstance.Doctor = null;
        }

        private void AuthorizationNavigationButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserAuthorizationPage());

        private void RegistrationNavigationButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationPositionPage(UserDataSectionsInstance.User));
    }
}