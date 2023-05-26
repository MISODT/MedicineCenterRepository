using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionAppointmentsReceivingPage : Page
    {
        private ObservableCollection<UserMainInteractionAppointmentReceiveUserControl> userMainInteractionAppointmentReceiveUserControlCollection = new ObservableCollection<UserMainInteractionAppointmentReceiveUserControl>();

        public UserMainInteractionAppointmentsReceivingPage()
        {
            InitializeComponent();
        }

        private void UserMainInteractionAppointmentsReceivingPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionApointmentsReceiving();

            UserMainInteractionAppointmentReceivesItemsControl.ItemsSource = userMainInteractionAppointmentReceiveUserControlCollection;

            UserMainInteractionAppointmentsReceivingEmptyHandler();
        }

        private void UserMainInteractionAppointmentsReceivingEmptyHandler()
        {
            if (userMainInteractionAppointmentReceiveUserControlCollection.Count == 0)
            {
                UserMainInteractionAppointmentsReceivesEmptyTextBlock.Visibility = Visibility.Visible;
            }

            else
            {
                UserMainInteractionAppointmentsReceivesEmptyTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void UserMainInteractionApointmentsReceiving()
        {
            foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId FROM Appointments, Shifts WHERE AppointmentShiftId = ShiftId AND Shifts.DoctorId = {UserDataSectionsInstance.Doctor.Id} AND AppointmentStatus = 'Отправлен'"))
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Patients, Appointments WHERE PatientId = Id AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                                {
                                    foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                    {
                                        userMainInteractionAppointmentReceiveUserControlCollection.Add(new UserMainInteractionAppointmentReceiveUserControl(appointment, shift, patient, healingDirection, city, street, house));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            UserMainInteractionAppointmentsReceivingEmptyHandler();
        }
    }
}