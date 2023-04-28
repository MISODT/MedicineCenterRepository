﻿using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction.UserRegistration
{
    public partial class UserRegistrationCredentialsPage : Page
    {
        public UserRegistrationCredentialsPage()
        {
            InitializeComponent();
        }

        private void NavigationNextButtonState()
        {
            if (UserDataFieldsViewManager.IsUserDataPasswordVisible)
            {
                if (!string.IsNullOrWhiteSpace(UserDataLoginTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataPasswordTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataRepeatPasswordPasswordBox.Password))
                {
                    NavigateConfirmButton.IsEnabled = true;
                }

                else
                {
                    NavigateConfirmButton.IsEnabled = false;
                }
            }

            else
            {
                if (!string.IsNullOrWhiteSpace(UserDataLoginTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataPasswordPasswordBox.Password) && !string.IsNullOrWhiteSpace(UserDataRepeatPasswordPasswordBox.Password))
                {
                    NavigateConfirmButton.IsEnabled = true;
                }

                else
                {
                    NavigateConfirmButton.IsEnabled = false;
                }
            }
        }

        private void UserRegistrationCredentialsPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataPasswordTextBox.Visibility = Visibility.Hidden;

            ClearLoginButton.Visibility = Visibility.Hidden;
            ClearPasswordButton.Visibility = Visibility.Hidden;
            ClearRepeatPasswordButton.Visibility = Visibility.Hidden;

            UserDataLoginMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataPasswordMistakeTextBlock.Visibility = Visibility.Hidden;

            AlertBorderMessage.Visibility = Visibility.Hidden;

            UserDataLoginMailDomainComboBox.ItemsSource = OuteriorControlsInitializationManager.LoginMailDomainComboBoxInitialization();

            InteriorControlsInitializationManager.MailDomainComboBoxPrimaryInitialization(UserDataLoginMailDomainComboBox);

            NavigationNextButtonState();
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainFrame.Navigate(new UserRegistrationEducationPage(UserDataSectionsInstance.User));

        private void UserDataLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldVisibilityOptions(UserDataLoginTextBox, UserDataLoginTextBoxHintAssist, ClearLoginButton);

            UserDataExternalMistakesManager.ExternalUserDataTextBoxFieldMistakesHandler(UserDataLoginTextBox, UserDataLoginMistakeTextBlock, "Укажите адрес эл. почты");

            NavigationNextButtonState();
        }

        private void ClearLoginButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataLoginTextBox.Text = "";
        }

        private void UserDataPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            UserDataExternalMistakesManager.ExternalUserDataPasswordMistakesHandler(UserDataPasswordTextBox, UserDataPasswordPasswordBox, UserDataRepeatPasswordPasswordBox, UserDataPasswordMistakeTextBlock);

            InteriorControlsInitializationManager.PasswordComplexityProgressBarInitialization(UserDataPasswordTextBox, UserDataPasswordPasswordBox, PasswordComplexityProgressBar);

            NavigationNextButtonState();
        }

        private void UserDataPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            UserDataExternalMistakesManager.ExternalUserDataPasswordMistakesHandler(UserDataPasswordTextBox, UserDataPasswordPasswordBox,  UserDataRepeatPasswordPasswordBox, UserDataPasswordMistakeTextBlock);

            InteriorControlsInitializationManager.PasswordComplexityProgressBarInitialization(UserDataPasswordTextBox, UserDataPasswordPasswordBox, PasswordComplexityProgressBar);

            NavigationNextButtonState();
        }

        private void ChangeUserDataPasswordVisibilityButton_Click(object sender, RoutedEventArgs e) => UserDataFieldsViewManager.ChangeUserDataPasswordVisibility(UserDataPasswordTextBox, UserDataPasswordPasswordBox, ChangeUserDataPasswordVisibilityButton);

        private void ClearPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataPasswordTextBox.Text = "";
            UserDataPasswordPasswordBox.Password = "";
        }

        private void ChangeUserDataRepeatPasswordVisibilityButton_Click(object sender, RoutedEventArgs e) => UserDataFieldsViewManager.ChangeUserDataPasswordVisibility(null, UserDataRepeatPasswordPasswordBox, ChangeUserDataPasswordVisibilityButton);

        private void UserDataRepeatPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataRepeatPasswordPasswordBoxHintAssist, UserDataRepeatPasswordPasswordBox, ClearRepeatPasswordButton, ChangeUserDataPasswordVisibilityButton);

            UserDataExternalMistakesManager.ExternalUserDataPasswordMistakesHandler(UserDataPasswordTextBox, UserDataPasswordPasswordBox, UserDataRepeatPasswordPasswordBox, UserDataPasswordMistakeTextBlock);

            NavigationNextButtonState();
        }

        private void ClearRepeatPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataRepeatPasswordPasswordBox.Password = "";
        }

        private void NavigateConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataInternalMistakesManager.InternalUserDataLoginMistakesHandler(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), AlertBorderMessage, AlertBorderMessageType, AlertBorderMessageText))
            {
                if (UserDataFieldsViewManager.IsUserDataPasswordVisible)
                {
                    CredentialsUserDataOperationsManager.UserDataRegistrationOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordTextBox.Text);

                    FrameManager.MainFrame.Navigate(new WelcomePage());
                }

                else
                {
                    CredentialsUserDataOperationsManager.UserDataRegistrationOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordPasswordBox.Password);

                    FrameManager.MainFrame.Navigate(new WelcomePage());
                }
            }
        }
    }
}