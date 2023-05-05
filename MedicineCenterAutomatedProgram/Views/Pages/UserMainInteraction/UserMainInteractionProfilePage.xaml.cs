using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionProfilePage : Page
    {
        public UserMainInteractionProfilePage(UserDataSectionsBinding user)
        {
            InitializeComponent();

            DataContext = user;
        }

        private void UserMainInteractionProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataProfilePhotoUriMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataNameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataSurnameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataPatronymicMistakeTextBlock.Visibility = Visibility.Hidden;

            UserDataCityComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressCityComboBoxSelectedValueInitialization();
            UserDataStreetComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressStreetComboBoxSelectedValueInitialization();
            UserDataHouseComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressHouseComboBoxSelectedValueInitialization();

            UserDataCityComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressCityComboBoxInitialization();
            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();
            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();
        }

        private void UserDataNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataNameTextBox, UserDataNameMistakeTextBlock, "Укажите имя");
        }

        private void ClearNameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataNameTextBox.Text = "";
        }

        private void UserDataSurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataSurnameTextBox, UserDataSurnameMistakeTextBlock, "Укажите фамилию");
        }

        private void ClearSurnameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSurnameTextBox.Text = "";
        }

        private void UserDataPatronymicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataPatronymicTextBox, UserDataPatronymicMistakeTextBlock, "Укажите отчество");
        }

        private void ClearPatronymicButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataPatronymicTextBox.Text = "";
        }

        private void SelectMaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGenderIsMale = true;

            UserDataSectionsInstance.User.UserGender = "Мужской";
        }

        private void SelectFemaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGenderIsFemale = true;

            UserDataSectionsInstance.User.UserGender = "Женский";
        }

        private void UserDataCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataCityComboBox, UserDataCityTextBoxHintAssist);

            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();
        }

        private void UserDataStreetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataStreetComboBox, UserDataStreetTextBoxHintAssist);

            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();
        }

        private void UserDataHouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataHouseComboBox, UserDataHouseTextBoxHintAssist);

        private void UserProfileDataSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}