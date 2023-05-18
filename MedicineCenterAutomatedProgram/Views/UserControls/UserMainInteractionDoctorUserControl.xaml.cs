using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
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
    }
}