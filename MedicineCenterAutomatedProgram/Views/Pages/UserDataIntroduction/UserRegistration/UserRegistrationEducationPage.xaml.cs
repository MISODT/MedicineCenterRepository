using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationEducationPage : Page
    {
        public UserRegistrationEducationPage(UserDataSectionsBinding user)
        {
            InitializeComponent();

            DataContext = user;
        }

        private void UserRegistrationEducationPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataSchoolCityComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolCityComboBoxInitialization();
            UserDataSchoolTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTypeComboBoxInitialization();
            UserDataSchoolTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTitleComboBoxInitialization();

            UserDataUniversityCityComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityCityComboBoxInitialization();
            UserDataUniversityTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTypeComboBoxInitialization();
            UserDataUniversityTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTitleComboBoxInitialization();

            InteriorControlsInitializationManager.YearComboBoxInitialization(UserDataUniversityStartEducationYearComboBox, DateTime.Now.Year);
            InteriorControlsInitializationManager.YearComboBoxInitialization(UserDataUniversityEndEducationYearComboBox, DateTime.Now.Year);
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserRegistrationLocationPage(UserDataSectionsInstance.User));

        private void UserDataSchoolCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataSchoolCityComboBox, UserDataSchoolCityComboBoxHintAssist);

            UserDataSchoolTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTypeComboBoxInitialization();
        }

        private void UserDataSchoolTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataSchoolTypeComboBox, UserDataSchoolTypeComboBoxHintAssist);

            UserDataSchoolTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTitleComboBoxInitialization();
        }

        private void UserDataSchoolTitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataSchoolTitleComboBox, UserDataSchoolComboBoxHintAssist);

        private void UserDataUniversityCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataUniversityCityComboBox, UserDataUniversityCityComboBoxHintAssist);

            UserDataUniversityTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTypeComboBoxInitialization();
        }

        private void UserDataUniversityTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataUniversityTypeComboBox, UserDataUniversityTypeComboBoxHintAssist);

            UserDataUniversityTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTitleComboBoxInitialization();
        }

        private void UserDataUniversityTitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataUniversityTitleComboBox, UserDataUniversityTitleComboBoxHintAssist);

        private void UserDataUniversityStartEducationYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataUniversityStartEducationYearComboBox, UserDataUniversityStartEducationYearComboBoxHintAssist);

        private void UserDataUniversityEndEducationYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataUniversityEndEducationYearComboBox, UserDataUniversityEndEducationYearComboBoxHintAssist);

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e) 
        {
            UserDataSectionsInstance.User.ExtractUserEducationData();

            FrameManager.MainFrame.Navigate(new UserRegistrationCredentialsPage()); 
        }
    }
}