using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionProfilePage : Page
    {
        public UserMainInteractionProfilePage(SectionsBindingManager user)
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
            if (SectionsInstance.SectionsBinding.UserGender == "Мужской")
            {
                SelectMaleGenderRadioButton.IsChecked = true;
            }

            if (SectionsInstance.SectionsBinding.UserGender == "Женский")
            {
                SelectFemaleGenderRadioButton.IsChecked = true;
            }

            if (SectionsInstance.SectionsBinding.UserGender == "Не указан")
            {
                SelectUndefinedGenderRadioButton.IsChecked = true;
            }
        }

        private void UserMainInteractionProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.ChangeTextBoxClearedView(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);
            FieldsViewManager.ChangeTextBoxClearedView(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);
            FieldsViewManager.ChangeTextBoxClearedView(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            UserDataProfilePhotoUriMistakeTextBlock.Visibility = Visibility.Hidden;

            if(!ExternalMistakesManager.CheckProfilePhotoMistakes(UserDataProfilePhotoUriTextBlock, UserDataProfilePhotoUriMistakeTextBlock, "Файл не был найден"))
            {
                SectionsInstance.SectionsBinding.UserProfilePhotoUri = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
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

            InteriorControlsInitializationManager.InitializeYearComboBox(UserDataUniversityStartEducationYearComboBox, DateTime.Now.Year);
            InteriorControlsInitializationManager.InitializeYearComboBox(UserDataUniversityEndEducationYearComboBox, DateTime.Now.Year);

            UserDataPasswordMistakeTextBlock.Visibility = Visibility.Hidden;
        }

        private void SelectUserDataProfilePhotoImageButton_Click(object sender, RoutedEventArgs e)
        {
            SectionsInstance.SectionsBinding.UserProfilePhotoUri = InteriorControlsInitializationManager.InitializeProfilePhotoImage(UserDataProfilePhotoImage);
        }

        private void UserDataNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FieldsViewManager.ChangeTextBoxClearedView(UserDataNameTextBox, UserDataNameTextBoxHintAssist, ClearNameButton);

            ExternalMistakesManager.CheckTextBoxMistakes(UserDataNameTextBox, UserDataNameMistakeTextBlock, "Укажите имя");

            ProfileSaveButtonState();
        }

        private void ClearNameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataNameTextBox.Text = "";
        }

        private void UserDataSurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FieldsViewManager.ChangeTextBoxClearedView(UserDataSurnameTextBox, UserDataSurnameTextBoxHintAssist, ClearSurnameButton);

            ExternalMistakesManager.CheckTextBoxMistakes(UserDataSurnameTextBox, UserDataSurnameMistakeTextBlock, "Укажите фамилию");

            ProfileSaveButtonState();
        }

        private void ClearSurnameButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSurnameTextBox.Text = "";
        }

        private void UserDataPatronymicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FieldsViewManager.ChangeTextBoxClearedView(UserDataPatronymicTextBox, UserDataPatronymicTextBoxHintAssist, ClearPatronymicButton);

            ExternalMistakesManager.CheckTextBoxMistakes(UserDataPatronymicTextBox, UserDataPatronymicMistakeTextBlock, "Укажите отчество");

            ProfileSaveButtonState();
        }

        private void ClearPatronymicButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataPatronymicTextBox.Text = "";
        }

        private void SelectMaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SectionsInstance.SectionsBinding.UserGender = "Мужской";
        }

        private void SelectFemaleGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SectionsInstance.SectionsBinding.UserGender = "Женский";
        }

        private void SelectUndefinedGenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SectionsInstance.SectionsBinding.UserGender = "Не указан";
        }

        private void UserDataCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataCityComboBox, UserDataCityTextBoxHintAssist);

            UserDataStreetComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressStreetComboBoxInitialization();

            ProfileSaveButtonState();
        }

        private void UserDataStreetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserDataStreetComboBox, UserDataStreetTextBoxHintAssist);

            UserDataHouseComboBox.ItemsSource = OuteriorControlsInitializationManager.AddressHouseComboBoxInitialization();

            ProfileSaveButtonState();
        }

        private void UserDataHouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            FieldsViewManager.ChangeComboBoxView(UserDataHouseComboBox, UserDataHouseTextBoxHintAssist);

            ProfileSaveButtonState();
        }

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

        private void UserDataOldPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) 
        {
            FieldsViewManager.ChangeHiddenPasswordView(UserDataOldPasswordPasswordBoxHintAssist, UserDataOldPasswordPasswordBox, ClearOldPasswordButton);

            ExternalMistakesManager.CheckPasswordMistakes(null, null, UserDataOldPasswordPasswordBox, UserDataNewPasswordPasswordBox, null, UserDataPasswordMistakeTextBlock);
        }

        private void ClearOldPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataOldPasswordPasswordBox.Password = "";
        }

        private void UserDataNewPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.ChangeHiddenPasswordView(UserDataNewPasswordPasswordBoxHintAssist, UserDataNewPasswordPasswordBox, ClearNewPasswordButton);

            ExternalMistakesManager.CheckPasswordMistakes(null, null, UserDataOldPasswordPasswordBox, UserDataNewPasswordPasswordBox, null, UserDataPasswordMistakeTextBlock);
        }

        private void ClearNewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataNewPasswordPasswordBox.Password = "";
        }

        private void UserProfileDataSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(ExternalMistakesManager.CheckPasswordMistakes(null, null, UserDataOldPasswordPasswordBox, UserDataNewPasswordPasswordBox, null, UserDataPasswordMistakeTextBlock))
            {
                SectionsInstance.SectionsBinding.ExtractUserAddressData();

                SectionsInstance.SectionsBinding.ExtractUserEducationData();

                if(UserDataOldPasswordPasswordBox.Password != "" && UserDataNewPasswordPasswordBox.Password != "")
                {
                    if (SectionsInstance.SectionsBinding.UserPositionIsPatient)
                    {
                        WebResponseManager.ResponseFromRequestQuery($"UPDATE Patients SET Password = '{CryptionManager.EncryptData(UserDataNewPasswordPasswordBox.Password)}' WHERE Id = {SectionsInstance.SectionsBinding.UserId}");
                    }

                    if (SectionsInstance.SectionsBinding.UserPositionIsDoctor)
                    {
                        WebResponseManager.ResponseFromRequestQuery($"UPDATE Doctors SET Password = '{CryptionManager.EncryptData(UserDataNewPasswordPasswordBox.Password)}' WHERE Id = {SectionsInstance.SectionsBinding.UserId}");
                    }

                    SectionsInstance.SectionsBinding.UserPassword = CryptionManager.EncryptData(UserDataNewPasswordPasswordBox.Password);
                }

                SectionsOperationsManager.UpdateUserOperation();

                if (SectionsInstance.Patient != null)
                {
                    SectionsInstance.Patient = new Patients()
                    {
                        Id = SectionsInstance.SectionsBinding.UserId,
                        ProfilePhotoUri = SectionsInstance.SectionsBinding.UserProfilePhotoUri,
                        Name = SectionsInstance.SectionsBinding.UserName,
                        Surname = SectionsInstance.SectionsBinding.UserSurname,
                        Patronymic = SectionsInstance.SectionsBinding.UserPatronymic,
                        DateOfBirth = SectionsInstance.SectionsBinding.UserDateOfBirth,
                        Gender = SectionsInstance.SectionsBinding.UserGender,
                        AddressId = SectionsInstance.SectionsBinding.UserAddressId,
                        SchoolId = SectionsInstance.SectionsBinding.UserSchoolId,
                        UniversityId = SectionsInstance.SectionsBinding.UserUniversityId,
                        UniversityStartEducationYear = Convert.ToString(SectionsInstance.SectionsBinding.UserUniversityStartEducationYear),
                        UniversityEndEducationYear = Convert.ToString(SectionsInstance.SectionsBinding.UserUniversityEndEducationYear),
                        Login = SectionsInstance.SectionsBinding.UserLogin,
                        Password = SectionsInstance.SectionsBinding.UserPassword
                    };
                }

                if (SectionsInstance.Doctor != null)
                {
                    SectionsInstance.Doctor = new Doctors()
                    {
                        Id = SectionsInstance.SectionsBinding.UserId,
                        ProfilePhotoUri = SectionsInstance.SectionsBinding.UserProfilePhotoUri,
                        Name = SectionsInstance.SectionsBinding.UserName,
                        Surname = SectionsInstance.SectionsBinding.UserSurname,
                        Patronymic = SectionsInstance.SectionsBinding.UserPatronymic,
                        DateOfBirth = SectionsInstance.SectionsBinding.UserDateOfBirth,
                        Gender = SectionsInstance.SectionsBinding.UserGender,
                        AddressId = SectionsInstance.SectionsBinding.UserAddressId,
                        SchoolId = SectionsInstance.SectionsBinding.UserSchoolId,
                        UniversityId = SectionsInstance.SectionsBinding.UserUniversityId,
                        UniversityStartEducationYear = Convert.ToString(SectionsInstance.SectionsBinding.UserUniversityStartEducationYear),
                        UniversityEndEducationYear = Convert.ToString(SectionsInstance.SectionsBinding.UserUniversityEndEducationYear),
                        Login = SectionsInstance.SectionsBinding.UserLogin,
                        Password = SectionsInstance.SectionsBinding.UserPassword
                    };
                }

                if (SectionsRememberConfigManager.IsRememberConfigExists())
                {
                    SectionsRememberConfigManager.RemoveRememberConfig();

                    SectionsRememberConfigManager.SealToRememberConfig(SectionsInstance.Patient, SectionsInstance.Doctor);
                }

                FrameManager.MainWindowFrame.Navigate(new UserMainInteractionHomePage(SectionsInstance.Patient, SectionsInstance.Doctor));
            }
        }

        private void UserProfileDataDeleteProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (InteriorControlsInitializationManager.InitializeQuestionAlertWindow())
            {
                if (InternalMistakesManager.CheckUserReferencesMistakes())
                {
                    if (SectionsInstance.Patient != null)
                    {
                        SectionsOperationsManager.RemoveUserOperation(SectionsInstance.Patient.Id);
                    }

                    if (SectionsInstance.Doctor != null)
                    {
                        SectionsOperationsManager.RemoveUserOperation(SectionsInstance.Doctor.Id);
                    }


                    if (SectionsRememberConfigManager.IsRememberConfigExists())
                    {
                        SectionsRememberConfigManager.RemoveRememberConfig();
                    }


                    FrameManager.MainWindowFrame.Navigate(new WelcomePage());
                }

                else
                {
                    InteriorControlsInitializationManager.InitializeErrorAlertWindow("Завершите записи и/или смены");
                }
            }
        }
    }
}