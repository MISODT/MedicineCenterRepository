using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager
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

        public static bool ExternalUserDataDateOfShiftMistakesHandler(ComboBox userDataDayOfShiftComboBox, ComboBox userDataMonthOfShiftComboBox, ComboBox userDataYearOfShiftComboBox, TextBlock userDataDateOfShiftMistakeTextBlock)
        {
            DateOnly userDataDateOfShift = DateOnly.Parse($"{userDataDayOfShiftComboBox.SelectedValue}.{userDataMonthOfShiftComboBox.SelectedIndex + 1}.{userDataYearOfShiftComboBox.SelectedValue}");

            if (DateOnly.Parse(DateTime.Now.Date.ToShortDateString()) == userDataDateOfShift)
            {
                userDataDateOfShiftMistakeTextBlock.Visibility = Visibility.Visible;

                userDataDateOfShiftMistakeTextBlock.Text = "Смена не может начаться в текущий день";

                userDataDayOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataMonthOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataYearOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else if (userDataDateOfShift < DateOnly.Parse(DateTime.Now.Date.ToShortDateString()))
            {
                userDataDateOfShiftMistakeTextBlock.Visibility = Visibility.Visible;

                userDataDateOfShiftMistakeTextBlock.Text = "Смена не может быть в прошлом времени";

                userDataDayOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataMonthOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataYearOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                userDataDateOfShiftMistakeTextBlock.Visibility = Visibility.Hidden;

                userDataDateOfShiftMistakeTextBlock.Text = "";

                userDataDayOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataMonthOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataYearOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool ExternalUserDataTimeOfShiftMistakesHandler(ComboBox userDataHourOfShiftStartActionTimeComboBox, ComboBox userDataMinuteOfShiftStartActionTimeComboBox, ComboBox userDataHourOfShiftEndActionTimeComboBox, ComboBox userDataMinuteOfShiftEndActionTimeComboBox, TextBlock userDataTimeOfShiftMistakeTextBlock)
        {
            TimeOnly userDataShiftStartActionTime = TimeOnly.Parse($"{userDataHourOfShiftStartActionTimeComboBox.SelectedValue}:{userDataMinuteOfShiftStartActionTimeComboBox.SelectedValue}");

            TimeOnly userDataShiftEndActionTime = TimeOnly.Parse($"{userDataHourOfShiftEndActionTimeComboBox.SelectedValue}:{userDataMinuteOfShiftEndActionTimeComboBox.SelectedValue}");

            if (userDataShiftEndActionTime < userDataShiftStartActionTime)
            {
                userDataTimeOfShiftMistakeTextBlock.Visibility = Visibility.Visible;

                userDataTimeOfShiftMistakeTextBlock.Text = "Время конца смены не может быть меньше времени начала смены";

                userDataHourOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataMinuteOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataHourOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                userDataMinuteOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                userDataTimeOfShiftMistakeTextBlock.Visibility = Visibility.Hidden;

                userDataTimeOfShiftMistakeTextBlock.Text = "";

                userDataHourOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataMinuteOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataHourOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                userDataMinuteOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

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

        public static bool ExternalUserDataPasswordMistakesHandler(TextBox userDataPasswordTextBox, PasswordBox userDataPasswordPasswordBox, PasswordBox userDataOldPasswordPasswordBox, PasswordBox userDataNewPasswordPasswordBox, PasswordBox userDataRepeatPasswordPasswordBox, TextBlock userDataPasswordMistakeTextBlock)
        {
            if (userDataPasswordTextBox != null)
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

            else
            {
                if (userDataNewPasswordPasswordBox.Password.Length > 0 || userDataOldPasswordPasswordBox.Password.Length > 0)
                {
                    if (userDataNewPasswordPasswordBox.Password.Length < 8)
                    {
                        userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                        userDataPasswordMistakeTextBlock.Text = "Пароль должен быть не короче 8 символов";

                        userDataNewPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (userDataOldPasswordPasswordBox.Password != UserDataCryptionManager.UserDataDecrypt(UserDataSectionsInstance.User.UserPassword))
                    {
                        userDataPasswordMistakeTextBlock.Visibility = Visibility.Visible;

                        userDataPasswordMistakeTextBlock.Text = "Неправильный пароль";

                        userDataOldPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else
                    {
                        userDataPasswordMistakeTextBlock.Visibility = Visibility.Hidden;

                        userDataPasswordMistakeTextBlock.Text = "";

                        userDataOldPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                        userDataNewPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                        return true;
                    }
                }

                else
                {
                    return true;
                }
            }
        }
    }
}