using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserDataIntroduction
{
    public partial class UserAuthorizationPage : Page
    {

        public UserAuthorizationPage()
        {
            InitializeComponent();
        }

        private void NavigationNextButtonState()
        {
            if (UserDataFieldsViewManager.IsUserDataPasswordVisible)
            {
                if (!string.IsNullOrWhiteSpace(UserDataLoginTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataPasswordTextBox.Text))
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
                if (!string.IsNullOrWhiteSpace(UserDataLoginTextBox.Text) && !string.IsNullOrWhiteSpace(UserDataPasswordPasswordBox.Password))
                {
                    NavigateConfirmButton.IsEnabled = true;
                }

                else
                {
                    NavigateConfirmButton.IsEnabled = false;
                }
            }
        }

        private void UserAuthorizationPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataPasswordTextBox.Visibility = Visibility.Hidden;

            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataLoginTextBox, UserDataLoginTextBoxHintAssist, ClearLoginButton);
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            RememberMeCheckBox.IsChecked = false;

            UserDataLoginMailDomainComboBox.ItemsSource = OuteriorControlsInitializationManager.LoginMailDomainComboBoxInitialization();

            InteriorControlsInitializationManager.MailDomainComboBoxPrimaryInitialization(UserDataLoginMailDomainComboBox);

            NavigationNextButtonState();
        }

        private void NavigateBeforeButton_Click(object sender, RoutedEventArgs e) => FrameManager.MainWindowFrame.Navigate(new WelcomePage());

        private void UserDataLoginTextBox_TextChanged(object sender, TextChangedEventArgs e) 
        {
            UserDataFieldsViewManager.UserDataTextBoxFieldClearedVisibilityOptions(UserDataLoginTextBox, UserDataLoginTextBoxHintAssist, ClearLoginButton);

            NavigationNextButtonState();
        }

        private void ClearLoginButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataLoginTextBox.Text = "";
        }

        private void UserDataPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            NavigationNextButtonState();
        }

        private void UserDataPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataPasswordFieldVisibilityOptions(UserDataPasswordTextBox, UserDataPasswordTextBoxHintAssist, UserDataPasswordPasswordBox, ClearPasswordButton, ChangeUserDataPasswordVisibilityButton);

            NavigationNextButtonState();
        }

        private void ChangeUserDataPasswordVisibilityButton_Click(object sender, RoutedEventArgs e) => UserDataFieldsViewManager.ChangeUserDataPasswordVisibility(UserDataPasswordTextBox, UserDataPasswordPasswordBox, ChangeUserDataPasswordVisibilityButton);

        private void ClearPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataPasswordTextBox.Text = "";
            UserDataPasswordPasswordBox.Password = "";
        }

        private void NavigateConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataFieldsViewManager.IsUserDataPasswordVisible)
            {
                if (UserDataInternalMistakesManager.InternalUserDataMistakesHandler(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordTextBox.Text, "Пользователь не найден"))
                {
                    FrameManager.MainWindowFrame.Navigate(new UserMainInteractionHomePage(UserDataSectionsDataOperations.UserDataPatientAuthorizationOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordTextBox.Text), UserDataSectionsDataOperations.UserDataDoctorAuthorizationOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordTextBox.Text)));

                    if (RememberMeCheckBox.IsChecked == true)
                    {
                        UserDataSectionsRemember.RememberUserDataSeal(UserDataSectionsInstance.Patient, UserDataSectionsInstance.Doctor);
                    }
                }
            }

            else
            {
                if (UserDataInternalMistakesManager.InternalUserDataMistakesHandler(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordPasswordBox.Password, "Пользователь не найден"))
                {
                    FrameManager.MainWindowFrame.Navigate(new UserMainInteractionHomePage(UserDataSectionsDataOperations.UserDataPatientAuthorizationOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordPasswordBox.Password), UserDataSectionsDataOperations.UserDataDoctorAuthorizationOperation(UserDataLoginTextBox.Text, UserDataLoginMailDomainComboBox.SelectedValue.ToString(), UserDataPasswordPasswordBox.Password)));

                    if (RememberMeCheckBox.IsChecked == true)
                    {
                        UserDataSectionsRemember.RememberUserDataSeal(UserDataSectionsInstance.Patient, UserDataSectionsInstance.Doctor);
                    }
                }
            }
        }
    }
}