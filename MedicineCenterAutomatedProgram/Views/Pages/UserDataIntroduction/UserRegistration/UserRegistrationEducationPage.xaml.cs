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
        public UserRegistrationEducationPage(SectionsBindingManager user)
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

            InteriorControlsInitializationManager.InitializeYearComboBox(UserDataUniversityStartEducationYearComboBox, DateTime.Now.Year);
            InteriorControlsInitializationManager.InitializeYearComboBox(UserDataUniversityEndEducationYearComboBox, DateTime.Now.Year);
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationLocationPage(SectionsInstance.SectionsBinding));

        private void UserDataSchoolCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataSchoolCityComboBox, UserDataSchoolCityComboBoxHintAssist);

            UserDataSchoolTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTypeComboBoxInitialization();
        }

        private void UserDataSchoolTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataSchoolTypeComboBox, UserDataSchoolTypeComboBoxHintAssist);

            UserDataSchoolTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTitleComboBoxInitialization();
        }

        private void UserDataSchoolTitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => FieldsViewManager.ChangeComboBoxView(UserDataSchoolTitleComboBox, UserDataSchoolComboBoxHintAssist);

        private void UserDataUniversityCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataUniversityCityComboBox, UserDataUniversityCityComboBoxHintAssist);

            UserDataUniversityTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTypeComboBoxInitialization();
        }

        private void UserDataUniversityTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataUniversityTypeComboBox, UserDataUniversityTypeComboBoxHintAssist);

            UserDataUniversityTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTitleComboBoxInitialization();
        }

        private void UserDataUniversityTitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => FieldsViewManager.ChangeComboBoxView(UserDataUniversityTitleComboBox, UserDataUniversityTitleComboBoxHintAssist);

        private void UserDataUniversityStartEducationYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => FieldsViewManager.ChangeComboBoxView(UserDataUniversityStartEducationYearComboBox, UserDataUniversityStartEducationYearComboBoxHintAssist);

        private void UserDataUniversityEndEducationYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => FieldsViewManager.ChangeComboBoxView(UserDataUniversityEndEducationYearComboBox, UserDataUniversityEndEducationYearComboBoxHintAssist);

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e) 
        {
            SectionsInstance.SectionsBinding.ExtractUserEducationData();

            FrameManager.MainWindowFrame.Navigate(new UserRegistrationCredentialsPage()); 
        }
    }
}