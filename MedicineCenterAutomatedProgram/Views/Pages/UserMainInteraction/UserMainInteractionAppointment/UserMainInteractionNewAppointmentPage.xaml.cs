using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
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
        private string userMainInteractionAppointmentIdValue;

        private List<UserMainInteractionDoctorUserControl> userMainInteractionDoctorUserControlList = new List<UserMainInteractionDoctorUserControl>();

        private List<Shifts> shiftsList = new List<Shifts>();

        public UserMainInteractionNewAppointmentPage(string userMainInteractionAppointmentId, string userMainInteractionAppointmentDescription)
        {
            InitializeComponent();

            if(userMainInteractionAppointmentId != null)
            {
                userMainInteractionAppointmentIdValue = userMainInteractionAppointmentId;

                UserMainInteractionNewAppointmentDescriptionTextBox.Text = userMainInteractionAppointmentDescription;
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
                if (UserMainInteractionNewAppointmentDoctorComboBox.SelectedIndex == i)
                {
                    return shift.ShiftId;
                }

                i++;
            }

            return "";
        }

        private void UserMainInteractionNewAppointmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionNewAppointmentDoctorComboBox.ItemsSource = userMainInteractionDoctorUserControlList;

            UserMainInteractionNewAppointmentHospitalComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentDateComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentTimeComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserMainInteractionNewAppointmentDescriptionTextBox, UserMainInteractionNewAppointmentDescriptionTextBoxHintAssist, ClearDescriptionButton);
        }

        private void UserMainInteractionNewAppointmentDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentDateComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentDateComboBox.SelectedIndex = 0;

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = OuteriorControlsInitializationManager.AppointmentTimeComboBoxValueInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentTimeComboBox.SelectedIndex = 0;

            UserMainInteractionNewAppointmentHospitalComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(AppointmentOperationsDoctorShiftComboBoxIndexInitialization());

            UserMainInteractionNewAppointmentHospitalComboBox.SelectedIndex = 0;
        }

        private void ClearDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainInteractionNewAppointmentDescriptionTextBox.Text = "";
        }

        private void UserMainInteractionNewAppointmentDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e) => UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserMainInteractionNewAppointmentDescriptionTextBox, UserMainInteractionNewAppointmentDescriptionTextBoxHintAssist, ClearDescriptionButton);

        private void UserMainInteractionAcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if(userMainInteractionAppointmentIdValue != null)
            {
                UserDataSectionsDataOperations.UserDataMainInteractionUpdateAppointmentOperation(userMainInteractionAppointmentIdValue, AppointmentOperationsDoctorShiftComboBoxIndexInitialization(), UserMainInteractionNewAppointmentDescriptionTextBox.Text);
            }

            else
            {
                UserDataSectionsDataOperations.UserDataMainInteractionNewAppointmentOperation(AppointmentOperationsDoctorShiftComboBoxIndexInitialization(), UserMainInteractionNewAppointmentDescriptionTextBox.Text);
            }   

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));
        }
    }
}