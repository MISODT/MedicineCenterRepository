using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
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

            foreach (var item in doctorsList)
            {
                if (UserMainInteractionNewAppointmentDoctorComboBox.SelectedIndex == i)
                {
                    return item.Id;
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
                shiftDates.Add(shift.ShiftDate);
            }

            return shiftDates;
        }

        private List<string> NewAppointmentTimeComboBoxValueInitialization()
        {
            List<string> shiftTimes = new List<string>();

            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftStartActionTime, ShiftEndActionTime FROM Doctors, Shifts WHERE Id = DoctorId AND Id = {NewAppointmentDoctorComboBoxIndexInitialization()}"))
            {
                shiftTimes.Add($"{shift.ShiftStartActionTime} - {shift.ShiftEndActionTime}");
            }

            return shiftTimes;
        }

        private void UserMainInteractionNewAppointmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            NewAppointmentDoctorComboBoxValueInitialization();

            UserMainInteractionNewAppointmentDoctorComboBox.ItemsSource = userMainInteractionDoctorUserControlList;

            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = NewAppointmentDateComboBoxValueInitialization();

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = NewAppointmentTimeComboBoxValueInitialization();
        }

        private void UserMainInteractionNewAppointmentDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMainInteractionNewAppointmentDateComboBox.ItemsSource = NewAppointmentDateComboBoxValueInitialization();

            UserMainInteractionNewAppointmentDateComboBox.SelectedIndex = 0;

            UserMainInteractionNewAppointmentTimeComboBox.ItemsSource = NewAppointmentTimeComboBoxValueInitialization();

            UserMainInteractionNewAppointmentTimeComboBox.SelectedIndex = 0;
        }

        private void UserMainInteractionNewAppointmentDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void UserMainInteractionNewAppointmentTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserMainInteractionAcceptButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}