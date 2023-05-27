using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationPersonalPage : Page
    {
        public UserRegistrationPersonalPage(UserDataSectionsBinding user)
        {
            InitializeComponent();

            DataContext = user;
        }

        private void NavigationNextButtonState()
        {
            if (!string.IsNullOrWhiteSpace(UserDataNameTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataSurnameTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataPatronymicTextBox.Text) && UserDataDayOfBirthComboBox.SelectedItem != null && UserDataMonthOfBirthComboBox.SelectedItem != null && UserDataYearOfBirthComboBox.SelectedItem != null && (SelectMaleGenderRadioButton.IsChecked == true || SelectFemaleGenderRadioButton.IsChecked == true || SelectUndefinedGenderRadioButton.IsChecked == true))
            {
                NavigateNextButton.IsEnabled = true;
            }

            else
            {
                NavigateNextButton.IsEnabled = false;
            }
        }

        private void UserRegistrationPersonalDataPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataNameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataSurnameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataPatronymicMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataDateOfBirthMistakeTextBlock.Visibility = Visibility.Hidden;

            InteriorControlsInitializationManager.DayComboBoxInitialization(UserDataDayOfBirthComboBox, UserDataMonthOfBirthComboBox.SelectedIndex + 1);
            InteriorControlsInitializationManager.MonthComboBoxInitialization(UserDataMonthOfBirthComboBox);
            InteriorControlsInitializationManager.YearComboBoxInitialization(UserDataYearOfBirthComboBox, DateTime.Now.Year);

            NavigationNextButtonState();
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationProfilePhotoPage());

        private void UserDataNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataNameTextBox, UserDataNameMistakeTextBlock, "Укажите имя");

            NavigationNextButtonState();
        }

        private void ClearNameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataNameTextBox.Text = "";
        }

        private void UserDataSurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataSurnameTextBox, UserDataSurnameMistakeTextBlock, "Укажите фамилию");

            NavigationNextButtonState();
        }

        private void ClearSurnameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSurnameTextBox.Text = "";
        }

        private void UserDataPatronymicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataPatronymicTextBox, UserDataPatronymicMistakeTextBlock, "Укажите отчество");

            NavigationNextButtonState();
        }

        private void ClearPatronymicButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataPatronymicTextBox.Text = "";
        }

        private void UserDataDayOfBirthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataDayOfBirthComboBox, UserDataDayOfBirthComboBoxHintAssist);

            NavigationNextButtonState();
        }

        private void UserDataMonthOfBirthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataMonthOfBirthComboBox, UserDataMonthOfBirthComboBoxHintAssist);

            InteriorControlsInitializationManager.DayComboBoxInitialization(UserDataDayOfBirthComboBox, UserDataMonthOfBirthComboBox.SelectedIndex + 1);

            InteriorControlsInitializationManager.MonthNumberComboBoxInitialization(UserDataMonthOfBirthComboBox);

            NavigationNextButtonState();
        }

        private void UserDataYearOfBirthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataYearOfBirthComboBox, UserDataYearOfBirthComboBoxHintAssist);

            UserDataExternalMistakesManager.ExternalUserDataDateOfBirthMistakesHandler(UserDataDayOfBirthComboBox, UserDataMonthOfBirthComboBox, UserDataYearOfBirthComboBox, UserDataDateOfBirthMistakeTextBlock);

            NavigationNextButtonState();
        }

        private void SelectMaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGenderIsMale = true;

            UserDataSectionsInstance.User.UserGender = "Мужской";

            NavigationNextButtonState();
        }

        private void SelectFemaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGenderIsFemale = true;

            UserDataSectionsInstance.User.UserGender = "Женский";

            NavigationNextButtonState();
        }

        private void SelectUndefinedGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGenderIsUndefined = true;

            UserDataSectionsInstance.User.UserGender = "Не указан";

            NavigationNextButtonState();
        }

        private void NavigateNextButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataNameTextBox, UserDataNameMistakeTextBlock, "Укажите имя") && 
               UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataSurnameTextBox, UserDataSurnameMistakeTextBlock, "Укажите фамилию") &&
               UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataPatronymicTextBox, UserDataPatronymicMistakeTextBlock, "Укажите отчество") &&
               UserDataExternalMistakesManager.ExternalUserDataDateOfBirthMistakesHandler(UserDataDayOfBirthComboBox, UserDataMonthOfBirthComboBox, UserDataYearOfBirthComboBox, UserDataDateOfBirthMistakeTextBlock))
            {
                FrameManager.MainWindowFrame.Navigate(new UserRegistrationLocationPage(UserDataSectionsInstance.User));
            }
        }
    }
}