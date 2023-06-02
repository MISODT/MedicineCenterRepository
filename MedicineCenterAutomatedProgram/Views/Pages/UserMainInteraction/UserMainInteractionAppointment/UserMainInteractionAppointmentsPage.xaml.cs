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
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment
{
    public partial class UserMainInteractionAppointmentsPage : Page
    {
        private string userMainInteractionAppointmentParameterValue;


        private ObservableCollection<UserMainInteractionAppointmentUserControl> userMainInteractionStartAppointmentsUserControlCollection = new ObservableCollection<UserMainInteractionAppointmentUserControl>();

        private ObservableCollection<UserMainInteractionAppointmentUserControl> userMainInteractionRunAppointmentsUserControlCollection = new ObservableCollection<UserMainInteractionAppointmentUserControl>();


        private List<Appointments> appointmentsList = new List<Appointments>();


        private List<Shifts> shiftsList = new List<Shifts>();


        private List<Doctors> doctorsList = new List<Doctors>();


        private List<HealingDirections> healingDirectionsList = new List<HealingDirections>();


        private List<HospitalAddresses> hospitalAddressesList = new List<HospitalAddresses>();


        private List<Cities> citiesList = new List<Cities>();

        private List<Streets> streetsList = new List<Streets>();

        private List<Houses> housesList = new List<Houses>();

        public UserMainInteractionAppointmentsPage(string userMainInteractionAppointmentParameter)
        {
            InitializeComponent();

            if(userMainInteractionAppointmentParameter != null)
            {
                userMainInteractionAppointmentParameterValue = userMainInteractionAppointmentParameter;

                if (userMainInteractionAppointmentParameter == "Текущие")
                {
                    UserMainInteractionAppointmentsClearButton.Visibility = Visibility.Hidden;
                }

                if (userMainInteractionAppointmentParameter == "Старые")
                {
                    UserMainInteractionAppointmentsClearButton.Visibility = Visibility.Visible;
                }

                UserMainInteractionAppointmentsStartItemsControlInitialization();


                UserMainInteractionAppointmentsItemsEmptyHandler();
            }

            InteriorControlsInitializationManager.InitializeComboBoxSortingParameters(UserMainInteractionAppointmentsSortingParametersComboBox);
        }

        private void UserMainInteractionAppointmentsItemsEmptyHandler()
        {
            if (userMainInteractionStartAppointmentsUserControlCollection.Count == 0)
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

        private void UserMainInteractionAppointmentsStartItemsControlInitialization()
        {
            string userMainInteractionAppointmentParameterSign = "";


            string userMainInteractionAppointmentUserParameter = "";


            if (userMainInteractionAppointmentParameterValue == "Текущие")
            {
                userMainInteractionAppointmentParameterSign = ">=";
            }

            if(userMainInteractionAppointmentParameterValue == "Старые")
            {
                userMainInteractionAppointmentParameterSign = "<";
            }


            if (SectionsInstance.Patient != null)
            {
                userMainInteractionAppointmentUserParameter = $"Appointments.PatientId = {SectionsInstance.Patient.Id}";
            }

            if (SectionsInstance.Doctor != null)
            {
                userMainInteractionAppointmentUserParameter = $"Appointments.DoctorId = {SectionsInstance.Doctor.Id}";
            }

            foreach (var appointment in DataResponseManager.AppointmentsJsonDataDeserialize($"SELECT AppointmentId, AppointmentStatus, AppointmentDescription, AppointmentShiftId FROM Appointments, Shifts WHERE AppointmentShiftId = ShiftId AND {userMainInteractionAppointmentUserParameter}"))
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT DISTINCT(ShiftId), ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, Shifts.DoctorId, ShiftHospitalAddressId FROM Shifts, Doctors, HealingDirections, Appointments WHERE ShiftDate {userMainInteractionAppointmentParameterSign} '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}' AND ShiftId = AppointmentShiftId AND AppointmentId = {appointment.AppointmentId}"))
                {
                    foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, Shifts WHERE DoctorId = Id AND ShiftId = {shift.ShiftId}"))
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
                                            userMainInteractionStartAppointmentsUserControlCollection.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));

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

                        doctorsList.Add(doctor);
                    }

                    shiftsList.Add(shift);
                }

                appointmentsList.Add(appointment);
            }


            appointmentsList = appointmentsList.DistinctBy(x => x.AppointmentId).ToList();

            shiftsList = shiftsList.DistinctBy(x => x.ShiftId).ToList();

            doctorsList = doctorsList.DistinctBy(x => x.Id).ToList();

            healingDirectionsList = healingDirectionsList.DistinctBy(x => x.HealingDirectionId).ToList();

            hospitalAddressesList = hospitalAddressesList.DistinctBy(x => x.HospitalAddressId).ToList();

            citiesList = citiesList.DistinctBy(x => x.CityId).ToList();

            streetsList = streetsList.DistinctBy(x => x.StreetId).ToList();

            housesList = housesList.DistinctBy(x => x.HouseId).ToList();


            UserMainInteractionAppointmentsItemsEmptyHandler();
        }

        private void UserMainInteractionAppointmentsRunItemsControlInitialization()
        {
            IOrderedEnumerable<Appointments> iOrderedEnumerableAppointments = null;

            IOrderedEnumerable<Shifts> iOrderedEnumerableShifts = null;


            if (UserMainInteractionAppointmentsSortingParametersComboBox.SelectedValue == "Все значения")
            {
                if (FieldsViewManager.IsSortingMinButtonClicked)
                {
                    iOrderedEnumerableAppointments = appointmentsList.OrderByDescending(x => x.AppointmentId);
                }

                else if (FieldsViewManager.IsSortingMaxButtonClicked)
                {
                    iOrderedEnumerableAppointments = appointmentsList.OrderBy(x => x.AppointmentId);
                }

                if (iOrderedEnumerableAppointments != null)
                {
                    foreach (var appointment in iOrderedEnumerableAppointments)
                    {
                        foreach (var doctor in doctorsList)
                        {
                            foreach (var shift in shiftsList)
                            {
                                if (shift.ShiftId == appointment.AppointmentShiftId)
                                {
                                    if (doctor.Id == shift.DoctorId)
                                    {
                                        foreach (var healingDirection in healingDirectionsList)
                                        {
                                            if (healingDirection.HealingDirectionId == shift.ShiftHealingDirectionId)
                                            {
                                                foreach (var hospitalAddress in hospitalAddressesList)
                                                {
                                                    if (hospitalAddress.HospitalAddressId == shift.ShiftHospitalAddressId)
                                                    {
                                                        foreach (var city in citiesList)
                                                        {
                                                            foreach (var street in streetsList)
                                                            {
                                                                foreach (var house in housesList)
                                                                {
                                                                    if (hospitalAddress.HospitalAddressCityId == city.CityId && hospitalAddress.HospitalAddressStreetId == street.StreetId && hospitalAddress.HospitalAddressHouseId == house.HouseId)
                                                                    {
                                                                        userMainInteractionRunAppointmentsUserControlCollection.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionRunAppointmentsUserControlCollection;
                }
            }

            else if (UserMainInteractionAppointmentsSortingParametersComboBox.SelectedValue == "По дате")
            {
                if (FieldsViewManager.IsSortingMinButtonClicked)
                {
                    iOrderedEnumerableShifts = shiftsList.OrderByDescending(x => x.ShiftDate);
                }

                else if (FieldsViewManager.IsSortingMaxButtonClicked)
                {
                    iOrderedEnumerableShifts = shiftsList.OrderBy(x => x.ShiftDate);
                }

                if(iOrderedEnumerableShifts != null)
                {
                    foreach (var shift in iOrderedEnumerableShifts)
                    {
                        foreach (var appointment in appointmentsList)
                        {
                            if (appointment.AppointmentShiftId == shift.ShiftId)
                            {
                                foreach(var doctor in doctorsList)
                                {
                                    if (doctor.Id == shift.DoctorId)
                                    {
                                        foreach (var healingDirection in healingDirectionsList)
                                        {
                                            if (healingDirection.HealingDirectionId == shift.ShiftHealingDirectionId)
                                            {
                                                foreach (var hospitalAddress in hospitalAddressesList)
                                                {
                                                    if (hospitalAddress.HospitalAddressId == shift.ShiftHospitalAddressId)
                                                    {
                                                        foreach (var city in citiesList)
                                                        {
                                                            foreach (var street in streetsList)
                                                            {
                                                                foreach (var house in housesList)
                                                                {
                                                                    if (hospitalAddress.HospitalAddressCityId == city.CityId && hospitalAddress.HospitalAddressStreetId == street.StreetId && hospitalAddress.HospitalAddressHouseId == house.HouseId)
                                                                    {
                                                                        userMainInteractionRunAppointmentsUserControlCollection.Add(new UserMainInteractionAppointmentUserControl(appointment, shift, doctor, healingDirection, city, street, house));
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionRunAppointmentsUserControlCollection;
                }
            }

            else
            {
                UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionStartAppointmentsUserControlCollection;
            }
        }

        private void UserMainInteractionAppointmentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.IsSortingMinButtonClicked = false;

            FieldsViewManager.IsSortingMaxButtonClicked = false;


            UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionStartAppointmentsUserControlCollection;
        }

        private void UserMainInteractionAppointmentsSortingParametersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userMainInteractionRunAppointmentsUserControlCollection.Clear();

            UserMainInteractionAppointmentsRunItemsControlInitialization();
        }

        private void UserMainInteractionAppointmentsSortingMinButton_Click(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.ChangeSortingMinButtonFieldView(UserMainInteractionAppointmentsSortingMinButton, UserMainInteractionAppointmentsSortingMaxButton);


            userMainInteractionRunAppointmentsUserControlCollection.Clear();

            UserMainInteractionAppointmentsRunItemsControlInitialization();
        }

        private void UserMainInteractionAppointmentsSortingMaxButton_Click(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.ChangeSortingMaxButtonFieldView(UserMainInteractionAppointmentsSortingMaxButton, UserMainInteractionAppointmentsSortingMinButton);


            userMainInteractionRunAppointmentsUserControlCollection.Clear();

            UserMainInteractionAppointmentsRunItemsControlInitialization();
        }

        private void UserMainInteractionAppointmentsClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var appointment in appointmentsList)
            {
                SectionsOperationsManager.RemoveAppointmentOperation(appointment.AppointmentId);
            }

            FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsPage("Старые"));
        }
    }
}