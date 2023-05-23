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

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift
{
    public partial class UserMainInteractionShiftsPage : Page
    {
        private string userMainInteractionShiftParameterValue;

        private ObservableCollection<UserMainInteractionShiftUserControl> userMainInteractionShiftUserControlCollection = new ObservableCollection<UserMainInteractionShiftUserControl>();

        private ObservableCollection<Shifts> shiftsCollection = new ObservableCollection<Shifts>();

        public UserMainInteractionShiftsPage(string userMainInteractionShiftParameter)
        {
            InitializeComponent();

            if(userMainInteractionShiftParameter != null)
            {
                userMainInteractionShiftParameterValue = userMainInteractionShiftParameter;

                if (userMainInteractionShiftParameter == "Текущие")
                {
                    NowShiftsInitialization();

                    UserMainInteractionShiftsClearButton.Visibility = Visibility.Hidden;
                }

                if (userMainInteractionShiftParameter == "Старые")
                {
                    OldShiftsInitialization();

                    UserMainInteractionShiftsClearButton.Visibility = Visibility.Visible;
                }

                UserMainInteractionShiftsEmptyHandler();
            }

            InteriorControlsInitializationManager.VariablesSortingParametersComboBoxInitialization(UserMainInteractionShiftsSortingParametersComboBox);
        }

        private void UserMainInteractionShiftsEmptyHandler()
        {
            if (userMainInteractionShiftUserControlCollection.Count == 0)
            {
                UserMainInteractionShiftsEmptyTextBlock.Visibility = Visibility.Visible;

                UserMainInteractionShiftsClearButton.IsEnabled = false;
            }

            else
            {
                UserMainInteractionShiftsEmptyTextBlock.Visibility = Visibility.Hidden;

                UserMainInteractionShiftsClearButton.IsEnabled = true;
            }
        }

        private void NowShiftsInitialization()
        {
            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
            {
                shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                {
                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                        {
                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                            {
                                userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                            }
                        }
                    }
                }

                shiftsCollection.Add(shift);
            }

            UserMainInteractionShiftsEmptyHandler();
        }

        private void OldShiftsInitialization()
        {
            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
            {
                shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                {
                    foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                        {
                            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                            {
                                userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                            }
                        }
                    }
                }

                shiftsCollection.Add(shift);
            }

            UserMainInteractionShiftsEmptyHandler();
        }

        private void AllShiftsSortingDescendingInitialization()
        {
            if(userMainInteractionShiftParameterValue == "Текущие")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftId DESC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            if(userMainInteractionShiftParameterValue == "Старые")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftId DESC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            UserMainInteractionShiftsEmptyHandler();
        }

        private void AllShiftsSortingAscendingInitialization()
        {
            if (userMainInteractionShiftParameterValue == "Текущие")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftId ASC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            if (userMainInteractionShiftParameterValue == "Старые")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftId ASC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            UserMainInteractionShiftsEmptyHandler();
        }

        private void DateShiftsSortingDescendingInitialization()
        {
            if (userMainInteractionShiftParameterValue == "Текущие")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftDate DESC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            if (userMainInteractionShiftParameterValue == "Старые")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftDate DESC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            UserMainInteractionShiftsEmptyHandler();
        }

        private void DateShiftsSortingAscendingInitialization()
        {
            if (userMainInteractionShiftParameterValue == "Текущие")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftDate ASC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            if (userMainInteractionShiftParameterValue == "Старые")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id} ORDER BY ShiftDate ASC"))
                {
                    shift.ShiftDate = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

                    shift.ShiftStartActionTime = TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString();

                    shift.ShiftEndActionTime = TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString();

                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                            {
                                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                {
                                    userMainInteractionShiftUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                }
                            }
                        }
                    }

                    shiftsCollection.Add(shift);
                }
            }

            UserMainInteractionShiftsEmptyHandler();
        }

        private void UserMainInteractionShiftsPage_Loaded(object sender, RoutedEventArgs e) 
        {
            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked = false;

            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked = false;

            UserMainInteractionShiftsItemsControl.ItemsSource = userMainInteractionShiftUserControlCollection;
        }

        private void UserMainInteractionShiftsSortingParametersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked)
            {
                if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "Все значения")
                {
                    userMainInteractionShiftUserControlCollection.Clear();

                    shiftsCollection.Clear();

                    AllShiftsSortingDescendingInitialization();
                }

                if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "По дате")
                {
                    userMainInteractionShiftUserControlCollection.Clear();

                    shiftsCollection.Clear();

                    DateShiftsSortingDescendingInitialization();
                }
            }

            if (UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked)
            {
                if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "Все значения")
                {
                    userMainInteractionShiftUserControlCollection.Clear();

                    shiftsCollection.Clear();

                    AllShiftsSortingAscendingInitialization();
                }

                if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "По дате")
                {
                    userMainInteractionShiftUserControlCollection.Clear();

                    shiftsCollection.Clear();

                    DateShiftsSortingAscendingInitialization();
                }
            }
        }

        private void UserMainInteractionShiftsSortingMinButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserMainInteractionVariablesSortingMinButtonFieldVisibilityOptions(UserMainInteractionShiftsSortingMinButton, UserMainInteractionShiftsSortingMaxButton);

            if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "Все значения" && UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked)
            {
                userMainInteractionShiftUserControlCollection.Clear();

                shiftsCollection.Clear();

                AllShiftsSortingDescendingInitialization();
            }

            if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "По дате" && UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked)
            {
                userMainInteractionShiftUserControlCollection.Clear();

                shiftsCollection.Clear();

                DateShiftsSortingDescendingInitialization();
            }
        }

        private void UserMainInteractionShiftsSortingMaxButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserMainInteractionVariablesSortingMaxButtonFieldVisibilityOptions(UserMainInteractionShiftsSortingMaxButton, UserMainInteractionShiftsSortingMinButton);

            if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "Все значения" && UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked)
            {
                userMainInteractionShiftUserControlCollection.Clear();

                shiftsCollection.Clear();

                AllShiftsSortingDescendingInitialization();
            }

            if (UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "По дате" && UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked)
            {
                userMainInteractionShiftUserControlCollection.Clear();

                shiftsCollection.Clear();

                DateShiftsSortingDescendingInitialization();
            }
        }

        private void UserMainInteractionShiftsClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shift in shiftsCollection)
            {
                UserDataSectionsDataOperations.UserDataRemoveShiftOperation(shift.ShiftId);
            }

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionShiftsPage("Старые"));
        }
    }
}