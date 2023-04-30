using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
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
    }
}