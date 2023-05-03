using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionHomePage : Page
    {
        public UserMainInteractionHomePage(Patients patient, Doctors doctor)
        {
            InitializeComponent();

            DataContext = patient;
            DataContext = doctor;
        }

        private void UserMainInteractionHomePage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserDataProfileButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserMainInteractionProfilePage(UserDataSectionsInstance.Patient, UserDataSectionsInstance.Doctor));

        private void UserDataOutProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsRemember.RememberUserDataConfigRemove();

            FrameManager.MainFrame.Navigate(new WelcomePage());
        }
    }
}