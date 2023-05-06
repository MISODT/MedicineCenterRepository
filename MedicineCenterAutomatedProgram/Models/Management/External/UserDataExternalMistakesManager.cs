using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Models.Management.External
{
    public class UserDataExternalMistakesManager
    {
        public static bool ExternalUserDataTextBoxFieldMistakesHandler(TextBox userDataFieldTextBox, TextBlock userDataFieldMistakeTextBlock, string userDataFieldMistakeString)
        {
            if (string.IsNullOrWhiteSpace(userDataFieldTextBox.Text))
            {
                userDataFieldTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                userDataFieldMistakeTextBlock.Visibility = Visibility.Visible;

                userDataFieldMistakeTextBlock.Text = userDataFieldMistakeString;

                return false;
            }

            else
            {
                userDataFieldTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                userDataFieldMistakeTextBlock.Visibility = Visibility.Hidden;

                userDataFieldMistakeTextBlock.Text = "";

                return true;
            }
        }

        public static bool ExternalUserDataComboBoxFieldMistakesHandler(ComboBox userDataFieldComboBox, TextBlock userDataFieldMistakeTextBlock, string userDataFieldMistakeString)
        {
            if (userDataFieldComboBox.SelectedItem == null)
            {
                userDataFieldMistakeTextBlock.Visibility = Visibility.Visible;

                userDataFieldMistakeTextBlock.Text = userDataFieldMistakeString;

                userDataFieldComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                userDataFieldMistakeTextBlock.Visibility = Visibility.Hidden;

                userDataFieldMistakeTextBlock.Text = "";

                userDataFieldComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool ExternalUserDataDateOfBirthMistakesHandler(ComboBox userDataDayOfBirthComboBox, ComboBox userDataMonthOfBirthComboBox, ComboBox userDataYearOfBirthComboBox, TextBlock userDataDateOfBirthMistakeTextBlock)
        {
            int yearDifference = DateTime.Now.Year - Convert.ToInt32(userDataYearOfBirthComboBox.SelectedValue);

            if (yearDifference < 18)
            {
                userDataDateOfBirthMistakeTextBlock.Visibility = Visibility.Visible;

                userDataDateOfBirthMistakeTextBlock.Text = "Пользователю должно быть 18 лет и более";

                userDataDayOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataMonthOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataYearOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                userDataDateOfBirthMistakeTextBlock.Visibility = Visibility.Hidden;

                userDataDateOfBirthMistakeTextBlock.Text = "";

                userDataDayOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataMonthOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataYearOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool ExternalUserDataProfilePhotoUriMistakesHandler(TextBlock userDataProfilePhotoUriTextBlock, TextBlock userDataProfilePhotoUriMistakeTextBlock, string userDataProfilePhotoUriMistakeString)
        {
            if (!File.Exists(userDataProfilePhotoUriTextBlock.Text))
            {
                userDataProfilePhotoUriMistakeTextBlock.Visibility = Visibility.Visible;

                userDataProfilePhotoUriMistakeTextBlock.Text = userDataProfilePhotoUriMistakeString;

                return false;
            }

            else
            {
                userDataProfilePhotoUriMistakeTextBlock.Visibility = Visibility.Hidden;

                userDataProfilePhotoUriMistakeTextBlock.Text = "";

                return true;
            }
        }

        public static bool ExternalUserDataPasswordMistakesHandler(TextBox userDataPasswordTextBox, PasswordBox userDataPasswordPasswordBox, PasswordBox userDataRepeatPasswordPasswordBox, TextBlock userDataPasswordMistakeTextBlock)
        {
            if (UserDataFieldsViewManager.IsUserDataPasswordVisible)
            {
                if (userDataPasswordTextBox.Text != userDataRepeatPasswordPasswordBox.Password && userDataPasswordTextBox.Text.Length < 8)
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                    userDataPasswordMistakeTextBlock.Text = "Пароли не совпадают \n Пароль должен быть не короче 8 символов";

                    userDataPasswordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                    return false;
                }

                else if (userDataPasswordTextBox.Text.Length < 8)
                {
                    userDataPasswordMistakeTextBlock.Text = "Пароль должен быть не короче 8 символов";

                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                    userDataPasswordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                    return false;
                }

                else if (userDataPasswordTextBox.Text != userDataRepeatPasswordPasswordBox.Password)
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                    userDataPasswordMistakeTextBlock.Text = "Пароли не совпадают";

                    userDataPasswordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                    return false;
                }

                else
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Hidden;

                    userDataPasswordMistakeTextBlock.Text = "";

                    userDataPasswordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                    return true;
                }
            }

            else
            {
                if (userDataPasswordPasswordBox.Password != userDataRepeatPasswordPasswordBox.Password && userDataPasswordPasswordBox.Password.Length < 8)
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                    userDataPasswordMistakeTextBlock.Text = "Пароли не совпадают \n Пароль должен быть не короче 8 символов";

                    userDataPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                    return false;
                }

                else if (userDataPasswordPasswordBox.Password.Length < 8)
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                    userDataPasswordMistakeTextBlock.Text = "Пароль должен быть не короче 8 символов";

                    userDataPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                    return false;
                }

                else if (userDataPasswordPasswordBox.Password != userDataRepeatPasswordPasswordBox.Password)
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                    userDataPasswordMistakeTextBlock.Text = "Пароли не совпадают";

                    userDataPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                    return false;
                }

                else
                {
                    userDataPasswordMistakeTextBlock.Visibility = Visibility.Hidden;

                    userDataPasswordMistakeTextBlock.Text = "";

                    userDataPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                    return true;
                }
            }
        }
    }
}