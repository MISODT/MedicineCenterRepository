using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationLocationPage : Page
    {
        public UserRegistrationLocationPage(UserDataSectionsBinding user)
        {
            InitializeComponent();

            DataContext = user;
        }

        private void NavigationNextButtonState()
        {
            if (UserDataCityComboBox.SelectedValue != null && UserDataStreetComboBox.SelectedValue != null && UserDataHouseComboBox.SelectedValue != null)
            {
                NavigateNextButton.IsEnabled = true;
            }

            else
            {
                NavigateNextButton.IsEnabled = false;
            }
        }

        private void UserRegistrationLocationPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataCityMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataStreetMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataHouseMistakeTextBlock.Visibility = Visibility.Hidden;

            UserDataCityComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressCityComboBoxInitialization();
            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();
            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();

            NavigationNextButtonState();
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserRegistrationPersonalPage(UserDataSectionsInstance.User));

        private void UserDataCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataCityComboBox, UserDataCityTextBoxHintAssist);

            UserDataExternalMistakesManager.ExternalUserDataComboBoxFieldMistakesHandler(UserDataCityComboBox, UserDataCityMistakeTextBlock, "Укажите город");

            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();

            NavigationNextButtonState();
        }

        private void UserDataStreetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataStreetComboBox, UserDataStreetTextBoxHintAssist);

            UserDataExternalMistakesManager.ExternalUserDataComboBoxFieldMistakesHandler(UserDataStreetComboBox, UserDataStreetMistakeTextBlock, "Укажите улицу");

            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();

            NavigationNextButtonState();
        }

        private void UserDataHouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataHouseComboBox, UserDataHouseTextBoxHintAssist);

            UserDataExternalMistakesManager.ExternalUserDataComboBoxFieldMistakesHandler(UserDataHouseComboBox, UserDataHouseMistakeTextBlock, "Укажите дом");

            NavigationNextButtonState();
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.ExtractUserAddressData();

            NavigationNextButtonState();

            FrameManager.MainFrame.Navigate(new UserRegistrationEducationPage(UserDataSectionsInstance.User));
        }
    }
}