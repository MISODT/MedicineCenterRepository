using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment
{
    public partial class UserMainInteractionAppointmentOperationsPage : Page
    {
        private Appointments appointments;


        private List<UserMainInteractionDoctorUserControl> userMainInteractionDoctorUserControlList = new List<UserMainInteractionDoctorUserControl>();


        private List<Shifts> shiftsList = new List<Shifts>();

        public UserMainInteractionAppointmentOperationsPage(Appointments appointment)
        {
            InitializeComponent();

            if (appointment != null)
            {
                appointments = appointment;

                UserMainInteractionAppointmentOperationsDescriptionTextBox.Text = appointment.AppointmentDescription;
            }

            AppointmentDoctorComboBoxValueInitialization();
        }

        private void AppointmentDoctorComboBoxValueInitialization()
        {
            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId FROM Shifts, Doctors WHERE DoctorId = Id"))
            {
                foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic FROM Doctors, Shifts WHERE ShiftId = {shift.ShiftId}"))
                {
                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                    {
                        userMainInteractionDoctorUserControlList.Add(new UserMainInteractionDoctorUserControl(doctor, healingDirection));
                    }
                }

                shiftsList.Add(shift);
            }
        }

        private string AppointmentOperationsDoctorShiftComboBoxIndexInitialization()
        {
            int i = 0;

            foreach (var shift in shiftsList)
            {
                if (UserMainInteractionAppointmentOperationsDoctorComboBox.SelectedIndex == i)
                {
                    return shift.ShiftId;
                }

                i++;
            }

            return "";
        }

        private void UserMainInteractionAppointmentOperationsPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionAppointmentOperationsDoctorComboBox.ItemsSource = userMainInteractionDoctorUserControlList;

            UserMainInteractionAppointmentOperationsHospitalComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionAppointmentOperationsDateComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentDateComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionAppointmentOperationsTimeComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentTimeComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserMainInteractionAppointmentOperationsDescriptionTextBox, UserMainInteractionAppointmentOperationsDescriptionTextBoxHintAssist, UserMainInteractionAppointmentOperationsClearDescriptionButton);
        }

        private void UserMainInteractionAppointmentOperationsDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMainInteractionAppointmentOperationsDateComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentDateComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionAppointmentOperationsDateComboBox.SelectedIndex = 0;

            UserMainInteractionAppointmentOperationsTimeComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentTimeComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionAppointmentOperationsTimeComboBox.SelectedIndex = 0;

            UserMainInteractionAppointmentOperationsHospitalComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionAppointmentOperationsHospitalComboBox.SelectedIndex = 0;
        }

        private void UserMainInteractionAppointmentOperationsDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserMainInteractionAppointmentOperationsDescriptionTextBox, UserMainInteractionAppointmentOperationsDescriptionTextBoxHintAssist, UserMainInteractionAppointmentOperationsClearDescriptionButton);
            
        }

        private void UserMainInteractionAppointmentOperationsClearDescriptionButton_Click(object sender, RoutedEventArgs e) 
        {
            UserMainInteractionAppointmentOperationsDescriptionTextBox.Text = "";
        }

        private void UserMainInteractionAcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointments != null)
            {
                UserDataSectionsDataOperations.UserDataMainInteractionUpdateAppointmentOperation(appointments.AppointmentId, AppointmentOperationsDoctorShiftComboBoxIndexInitialization(), UserMainInteractionAppointmentOperationsDescriptionTextBox.Text);
            }

            else
            {
                UserDataSectionsDataOperations.UserDataMainInteractionNewAppointmentOperation(AppointmentOperationsDoctorShiftComboBoxIndexInitialization(), UserMainInteractionAppointmentOperationsDescriptionTextBox.Text);
            }

            FrameManager.UserMainInteractionHomeFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));
        }
    }
}