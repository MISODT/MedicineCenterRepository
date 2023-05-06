using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
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

        public void UserProfileDataGenderDefinition()
        {
            if (UserDataSectionsInstance.User.UserGender == "Мужской")
            {
                SelectMaleGenderRadioButton.IsChecked = true;
            }

            if (UserDataSectionsInstance.User.UserGender == "Женский")
            {
                SelectFemaleGenderRadioButton.IsChecked = true;
            }

            if (UserDataSectionsInstance.User.UserGender == "Не указан")
            {
                SelectUndefinedGenderRadioButton.IsChecked = true;
            }
        }

        private void UserMainInteractionProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataProfilePhotoUriMistakeTextBlock.Visibility = Visibility.Hidden;

            UserDataExternalMistakesManager.ExternalUserDataProfilePhotoUriMistakesHandler(UserDataProfilePhotoUriTextBlock, UserDataProfilePhotoUriMistakeTextBlock, "Файл не был найден");

            UserDataNameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataSurnameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataPatronymicMistakeTextBlock.Visibility = Visibility.Hidden;

            UserProfileDataGenderDefinition();

            UserDataCityComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressCityComboBoxSelectedValueInitialization();
            UserDataStreetComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressStreetComboBoxSelectedValueInitialization();
            UserDataHouseComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressHouseComboBoxSelectedValueInitialization();

            UserDataCityComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressCityComboBoxInitialization();
            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();
            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();
        }

        private void SelectUserDataProfilePhotoImageButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserProfilePhotoUri = InteriorControlsInitializationManager.ProfilePhotoImageInitialization(UserDataProfilePhotoImage);
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
            UserDataSectionsInstance.User.UserGender = "Мужской";
        }

        private void SelectFemaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGender = "Женский";
        }

        private void SelectUndefinedGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserGender = "Не указан";
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