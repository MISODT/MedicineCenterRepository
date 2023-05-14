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
        public UserMainInteractionNewAppointmentPage()
        {
            InitializeComponent();
        }

        private List<UserMainInteractionDoctorUserControl> DoctorInfoComboBoxValue()
        {
            List<UserMainInteractionDoctorUserControl> userMainInteractionDoctorUserControlList = new List<UserMainInteractionDoctorUserControl>();

            Doctors doctors = new Doctors();

            Shifts shifts = new Shifts();

            HealingDirections healingDirections = new HealingDirections();

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic FROM Doctors, Shifts WHERE ShiftId = DoctorId"))
            {
                doctors.Id = doctor.Id;
                doctors.ProfilePhotoUri = doctor.ProfilePhotoUri;
                doctors.Name = doctor.Name;
                doctors.Surname = doctor.Surname;
                doctors.Patronymic = doctor.Patronymic;
            }

            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId FROM Shifts, Doctors WHERE DoctorId = {doctors.Id}"))
            {
                shifts.ShiftId = shift.ShiftId;
            }

            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId"))
            {
                healingDirections.HealingDirectionTitle = healingDirection.HealingDirectionTitle;
            }

            userMainInteractionDoctorUserControlList.Add(new UserMainInteractionDoctorUserControl(doctors, healingDirections));

            return userMainInteractionDoctorUserControlList;
        }

        private void UserMainInteractionNewAppointmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionNewAppointmentDoctorComboBox.ItemsSource = DoctorInfoComboBoxValue();
        }

        private void UserMainInteractionNewAppointmentDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserMainInteractionNewAppointmentDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserMainInteractionNewAppointmentTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserMainInteractionAcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}