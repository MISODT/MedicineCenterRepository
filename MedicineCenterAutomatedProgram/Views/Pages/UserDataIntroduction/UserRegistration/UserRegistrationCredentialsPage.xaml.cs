using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager;
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
            if (FieldsViewManager.IsPasswordVisible)
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

            FieldsViewManager.ChangeTextBoxClearedView(UserDataLoginTextBox, UserDataLoginTextBoxHintAssist, ClearLoginButton);
            FieldsViewManager.ChangePasswordView(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);
            FieldsViewManager.ChangePasswordView(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);
            FieldsViewManager.ChangePasswordView(UserDataPasswordTextBox, UserDataRepeatPasswordPasswordBoxHintAssist, UserDataRepeatPasswordPasswordBox, ClearRepeatPasswordButton, ChangeUserDataPasswordVisibilityButton);

            UserDataLoginMistakeTextBlock.Visibility = Visibility.Hidden;
            UserDataPasswordMistakeTextBlock.Visibility = Visibility.Hidden;

            UserDataLoginMailDomainComboBox.ItemsSource = OuteriorControlsInitializationManager.LoginMailDomainComboBoxInitialization();

            if (UserDataLoginMailDomainComboBox.SelectedItem == null)
            {
                UserDataLoginMailDomainComboBox.SelectedIndex = 0;
            }

            NavigationNextButtonState();
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new UserRegistrationEducationPage(SectionsInstance.SectionsBinding));

        private void UserDataLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FieldsViewManager.ChangeTextBoxClearedView(UserDataLoginTextBox, UserDataLoginTextBoxHintAssist, ClearLoginButton);

            ExternalMistakesManager.CheckTextBoxMistakes(UserDataLoginTextBox, UserDataLoginMistakeTextBlock, "Укажите адрес эл. почты");

            NavigationNextButtonState();
        }

        private void ClearLoginButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataLoginTextBox.Text = "";
        }

        private void UserDataPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.ChangePasswordView(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            ExternalMistakesManager.CheckPasswordMistakes(UserDataPasswordTextBox, UserDataPasswordPasswordBox, null, null, UserDataRepeatPasswordPasswordBox, UserDataPasswordMistakeTextBlock) ;

            InteriorControlsInitializationManager.InitializePasswordComplexityProgressBar(UserDataPasswordTextBox, UserDataPasswordPasswordBox, PasswordComplexityProgressBar);

            NavigationNextButtonState();
        }

        private void UserDataPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FieldsViewManager.ChangePasswordView(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            ExternalMistakesManager.CheckPasswordMistakes(UserDataPasswordTextBox, UserDataPasswordPasswordBox, null, null,  UserDataRepeatPasswordPasswordBox, UserDataPasswordMistakeTextBlock);

            InteriorControlsInitializationManager.InitializePasswordComplexityProgressBar(UserDataPasswordTextBox, UserDataPasswordPasswordBox, PasswordComplexityProgressBar);

            NavigationNextButtonState();
        }

        private void ChangeUserDataPasswordVisibilityButton_Click(object sender, RoutedEventArgs e) => FieldsViewManager.ChangePasswordVisibility(UserDataPasswordTextBox, UserDataPasswordPasswordBox, ChangeUserDataPasswordVisibilityButton);

        private void ClearPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataPasswordTextBox.Text = "";
            UserDataPasswordPasswordBox.Password = "";
        }

        private void UserDataRepeatPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            FieldsViewManager.ChangePasswordView(UserDataPasswordTextBox, UserDataRepeatPasswordPasswordBoxHintAssist, UserDataRepeatPasswordPasswordBox, ClearRepeatPasswordButton, ChangeUserDataPasswordVisibilityButton);

            ExternalMistakesManager.CheckPasswordMistakes(UserDataPasswordTextBox, UserDataPasswordPasswordBox, null, null, UserDataRepeatPasswordPasswordBox, UserDataPasswordMistakeTextBlock);

            NavigationNextButtonState();
        }

        private void ClearRepeatPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataRepeatPasswordPasswordBox.Password = "";
        }

        private void NavigateConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (InternalMistakesManager.CheckLoginMistakes(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), "Логин уже зарегистрирован"))
            {
                if (FieldsViewManager.IsPasswordVisible)
                {
                    SectionsOperationsManager.RegisterUserOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordTextBox.Text);

                    FrameManager.MainWindowFrame.Navigate(new WelcomePage());
                }

                else
                {
                    SectionsOperationsManager.RegisterUserOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordPasswordBox.Password);

                    FrameManager.MainWindowFrame.Navigate(new WelcomePage());
                }
            }
        }
    }
}