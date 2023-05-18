using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionAppointmentUserControl : UserControl
    {
        public UserMainInteractionAppointmentUserControl(Appointments appointment)
        {
            InitializeComponent();

            DataContext = appointment;
        }
    }
}