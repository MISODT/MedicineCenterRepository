using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment
{
    public partial class UserMainInteractionMyAppointmentsPage : Page
    {
        private List<UserMainInteractionAppointmentUserControl> userMainInteractionAppointmentUserControlList = new List<UserMainInteractionAppointmentUserControl>();

        private List<Appointments> appointmentsList = new List<Appointments>();

        public UserMainInteractionMyAppointmentsPage()
        {
            InitializeComponent();
        }

        private void AppointmentListBoxBoxValueInitialization()
        {
            if (UserDataSectionsInstance.Patient != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                {
                    userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment));

                    appointmentsList.Add(appointment);
                }
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
                {
                    userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment));

                    appointmentsList.Add(appointment);
                }
            }
        }

        private void UserMainInteractionMyAppointmentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            AppointmentListBoxBoxValueInitialization();

            UserMainInteractionMyAppointmentsListBox.ItemsSource = userMainInteractionAppointmentUserControlList;
        }
    }
}