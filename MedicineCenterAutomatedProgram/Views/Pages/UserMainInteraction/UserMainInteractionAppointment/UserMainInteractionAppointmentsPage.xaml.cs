using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            InteriorControlsInitializationManager.VariablesSortingParametersComboBoxInitialization(UserMainInteractionAppointmentsSortingParametersComboBox);
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


            UserMainInteractionAppointmentsItemsEmptyHandler();
        }

        private void UserMainInteractionAppointmentsRunItemsControlInitialization()
        {

        }

        private void UserMainInteractionAppointmentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked = false;

            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked = false;


            UserMainInteractionAppointmentsItemsControl.ItemsSource = userMainInteractionStartAppointmentsUserControlCollection;
        }

        private void UserMainInteractionAppointmentsSortingParametersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userMainInteractionRunAppointmentsUserControlCollection.Clear();


            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMinButtonClicked = false;

            UserDataFieldsViewManager.IsUserMainInteractionVariablesSortingMaxButtonClicked = false;


            UserMainInteractionAppointmentsSortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

            UserMainInteractionAppointmentsSortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");


            UserMainInteractionAppointmentsRunItemsControlInitialization();
        }

        private void UserMainInteractionAppointmentsSortingMinButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserMainInteractionVariablesSortingMinButtonFieldVisibilityOptions(UserMainInteractionAppointmentsSortingMinButton, UserMainInteractionAppointmentsSortingMaxButton);


            userMainInteractionRunAppointmentsUserControlCollection.Clear();

            UserMainInteractionAppointmentsRunItemsControlInitialization();
        }

        private void UserMainInteractionAppointmentsSortingMaxButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserMainInteractionVariablesSortingMaxButtonFieldVisibilityOptions(UserMainInteractionAppointmentsSortingMaxButton, UserMainInteractionAppointmentsSortingMinButton);


            userMainInteractionRunAppointmentsUserControlCollection.Clear();

            UserMainInteractionAppointmentsRunItemsControlInitialization();
        }

        private void UserMainInteractionAppointmentsClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var appointment in appointmentsList)
            {
                UserDataSectionsDataOperations.UserDataMainInteractionRemoveAppointmentOperation(appointment.AppointmentId);
            }

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionAppointmentsPage("Старые"));
        }
    }
}