using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift
{
    public partial class UserMainInteractionShiftsPage : Page
    {
        private string userMainInteractionShiftParameterValue;


        private ObservableCollection<UserMainInteractionShiftUserControl> userMainInteractionStartShiftsUserControlCollection = new ObservableCollection<UserMainInteractionShiftUserControl>();

        private ObservableCollection<UserMainInteractionShiftUserControl> userMainInteractionRunShiftsUserControlCollection = new ObservableCollection<UserMainInteractionShiftUserControl>();


        private List<Shifts> shiftsList = new List<Shifts>();


        private List<HealingDirections> healingDirectionsList = new List<HealingDirections>();


        private List<HospitalAddresses> hospitalAddressesList = new List<HospitalAddresses>();


        private List<Cities> citiesList = new List<Cities>();

        private List<Streets> streetsList = new List<Streets>();

        private List<Houses> housesList = new List<Houses>();

        public UserMainInteractionShiftsPage(string userMainInteractionShiftParameter)
        {
            InitializeComponent();

            if(userMainInteractionShiftParameter != null)
            {
                userMainInteractionShiftParameterValue = userMainInteractionShiftParameter;

                if (userMainInteractionShiftParameter == "Текущие")
                {
                    UserMainInteractionShiftsClearButton.Visibility = Visibility.Hidden;
                }

                if (userMainInteractionShiftParameter == "Старые")
                {
                    UserMainInteractionShiftsClearButton.Visibility = Visibility.Visible;
                }

                UserMainInteractionShiftsStartItemsControlInitialization();


                UserMainInteractionShiftsItemsEmptyHandler();
            }

            InteriorControlsInitializationManager.VariablesSortingParametersComboBoxInitialization(UserMainInteractionShiftsSortingParametersComboBox);
        }

        private void UserMainInteractionShiftsItemsEmptyHandler()
        {
            if (userMainInteractionStartShiftsUserControlCollection.Count == 0)
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

        private void UserMainInteractionShiftsStartItemsControlInitialization()
        {
            if (userMainInteractionShiftParameterValue == "Текущие")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate >= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
                {
                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionId, HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var hospitalAddress in DataResponseManager.HospitalAddressesJsonDataDeserialize($"SELECT HospitalAddressId, HospitalAddressCityId, HospitalAddressStreetId, HospitalAddressHouseId FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                                {
                                    foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                    {
                                        userMainInteractionStartShiftsUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));

                                        housesList.Add(house);
                                    }

                                    streetsList.Add(street);
                                }

                                citiesList.Add(city);
                            }

                            hospitalAddressesList.Add(hospitalAddress);
                        }

                        healingDirectionsList.Add(healingDirection);
                    }

                    shiftsList.Add(shift);
                }
            }

            if (userMainInteractionShiftParameterValue == "Старые")
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId FROM Shifts WHERE ShiftDate < '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND DoctorId = {UserDataSectionsInstance.Doctor.Id}"))
                {
                    foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionId, HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shift.ShiftId}"))
                    {
                        foreach (var hospitalAddress in DataResponseManager.HospitalAddressesJsonDataDeserialize($"SELECT HospitalAddressId, HospitalAddressCityId, HospitalAddressStreetId, HospitalAddressHouseId FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                        {
                            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId, CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId}"))
                            {
                                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId, StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND CityId = {city.CityId}"))
                                {
                                    foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId, HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shift.ShiftId} AND StreetId = {street.StreetId}"))
                                    {
                                        userMainInteractionStartShiftsUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));

                                        housesList.Add(house);
                                    }

                                    streetsList.Add(street);
                                }

                                citiesList.Add(city);
                            }

                            hospitalAddressesList.Add(hospitalAddress);
                        }

                        healingDirectionsList.Add(healingDirection);
                    }

                    shiftsList.Add(shift);
                }
            }

            UserMainInteractionShiftsItemsEmptyHandler();
        }

        private void UserMainInteractionShiftsRunItemsControlInitialization()
        {
            IOrderedEnumerable<Shifts> iOrderedEnumerableShifts = null;

            if(UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "Все значения")
            {
                if (UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked)
                {
                    iOrderedEnumerableShifts = shiftsList.OrderByDescending(x => x.ShiftId);
                }

                if (UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked)
                {
                    iOrderedEnumerableShifts = shiftsList.OrderBy(x => x.ShiftId);
                }
            }

            if(UserMainInteractionShiftsSortingParametersComboBox.SelectedValue == "По дате")
            {
                if (UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked)
                {
                    iOrderedEnumerableShifts = shiftsList.OrderByDescending(x => x.ShiftDate);
                }

                if (UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked)
                {
                    iOrderedEnumerableShifts = shiftsList.OrderBy(x => x.ShiftDate);
                }
            }


            if (iOrderedEnumerableShifts != null)
            {
                foreach (var shift in iOrderedEnumerableShifts)
                {
                    foreach (var healingDirection in healingDirectionsList)
                    {
                        if (shift.ShiftHealingDirectionId == healingDirection.HealingDirectionId)
                        {
                            foreach (var hospitalAddress in hospitalAddressesList)
                            {
                                foreach (var city in citiesList)
                                {
                                    foreach (var street in streetsList)
                                    {
                                        foreach (var house in housesList)
                                        {
                                            if (hospitalAddress.HospitalAddressCityId == city.CityId && hospitalAddress.HospitalAddressStreetId == street.StreetId && hospitalAddress.HospitalAddressHouseId == house.HouseId && hospitalAddress.HospitalAddressId == shift.ShiftHospitalAddressId)
                                            {
                                                userMainInteractionRunShiftsUserControlCollection.Add(new UserMainInteractionShiftUserControl(shift, healingDirection, city, street, house));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                UserMainInteractionShiftsItemsControl.ItemsSource = userMainInteractionRunShiftsUserControlCollection;
            }

            else
            {
                UserMainInteractionShiftsItemsControl.ItemsSource = userMainInteractionStartShiftsUserControlCollection;
            }
        }

        private void UserMainInteractionShiftsPage_Loaded(object sender, RoutedEventArgs e) 
        {
            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked = false;

            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked = false;


            UserMainInteractionShiftsItemsControl.ItemsSource = userMainInteractionStartShiftsUserControlCollection;
        }

        private void UserMainInteractionShiftsSortingParametersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userMainInteractionRunShiftsUserControlCollection.Clear();


            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked = false;

            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked = false;


            UserMainInteractionShiftsSortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

            UserMainInteractionShiftsSortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");


            UserMainInteractionShiftsRunItemsControlInitialization();
        }

        private void UserMainInteractionShiftsSortingMinButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserMainInteractionVariablesSortingMinButtonFieldVisibilityOptions(UserMainInteractionShiftsSortingMinButton, UserMainInteractionShiftsSortingMaxButton);

            
            userMainInteractionRunShiftsUserControlCollection.Clear();

            UserMainInteractionShiftsRunItemsControlInitialization();
        }

        private void UserMainInteractionShiftsSortingMaxButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserMainInteractionVariablesSortingMaxButtonFieldVisibilityOptions(UserMainInteractionShiftsSortingMaxButton, UserMainInteractionShiftsSortingMinButton);

            
            userMainInteractionRunShiftsUserControlCollection.Clear();

            UserMainInteractionShiftsRunItemsControlInitialization();
        }

        private void UserMainInteractionShiftsClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shift in shiftsList)
            {
                UserDataSectionsDataOperations.UserDataRemoveShiftOperation(shift.ShiftId);
            }

            FrameManager.UserMainInteractionHomeFrame.Navigate(new UserMainInteractionShiftsPage("Старые"));
        }
    }
}