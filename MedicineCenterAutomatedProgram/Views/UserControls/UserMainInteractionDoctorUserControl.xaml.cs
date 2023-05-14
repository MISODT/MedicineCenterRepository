using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionDoctorUserControl : UserControl
    {
        public UserMainInteractionDoctorUserControl(Doctors doctor, HealingDirections healingDirection)
        {
            InitializeComponent();

            DataContext = doctor;

            UserDataHealingDirectionTitleTextBlock.Text = healingDirection.HealingDirectionTitle;
        }

        private void UserMainInteractionDoctorUserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}