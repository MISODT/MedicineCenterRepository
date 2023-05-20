using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment
{
    public partial class UserMainInteractionAppointmentsPage : Page
    {
        private List<UserMainInteractionAppointmentUserControl> userMainInteractionAppointmentUserControlList = new List<UserMainInteractionAppointmentUserControl>();

        private List<Appointments> appointmentsList = new List<Appointments>();

        public UserMainInteractionAppointmentsPage(string userMainInteractionAppointmentParameter)
        {
            InitializeComponent();

            if(userMainInteractionAppointmentParameter == "Текущие")
            {
                NowAppointmentsInitialization();
            }

            if(userMainInteractionAppointmentParameter == "Старые")
            {
                OldAppointmentsInitialization();
            }
        }

        private void UserMainInteractionAppointmentsEmptyHandler()
        {
            if (userMainInteractionAppointmentUserControlList.Count == 0)
            {
                UserMainInteractionAppointmentsEmptyTextBlock.Visibility = Visibility.Visible;

                UserMainInteractionAppointmentsClearButton.IsEnabled = false;
            }

            else
            {
                UserMainInteractionAppointmentsEmptyTextBlock.Visibility = Visibility.Hidden;

                UserMainInteractionAppointmentsClearButton.IsEnabled = true;
            }
        }

        private void NowAppointmentsInitialization()
        {
            if (UserDataSectionsInstance.Patient != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE (ShiftDate = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) OR (ShiftDate > '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                        shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                        shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                        foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND DoctorId = {doctor.Id}"))
                            {
                                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                    {
                                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                        {
                                            userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    appointmentsList.Add(appointment);
                }
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE (ShiftDate = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) OR (ShiftDate > '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                        shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                        shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                        foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND DoctorId = {doctor.Id}"))
                            {
                                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                    {
                                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                        {
                                            userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    appointmentsList.Add(appointment);
                }
            }

            UserMainInteractionAppointmentsEmptyHandler();
        }

        private void OldAppointmentsInitialization()
        {
            if (UserDataSectionsInstance.Patient != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE (ShiftDate = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) OR (ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                        shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                        shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                        foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND DoctorId = {doctor.Id}"))
                            {
                                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                    {
                                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                        {
                                            userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    appointmentsList.Add(appointment);
                }
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE (ShiftDate = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) OR (ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ('{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' < ShiftStartActionTime OR '{DateTime.Now.Hour}:{DateTime.Now.Minute}:00' > ShiftEndActionTime)) AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                        shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                        shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                        foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND DoctorId = {doctor.Id}"))
                            {
                                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                    {
                                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                        {
                                            userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    appointmentsList.Add(appointment);
                }
            }

            UserMainInteractionAppointmentsEmptyHandler();
        }

        private void UserMainInteractionAppointmentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionAppointmentUserControlList;
        }

        private void UserMainInteractionAppointmentsClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var appointment in appointmentsList)
            {
                UserDataSectionsDataOperations.UserDataRemoveAppointmentOperation(appointment.AppointmentId);
            }

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionAppointmentsPage("Старые"));
        }
    }
}