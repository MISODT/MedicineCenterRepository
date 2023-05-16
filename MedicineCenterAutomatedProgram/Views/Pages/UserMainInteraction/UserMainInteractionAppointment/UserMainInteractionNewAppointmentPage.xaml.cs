using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System;
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

        private List<string> NewAppointmentDateComboBoxValueInitialization()
        {
            List<string> shiftDates = new List<string>();

            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftDate FROM Doctors, Shifts WHERE Id = DoctorId AND Id = {NewAppointmentDoctorComboBoxIndexInitialization()}"))
            {
                shiftDates.Add($"{DateOnly.Parse(shift.ShiftDate)}");
            }

            return shiftDates;
        }

        private List<string> NewAppointmentTimeComboBoxValueInitialization()
        {
            List<string> shiftTimes = new List<string>();

            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftStartActionTime, ShiftEndActionTime FROM Doctors, Shifts WHERE Id = DoctorId AND Id = {NewAppointmentDoctorComboBoxIndexInitialization()}"))
            {
                shiftTimes.Add($"с {TimeOnly.Parse(shift.ShiftStartActionTime)} до {TimeOnly.Parse(shift.ShiftEndActionTime)}");
            }

            return shiftTimes;
        }

        private void UserMainInteractionNewAppointmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            NewAppointmentDoctorComboBoxValueInitialization();

            UserMainInteractionNewAppointmentDoctorComboBox.ItemsSource = userMainInteractionDoctorUserControlList;

            UserMainInteractionNewAppointmentHospitalComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(NewAppointmentDoctorComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = NewAppointmentDateComboBoxValueInitialization();

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = NewAppointmentTimeComboBoxValueInitialization();

            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserMainInteractionNewAppointmentDescriptionTextBox, UserMainInteractionNewAppointmentDescriptionTextBoxHintAssist, ClearDescriptionButton);
        }

        private void UserMainInteractionNewAppointmentDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = NewAppointmentDateComboBoxValueInitialization();

            UserMainInteractionNewAppointmentDateComboBox.SelectedIndex = 0;

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = NewAppointmentTimeComboBoxValueInitialization();

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

        }
    }
}