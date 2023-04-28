using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization
{
    public class InteriorControlsInitializationManager
    {
        public static void DayComboBoxInitialization(ComboBox dayOfBirthComboBox, int monthOfBirthNumber)
        {
            List<int> daysList = new List<int>();

            if (monthOfBirthNumber == 2)
            {
                for (int i = 1; i <= 29; i++)
                {
                    daysList.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= 31; i++)
                {
                    daysList.Add(i);
                }
            }

            dayOfBirthComboBox.ItemsSource = daysList;
        }

        public static void MonthComboBoxInitialization(ComboBox monthOfBirthComboBox)
        {
            List<string> monthsList = new List<string>()
            {
                "Январь",
                "Февраль",
                "Март",
                "Апрель",
                "Май",
                "Июнь",
                "Июль",
                "Август"
            };

            monthOfBirthComboBox.ItemsSource = monthsList;
        }

        public static void MonthNumberComboBoxInitialization(ComboBox monthOfBirthComboBox)
        {
            UserDataSectionsInstance.User.UserMonthOfBirthNumber = monthOfBirthComboBox.SelectedIndex + 1;
        }

        public static void YearComboBoxInitialization(ComboBox yearOfBirthComboBox, int curYear)
        {
            List<int> yearsList = new List<int>();

            int minYear = 1900;
            int maxYear = curYear;

            for (int i = maxYear; i >= minYear; i--)
            {
                yearsList.Add(i);
            }

            yearOfBirthComboBox.ItemsSource = yearsList;
        }

        public static void MailDomainComboBoxPrimaryInitialization(ComboBox mailDomainComboBox)
        {
            if (mailDomainComboBox.SelectedItem == null)
            {
                mailDomainComboBox.SelectedIndex = 0;
            }
        }

        public static void PasswordComplexityProgressBarInitialization(TextBox userCredentialDataPasswordTextBox, PasswordBox userCredentialDataPasswordPasswordBox, ProgressBar userCredentialDataPasswordComplexityProgressBar)
        {
            if (UserDataFieldsViewManager.IsUserDataPasswordVisible)
            {
                if (userCredentialDataPasswordTextBox.Text.Length > 8)
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 1;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                }

                if (userCredentialDataPasswordTextBox.Text.Length > 8 && StringContainsManager.IsStringContainsDigits(userCredentialDataPasswordTextBox.Text))
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 2;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0");
                }

                if (userCredentialDataPasswordTextBox.Text.Length > 8 && StringContainsManager.IsStringContainsDigits(userCredentialDataPasswordTextBox.Text) && StringContainsManager.IsStringContainsSymbols(userCredentialDataPasswordTextBox.Text))
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 3;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#0A5");
                }

                if (userCredentialDataPasswordTextBox.Text == "")
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 0;
                }
            }

            else
            {
                if (userCredentialDataPasswordPasswordBox.Password.Length > 8)
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 1;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                }

                if (userCredentialDataPasswordPasswordBox.Password.Length > 8 && StringContainsManager.IsStringContainsDigits(userCredentialDataPasswordPasswordBox.Password))
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 2;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0");
                }

                if (userCredentialDataPasswordPasswordBox.Password.Length > 8 && StringContainsManager.IsStringContainsDigits(userCredentialDataPasswordPasswordBox.Password) && StringContainsManager.IsStringContainsSymbols(userCredentialDataPasswordPasswordBox.Password))
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 3;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#0A5");
                }

                if (userCredentialDataPasswordPasswordBox.Password == "")
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 0;
                }
            }
        }

        public static void AlertMessageBorderItemsInitialization(Border alertMessageBorder, TextBlock alertMessageTypeTextBlock, string alertMessageType, TextBlock alertMessageTextTextBlock, string alertMessageText)
        {
            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                alertMessageBorder.Visibility = Visibility.Visible;

                alertMessageBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                alertMessageTypeTextBlock.Text = alertMessageType;

                alertMessageTextTextBlock.Text = alertMessageText;

                await Task.Delay(2000);

                alertMessageBorder.Visibility = Visibility.Hidden;
            });
        }
    }
}