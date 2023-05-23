using ControlzEx.Standard;
using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment
{
    public partial class UserMainInteractionAppointmentsPage : Page
    {
        private string userMainInteractionAppointmentParameterValue = "";

        private bool isUserMainInteractionSortingMinButtonClicked = false;

        private bool isUserMainInteractionSortingMaxButtonClicked = false;

        private ObservableCollection<UserMainInteractionAppointmentUserControl> userMainInteractionAppointmentUserControlList = new ObservableCollection<UserMainInteractionAppointmentUserControl>();

        private ObservableCollection<Appointments> appointmentsList = new ObservableCollection<Appointments>();

        private ObservableCollection<Shifts> shiftsList = new ObservableCollection<Shifts>();

        public UserMainInteractionAppointmentsPage(string userMainInteractionAppointmentParameter)
        {
            InitializeComponent();

            userMainInteractionAppointmentParameterValue = userMainInteractionAppointmentParameter;

            InteriorControlsInitializationManager.AppointmentSortingParametersComboBoxInitialization(UserMainInteractionAppointmentsSortingParametersComboBox);

            if(userMainInteractionAppointmentParameter == "Текущие")
            {
                NowAppointmentsInitialization();

                UserMainInteractionAppointmentsClearButton.Visibility = Visibility.Hidden;
            }

            if(userMainInteractionAppointmentParameter == "Старые")
            {
                OldAppointmentsInitialization();

                UserMainInteractionAppointmentsClearButton.Visibility = Visibility.Visible;
            }

            UserMainInteractionAppointmentsEmptyHandler();
        }

        private void UserMainInteractionSortingMinButtonVisibility(Button userMainInteractionSortingMinButton, Button userMainInteractionSortingMaxButton)
        {
            if (!isUserMainInteractionSortingMinButtonClicked)
            {
                userMainInteractionSortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                isUserMainInteractionSortingMinButtonClicked = true;

                isUserMainInteractionSortingMaxButtonClicked = false;

                userMainInteractionSortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }

            else
            {
                userMainInteractionSortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                isUserMainInteractionSortingMinButtonClicked = false;

                isUserMainInteractionSortingMaxButtonClicked = true;

                userMainInteractionSortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }
        }

        private void UserMainInteractionSortingMaxButtonVisibility(Button userMainInteractionSortingMaxButton, Button userMainInteractionSortingMinButton)
        {
            if (!isUserMainInteractionSortingMaxButtonClicked)
            {
                userMainInteractionSortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                isUserMainInteractionSortingMaxButtonClicked = true;

                isUserMainInteractionSortingMinButtonClicked = false;

                userMainInteractionSortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }

            else
            {
                userMainInteractionSortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                isUserMainInteractionSortingMaxButtonClicked = false;

                isUserMainInteractionSortingMinButtonClicked = true;

                userMainInteractionSortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
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
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                        shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                        shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                        foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                            {
                                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                    {
                                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                        {
                                            userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                        }
                                    }
                                }
                            }
                        }

                        shiftsList.Add(shift);
                    }

                    appointmentsList.Add(appointment);
                }
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                    {
                        shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                        shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                        shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                        foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                            {
                                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                    {
                                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
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
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
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
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
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

        private void AllAppointmentsSortingDescendingInitialization()
        {
            if (userMainInteractionAppointmentParameterValue == "Текущие")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id} ORDER BY AppointmentId DESC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
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
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY AppointmentId DESC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
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
            }

            if (userMainInteractionAppointmentParameterValue == "Старые")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id} ORDER BY AppointmentId DESC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
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
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY AppointmentId DESC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
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
            }
        }

        private void AllAppointmentsSortingAscendingInitialization()
        {
            if (userMainInteractionAppointmentParameterValue == "Текущие")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id} ORDER BY AppointmentId ASC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
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
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY AppointmentId ASC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
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
            }

            if(userMainInteractionAppointmentParameterValue == "Старые")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients WHERE PatientId = Id AND PatientId = {UserDataSectionsInstance.Patient.Id} ORDER BY AppointmentId ASC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
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
                    foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Doctors WHERE DoctorId = Id AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY AppointmentId ASC"))
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId AND AppointmentId = {appointment.AppointmentId}"))
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
            }
        }

        private void DateAppointmentsSortingDescendingInitialization()
        {
            if (userMainInteractionAppointmentParameterValue == "Текущие")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate DESC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                            {
                                                userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                            }
                                        }
                                    }
                                }
                            }

                            appointmentsList.Add(appointment);
                        }
                    }
                }

                if (UserDataSectionsInstance.Doctor != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate DESC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                            {
                                                userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                            }
                                        }
                                    }
                                }
                            }

                            appointmentsList.Add(appointment);
                        }
                    }
                }
            }

            if (userMainInteractionAppointmentParameterValue == "Старые")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate DESC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
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

                            appointmentsList.Add(appointment);
                        }
                    }
                }

                if (UserDataSectionsInstance.Doctor != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate DESC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
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

                            appointmentsList.Add(appointment);
                        }
                    }
                }
            }
        }

        private void DateAppointmentsSortingAscendingInitialization()
        {
            if (userMainInteractionAppointmentParameterValue == "Текущие")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate ASC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                            {
                                                userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                            }
                                        }
                                    }
                                }
                            }

                            appointmentsList.Add(appointment);
                        }
                    }
                }

                if (UserDataSectionsInstance.Doctor != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate ASC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
                        {
                            shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                            shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                            shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                {
                                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id}"))
                                    {
                                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND CityId = {city.CityId}"))
                                        {
                                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND DoctorId = {doctor.Id} AND StreetId = {street.StreetId}"))
                                            {
                                                userMainInteractionAppointmentUserControlList.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                            }
                                        }
                                    }
                                }
                            }

                            appointmentsList.Add(appointment);
                        }
                    }
                }
            }

            if (userMainInteractionAppointmentParameterValue == "Старые")
            {
                if (UserDataSectionsInstance.Patient != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate ASC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
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

                            appointmentsList.Add(appointment);
                        }
                    }
                }

                if (UserDataSectionsInstance.Doctor != null)
                {
                    foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Appointments WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND AppointmentShiftId = ShiftId ORDER BY ShiftDate ASC"))
                    {
                        foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription FROM Appointments, Patients, Shifts WHERE PatientId = Id AND AppointmentShiftId = ShiftId AND ShiftId = {shift.ShiftId} AND PatientId = {UserDataSectionsInstance.Patient.Id}"))
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

                            appointmentsList.Add(appointment);
                        }
                    }
                }
            }
        }

        private void UserMainInteractionAppointmentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionAppointmentUserControlList;
        }

        private void UserMainInteractionAppointmentsSortingParametersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserMainInteractionAppointmentsSortingMinButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainInteractionSortingMinButtonVisibility(UserMainInteractionAppointmentsSortingMinButton, UserMainInteractionAppointmentsSortingMaxButton);

            if (UserMainInteractionAppointmentsSortingParametersComboBox.SelectedValue == "Все записи" && isUserMainInteractionSortingMinButtonClicked)
            {
                userMainInteractionAppointmentUserControlList.Clear();

                appointmentsList.Clear();

                AllAppointmentsSortingDescendingInitialization();
            }

            if(UserMainInteractionAppointmentsSortingParametersComboBox.SelectedValue == "По дате" && isUserMainInteractionSortingMinButtonClicked)
            {
                userMainInteractionAppointmentUserControlList.Clear();

                appointmentsList.Clear();

                DateAppointmentsSortingDescendingInitialization();

                foreach (var appointment in appointmentsList)
                {
                    MessageBox.Show(appointment.AppointmentId);
                }
            }
        }

        private void UserMainInteractionAppointmentsSortingMaxButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainInteractionSortingMaxButtonVisibility(UserMainInteractionAppointmentsSortingMaxButton, UserMainInteractionAppointmentsSortingMinButton);

            if (UserMainInteractionAppointmentsSortingParametersComboBox.SelectedValue == "Все записи" && isUserMainInteractionSortingMaxButtonClicked)
            {
                userMainInteractionAppointmentUserControlList.Clear();

                appointmentsList.Clear();

                AllAppointmentsSortingAscendingInitialization();
            }

            if (UserMainInteractionAppointmentsSortingParametersComboBox.SelectedValue == "По дате" && isUserMainInteractionSortingMaxButtonClicked)
            {
                userMainInteractionAppointmentUserControlList.Clear();

                appointmentsList.Clear();

                DateAppointmentsSortingAscendingInitialization();
            }
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