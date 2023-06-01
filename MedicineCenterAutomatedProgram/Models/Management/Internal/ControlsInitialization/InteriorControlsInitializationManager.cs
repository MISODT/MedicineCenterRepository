using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Windows.AlertWindows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization
{
    public class InteriorControlsInitializationManager
    {
        public static void VariablesSortingParametersComboBoxInitialization(ComboBox variableSortingParametersComboBox)
        {
            List<string> variableSortingParametersList = new List<string>()
            {
                "Все значения",
                "По дате"
            };

            variableSortingParametersComboBox.ItemsSource = variableSortingParametersList;
        }

        public static string ProfilePhotoImageInitialization(Image profilePhotoImage)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Uri selectedImageUri = new Uri($"{openFileDialog.FileName}", UriKind.Absolute);

                profilePhotoImage.Source = new BitmapImage(selectedImageUri);

                return openFileDialog.FileName.Replace('\\', '/');
            }

            else
            {
                Uri defaultImageUri = new Uri("/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png", UriKind.Relative);

                profilePhotoImage.Source = new BitmapImage(defaultImageUri);

                return "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
            }
        }

        public static void HourComboBoxInitialization(ComboBox hourComboBox)
        {
            List<int> hoursList = new List<int>();

            for (int i = 0; i <= 23; i++)
            {
                hoursList.Add(i);
            }

            hourComboBox.ItemsSource = hoursList;
        }

        public static void MinuteComboBoxInitialization(ComboBox minuteComboBox)
        {
            List<string> minutesList = new List<string>();

            for (int i = 0; i <= 59; i++)
            {
                minutesList.Add(i.ToString("D2"));
            }

            minuteComboBox.ItemsSource = minutesList;
        }

        public static void DayComboBoxInitialization(ComboBox dayComboBox, int monthNumber)
        {
            List<int> daysList = new List<int>();

            if (monthNumber == 2)
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

            dayComboBox.ItemsSource = daysList;
        }

        public static void MonthComboBoxInitialization(ComboBox monthComboBox)
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

            monthComboBox.ItemsSource = monthsList;
        }

        public static void MonthNumberComboBoxInitialization(ComboBox monthComboBox)
        {
            UserDataSectionsInstance.User.UserMonthOfBirthNumber = monthComboBox.SelectedIndex + 1;
        }

        public static void YearComboBoxInitialization(ComboBox yearComboBox, int nowYear)
        {
            List<int> yearsList = new List<int>();

            int minYear = 1900;
            int maxYear = nowYear;

            for (int i = maxYear; i >= minYear; i--)
            {
                yearsList.Add(i);
            }

            yearComboBox.ItemsSource = yearsList;
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

                if (userCredentialDataPasswordTextBox.Text.Length > 16)
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 2;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0");
                }

                if (userCredentialDataPasswordTextBox.Text.Length > 24)
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

                if (userCredentialDataPasswordPasswordBox.Password.Length > 16)
                {
                    userCredentialDataPasswordComplexityProgressBar.Value = 2;
                    userCredentialDataPasswordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0");
                }

                if (userCredentialDataPasswordPasswordBox.Password.Length > 24)
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

        public static void AlertMessageBorderItemsInitialization(string errorAlertWindowText)
        {
            ErrorAlertWindow errorAlertWindow = new ErrorAlertWindow(errorAlertWindowText);

            Application.Current.MainWindow.Opacity = 0.6;

            errorAlertWindow.ShowDialog();
        }
    }
}