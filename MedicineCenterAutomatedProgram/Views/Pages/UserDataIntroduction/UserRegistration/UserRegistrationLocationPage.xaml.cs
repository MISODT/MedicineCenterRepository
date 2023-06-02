using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationLocationPage : Page
    {
        public UserRegistrationLocationPage(SectionsBindingManager user)
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

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationPersonalPage(SectionsInstance.SectionsBinding));

        private void UserDataCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataCityComboBox, UserDataCityTextBoxHintAssist);

            ExternalMistakesManager.CheckComboBoxMistakes(UserDataCityComboBox, UserDataCityMistakeTextBlock, "Укажите город");

            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();

            NavigationNextButtonState();
        }

        private void UserDataStreetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataStreetComboBox, UserDataStreetTextBoxHintAssist);

            ExternalMistakesManager.CheckComboBoxMistakes(UserDataStreetComboBox, UserDataStreetMistakeTextBlock, "Укажите улицу");

            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();

            NavigationNextButtonState();
        }

        private void UserDataHouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataHouseComboBox, UserDataHouseTextBoxHintAssist);

            ExternalMistakesManager.CheckComboBoxMistakes(UserDataHouseComboBox, UserDataHouseMistakeTextBlock, "Укажите дом");

            NavigationNextButtonState();
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e)
        {
            SectionsInstance.SectionsBinding.ExtractUserAddressData();

            NavigationNextButtonState();

            FrameManager.MainWindowFrame.Navigate(new UserRegistrationEducationPage(SectionsInstance.SectionsBinding));
        }
    }
}