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

        private void DoctorInfoComboBoxValue()
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

        private void UserMainInteractionNewAppointmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            DoctorInfoComboBoxValue();

            UserMainInteractionNewAppointmentDoctorComboBox.ItemsSource = userMainInteractionDoctorUserControlList;
        }

        private void UserMainInteractionNewAppointmentDoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;

            foreach(var item in doctorsList)
            {
                if(UserMainInteractionNewAppointmentDoctorComboBox.SelectedIndex == i)
                {
                    MessageBox.Show(item.Id);
                }

                i++;
            }
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