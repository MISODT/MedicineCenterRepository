using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment
{
    public partial class UserMainInteractionNewAppointmentPage : Page
    {
        private List<UserMainInteractionDoctorUserControl> userMainInteractionDoctorUserControlList = new List<UserMainInteractionDoctorUserControl>();

        private List<Doctors> doctorsList = new List<Doctors>();

        public UserMainInteractionNewAppointmentPage()
        {
            InitializeComponent();
        }

        private void NewAppointmentDoctorComboBoxValueInitialization()
        {
            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId"))
            {
                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND DoctorId = {doctor.Id}"))
                {
                    userMainInteractionDoctorUserControlList.Add(new UserMainInteractionDoctorUserControl(doctor, healingDirection));
                }

                doctorsList.Add(doctor);
            }
        }

        private string NewAppointmentDoctorComboBoxIndexInitialization()
        {
            int i = 0;

            foreach (var doctor in doctorsList)
            {
                if (UserMainInteractionNewAppointmentDoctorComboBox.SelectedIndex == i)
                {
                    return doctor.Id;
                }

                i++;
            }

            return "";
        }

        private void UserMainInteractionNewAppointmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            NewAppointmentDoctorComboBoxValueInitialization();

            UserMainInteractionNewAppointmentDoctorComboBox.ItemsSource = userMainInteractionDoctorUserControlList;

            UserMainInteractionNewAppointmentHospitalComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentDateComboBoxValueInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentTimeComboBoxValueInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserMainInteractionNewAppointmentDescriptionTextBox, UserMainInteractionNewAppointmentDescriptionTextBoxHintAssist, ClearDescriptionButton);
        }

        private void UserMainInteractionNewAppointmentDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentDateComboBoxValueInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentDateComboBox.SelectedIndex = 0;

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentTimeComboBoxValueInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentTimeComboBox.SelectedIndex = 0;

            OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentHospitalComboBox.SelectedIndex = 0;
        }

        private void ClearDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainInteractionNewAppointmentDescriptionTextBox.Text = "";
        }

        private void UserMainInteractionNewAppointmentDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e) => UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserMainInteractionNewAppointmentDescriptionTextBox, UserMainInteractionNewAppointmentDescriptionTextBoxHintAssist, ClearDescriptionButton);

        private void UserMainInteractionAcceptButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.Appointment.AppointmentStatus = "Отправлен";
            UserDataSectionsInstance.Appointment.AppointmentDescription = UserMainInteractionNewAppointmentDescriptionTextBox.Text;

            OperationsManager.UserDataMainInteractionNewAppointmentOperation(NewAppointmentDoctorComboBoxIndexInitialization());

            //FrameManager.UserMainInteractionHomePageFrame.Navigate(new);
        }
    }
}