using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
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

        private void ProfileSaveButtonState()
        {
            if (!string.IsNullOrWhiteSpace(UserDataNameTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataSurnameTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataPatronymicTextBox.Text) && UserDataCityComboBox.SelectedItem != null && UserDataStreetComboBox.SelectedItem != null && UserDataHouseComboBox.SelectedItem != null)
            {
                UserProfileDataSaveButton.IsEnabled = true;
            }

            else
            {
                UserProfileDataSaveButton.IsEnabled = false;
            }
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

            if(UserDataExternalMistakesManager.ExternalUserDataProfilePhotoUriMistakesHandler(UserDataProfilePhotoUriTextBlock, UserDataProfilePhotoUriMistakeTextBlock, "Файл не был найден"))
            {
                UserDataSectionsInstance.User.UserProfilePhotoUri = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
            }

            UserDataNameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataSurnameMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataPatronymicMistakeTextBlock.Visibility = Visibility.Hidden;

            UserProfileDataGenderDefinition();

            UserDataCityComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressCityComboBoxSelectedValueInitialization();
            UserDataStreetComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressStreetComboBoxSelectedValueInitialization();
            UserDataHouseComboBox.SelectedValue = OuteriorControlsInitializationManager.AddressHouseComboBoxSelectedValueInitialization();

            UserDataSchoolCityComboBox.SelectedValue = OuteriorControlsInitializationManager.SchoolCityComboBoxSelectedValueInitialization();
            UserDataSchoolTypeComboBox.SelectedValue = OuteriorControlsInitializationManager.SchoolTypeComboBoxSelectedValueInitialization();
            UserDataSchoolTitleComboBox.SelectedValue = OuteriorControlsInitializationManager.SchoolTitleComboBoxSelectedValueInitialization();

            UserDataUniversityCityComboBox.SelectedValue = OuteriorControlsInitializationManager.UniversityCityComboBoxSelectedValueInitialization();
            UserDataUniversityTypeComboBox.SelectedValue = OuteriorControlsInitializationManager.UniversityTypeComboBoxSelectedValueInitialization();
            UserDataUniversityTitleComboBox.SelectedValue = OuteriorControlsInitializationManager.UniversityTitleComboBoxSelectedValueInitialization();

            UserDataCityComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressCityComboBoxInitialization();
            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();
            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();

            UserDataSchoolCityComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolCityComboBoxInitialization();
            UserDataSchoolTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTypeComboBoxInitialization();
            UserDataSchoolTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.SchoolTitleComboBoxInitialization();

            UserDataUniversityCityComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityCityComboBoxInitialization();
            UserDataUniversityTypeComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTypeComboBoxInitialization();
            UserDataUniversityTitleComboBox.ItemsSource = OuteriorControlsInitializationManager.UniversityTitleComboBoxInitialization();

            InteriorControlsInitializationManager.YearComboBoxInitialization(UserDataUniversityStartEducationYearComboBox, DateTime.Now.Year);
            InteriorControlsInitializationManager.YearComboBoxInitialization(UserDataUniversityEndEducationYearComboBox, DateTime.Now.Year);
        }

        private void SelectUserDataProfilePhotoImageButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsInstance.User.UserProfilePhotoUri = InteriorControlsInitializationManager.ProfilePhotoImageInitialization(UserDataProfilePhotoImage);
        }

        private void UserDataNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataNameTextBox, UserDataNameMistakeTextBlock, "Укажите имя");

            ProfileSaveButtonState();
        }

        private void ClearNameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataNameTextBox.Text = "";
        }

        private void UserDataSurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataSurnameTextBox, UserDataSurnameMistakeTextBlock, "Укажите фамилию");

            ProfileSaveButtonState();
        }

        private void ClearSurnameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSurnameTextBox.Text = "";
        }

        private void UserDataPatronymicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataPatronymicTextBox, UserDataPatronymicMistakeTextBlock, "Укажите отчество");

            ProfileSaveButtonState();
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

            ProfileSaveButtonState();
        }

        private void UserDataStreetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataStreetComboBox, UserDataStreetTextBoxHintAssist);

            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();

            ProfileSaveButtonState();
        }

        private void UserDataHouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserDataHouseComboBox, UserDataHouseTextBoxHintAssist);

            ProfileSaveButtonState();
        }

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

        private void UserDataOldPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) 
        {
            UserDataFieldsViewManager.UserDataHiddenPasswordFieldVisibilityOptions(UserDataOldPasswordPasswordBoxHintAssist, UserDataOldPasswordPasswordBox, ClearOldPasswordButton);

            UserDataExternalMistakesManager.ExternalUserDataPasswordMistakesHandler(null, null, UserDataOldPasswordPasswordBox, UserDataNewPasswordPasswordBox, null, UserDataPasswordMistakeTextBlock);
        }

        private void ClearOldPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataOldPasswordPasswordBox.Password = "";
        }

        private void UserDataNewPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataHiddenPasswordFieldVisibilityOptions(UserDataNewPasswordPasswordBoxHintAssist, UserDataNewPasswordPasswordBox, ClearNewPasswordButton);

            UserDataExternalMistakesManager.ExternalUserDataPasswordMistakesHandler(null, null, UserDataOldPasswordPasswordBox, UserDataNewPasswordPasswordBox, null, UserDataPasswordMistakeTextBlock);
        }

        private void ClearNewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataNewPasswordPasswordBox.Password = "";
        }

        private void UserProfileDataSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserDataExternalMistakesManager.ExternalUserDataPasswordMistakesHandler(null, null, UserDataOldPasswordPasswordBox, UserDataNewPasswordPasswordBox, null, UserDataPasswordMistakeTextBlock))
            {
                UserDataSectionsInstance.User.ExtractUserAddressData();

                UserDataSectionsInstance.User.ExtractUserEducationData();

                if(UserDataOldPasswordPasswordBox.Password != "" && UserDataNewPasswordPasswordBox.Password != "")
                {
                    if (UserDataSectionsInstance.User.UserPositionIsPatient)
                    {
                        WebResponseManager.ResponseFromRequestQuery($"UPDATE Patients SET Password = '{UserDataCryptionManager.UserDataEncrypt(UserDataNewPasswordPasswordBox.Password)}' WHERE Id = {UserDataSectionsInstance.User.UserId}");
                    }

                    if (UserDataSectionsInstance.User.UserPositionIsDoctor)
                    {
                        WebResponseManager.ResponseFromRequestQuery($"UPDATE Doctors SET Password = '{UserDataCryptionManager.UserDataEncrypt(UserDataNewPasswordPasswordBox.Password)}' WHERE Id = {UserDataSectionsInstance.User.UserId}");
                    }

                    UserDataSectionsInstance.User.UserPassword = UserDataCryptionManager.UserDataEncrypt(UserDataNewPasswordPasswordBox.Password);
                }

                CredentialsUserDataOperationsManager.UserDataUpdateOperation();

                if (UserDataSectionsInstance.Patient != null)
                {
                    UserDataSectionsInstance.Patient = new Patients()
                    {
                        Id = UserDataSectionsInstance.User.UserId,
                        ProfilePhotoUri = UserDataSectionsInstance.User.UserProfilePhotoUri,
                        Name = UserDataSectionsInstance.User.UserName,
                        Surname = UserDataSectionsInstance.User.UserSurname,
                        Patronymic = UserDataSectionsInstance.User.UserPatronymic,
                        DateOfBirth = UserDataSectionsInstance.User.UserDateOfBirth,
                        Gender = UserDataSectionsInstance.User.UserGender,
                        AddressId = UserDataSectionsInstance.User.UserAddressId,
                        SchoolId = UserDataSectionsInstance.User.UserSchoolId,
                        UniversityId = UserDataSectionsInstance.User.UserUniversityId,
                        UniversityStartEducationYear = Convert.ToString(UserDataSectionsInstance.User.UserUniversityStartEducationYear),
                        UniversityEndEducationYear = Convert.ToString(UserDataSectionsInstance.User.UserUniversityEndEducationYear),
                        Login = UserDataSectionsInstance.User.UserLogin,
                        Password = UserDataSectionsInstance.User.UserPassword
                    };
                }

                if (UserDataSectionsInstance.Doctor != null)
                {
                    UserDataSectionsInstance.Doctor = new Doctors()
                    {
                        Id = UserDataSectionsInstance.User.UserId,
                        ProfilePhotoUri = UserDataSectionsInstance.User.UserProfilePhotoUri,
                        Name = UserDataSectionsInstance.User.UserName,
                        Surname = UserDataSectionsInstance.User.UserSurname,
                        Patronymic = UserDataSectionsInstance.User.UserPatronymic,
                        DateOfBirth = UserDataSectionsInstance.User.UserDateOfBirth,
                        Gender = UserDataSectionsInstance.User.UserGender,
                        AddressId = UserDataSectionsInstance.User.UserAddressId,
                        SchoolId = UserDataSectionsInstance.User.UserSchoolId,
                        UniversityId = UserDataSectionsInstance.User.UserUniversityId,
                        UniversityStartEducationYear = Convert.ToString(UserDataSectionsInstance.User.UserUniversityStartEducationYear),
                        UniversityEndEducationYear = Convert.ToString(UserDataSectionsInstance.User.UserUniversityEndEducationYear),
                        Login = UserDataSectionsInstance.User.UserLogin,
                        Password = UserDataSectionsInstance.User.UserPassword
                    };
                }

                if (UserDataSectionsRemember.RememberUserDataConfigExists())
                {
                    UserDataSectionsRemember.RememberUserDataConfigRemove();

                    UserDataSectionsRemember.RememberUserDataSeal(UserDataSectionsInstance.Patient, UserDataSectionsInstance.Doctor);
                }

                FrameManager.MainWindowFrame.Navigate(new UserMainInteractionHomePage(UserDataSectionsInstance.Patient, UserDataSectionsInstance.Doctor));
            }
        }
    }
}