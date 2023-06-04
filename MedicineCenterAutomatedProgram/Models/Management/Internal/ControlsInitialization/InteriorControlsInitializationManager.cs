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
        public static void InitializeComboBoxSortingParameters(ComboBox sortingParametersComboBox)
        {
            List<string> sortingParametersList = new List<string>()
            {
                "Все значения",
                "По дате"
            };

            sortingParametersComboBox.ItemsSource = sortingParametersList;
        }

        public static string InitializeProfilePhotoImage(Image profilePhotoImage)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

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

        public static void InitializeHourComboBox(ComboBox hourComboBox)
        {
            List<int> hoursList = new List<int>();

            for (int i = 0; i <= 23; i++)
            {
                hoursList.Add(i);
            }

            hourComboBox.ItemsSource = hoursList;
        }

        public static void InitializeMinuteComboBox(ComboBox minuteComboBox)
        {
            List<string> minutesList = new List<string>();

            for (int i = 0; i <= 59; i++)
            {
                minutesList.Add(i.ToString("D2"));
            }

            minuteComboBox.ItemsSource = minutesList;
        }

        public static void InitializeDayComboBox(ComboBox dayComboBox, int monthNumber)
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

        public static void InitializeMonthComboBox(ComboBox monthComboBox)
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
                "Август",
                "Сентябрь",
                "Октябрь",
                "Ноябрь",
                "Декабрь"
            };

            monthComboBox.ItemsSource = monthsList;
        }

        public static void InitializeYearComboBox(ComboBox yearComboBox, int nowYear)
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

        public static void InitializePasswordComplexityProgressBar(TextBox passwordTextBox, PasswordBox passwordPasswordBox, ProgressBar passwordComplexityProgressBar)
        {
            if (FieldsViewManager.IsPasswordVisible)
            {
                if (passwordTextBox.Text.Length > 8)
                {
                    passwordComplexityProgressBar.Value = 1;
                    passwordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                }

                if (passwordTextBox.Text.Length > 16)
                {
                    passwordComplexityProgressBar.Value = 2;
                    passwordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0");
                }

                if (passwordTextBox.Text.Length > 24)
                {
                    passwordComplexityProgressBar.Value = 3;
                    passwordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#0A5");
                }

                if (passwordTextBox.Text == "")
                {
                    passwordComplexityProgressBar.Value = 0;
                }
            }

            else
            {
                if (passwordPasswordBox.Password.Length > 8)
                {
                    passwordComplexityProgressBar.Value = 1;
                    passwordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                }

                if (passwordPasswordBox.Password.Length > 16)
                {
                    passwordComplexityProgressBar.Value = 2;
                    passwordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0");
                }

                if (passwordPasswordBox.Password.Length > 24)
                {
                    passwordComplexityProgressBar.Value = 3;
                    passwordComplexityProgressBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#0A5");
                }

                if (passwordPasswordBox.Password == "")
                {
                    passwordComplexityProgressBar.Value = 0;
                }
            }
        }

        public static void InitializeErrorAlertWindow(string errorAlertWindowText)
        {
            ErrorAlertWindow errorAlertWindow = new ErrorAlertWindow(errorAlertWindowText);

            Application.Current.MainWindow.Opacity = 0.6;

            errorAlertWindow.ShowDialog();
        }

        public static bool InitializeQuestionAlertWindow()
        {
            QuestionAlertWindow questionAlertWindow = new QuestionAlertWindow();

            Application.Current.MainWindow.Opacity = 0.6;

            questionAlertWindow.ShowDialog();

            if (questionAlertWindow.questionAlertWindowAnswerValue == "Да")
            {
                return true;
            }

            if (questionAlertWindow.questionAlertWindowAnswerValue == "Нет")
            {
                return false;
            }

            return false;
        }
    }
}