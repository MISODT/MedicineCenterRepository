using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionProfilePage : Page
    {
        public UserMainInteractionProfilePage(Patients patient, Doctors doctor)
        {
            InitializeComponent();
        }

        private void UserProfileDataSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}