using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager
{
    public class ExternalMistakesManager
    {
        public static bool CheckTextBoxMistakes(TextBox textBox, TextBlock mistakeTextBlock, string mistakeText)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                mistakeTextBlock.Visibility = Visibility.Visible;

                mistakeTextBlock.Text = mistakeText;

                return false;
            }

            else
            {
                textBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                mistakeTextBlock.Visibility = Visibility.Hidden;

                mistakeTextBlock.Text = "";

                return true;
            }
        }

        public static bool CheckComboBoxMistakes(ComboBox comboBox, TextBlock mistakeTextBlock, string mistakeText)
        {
            if (comboBox.SelectedItem == null)
            {
                mistakeTextBlock.Visibility = Visibility.Visible;

                mistakeTextBlock.Text = mistakeText;

                comboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                mistakeTextBlock.Visibility = Visibility.Hidden;

                mistakeTextBlock.Text = "";

                comboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool CheckDateOfBirthMistakes(ComboBox dayOfBirthComboBox, ComboBox monthOfBirthComboBox, ComboBox yearOfBirthComboBox, TextBlock dateOfBirthMistakeTextBlock)
        {
            int yearDifference = DateTime.Now.Year - Convert.ToInt32(yearOfBirthComboBox.SelectedValue);

            if (yearDifference < 18)
            {
                dateOfBirthMistakeTextBlock.Visibility = Visibility.Visible;

                dateOfBirthMistakeTextBlock.Text = "Пользователю должно быть 18 лет и более";

                dayOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                monthOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                yearOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                dateOfBirthMistakeTextBlock.Visibility = Visibility.Hidden;

                dateOfBirthMistakeTextBlock.Text = "";

                dayOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                monthOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                yearOfBirthComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool CheckDateOfShiftMistakes(ComboBox dayOfShiftComboBox, ComboBox monthOfShiftComboBox, ComboBox yearOfShiftComboBox, TextBlock dateOfShiftMistakeTextBlock)
        {
            DateOnly userDataDateOfShift = DateOnly.Parse($"{dayOfShiftComboBox.SelectedValue}.{monthOfShiftComboBox.SelectedIndex + 1}.{yearOfShiftComboBox.SelectedValue}");

            if (DateOnly.Parse(DateTime.Now.Date.ToShortDateString()) == userDataDateOfShift)
            {
                dateOfShiftMistakeTextBlock.Visibility = Visibility.Visible;

                dateOfShiftMistakeTextBlock.Text = "Смена не может начаться в текущий день";

                dayOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                monthOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                yearOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else if (userDataDateOfShift < DateOnly.Parse(DateTime.Now.Date.ToShortDateString()))
            {
                dateOfShiftMistakeTextBlock.Visibility = Visibility.Visible;

                dateOfShiftMistakeTextBlock.Text = "Смена не может быть в прошлом времени";

                dayOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                monthOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                yearOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                dateOfShiftMistakeTextBlock.Visibility = Visibility.Hidden;

                dateOfShiftMistakeTextBlock.Text = "";

                dayOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                monthOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                yearOfShiftComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool CheckTimeOfShiftMistakes(ComboBox hourOfShiftStartActionTimeComboBox, ComboBox minuteOfShiftStartActionTimeComboBox, ComboBox hourOfShiftEndActionTimeComboBox, ComboBox minuteOfShiftEndActionTimeComboBox, TextBlock timeOfShiftMistakeTextBlock)
        {
            TimeOnly userDataShiftStartActionTime = TimeOnly.Parse($"{hourOfShiftStartActionTimeComboBox.SelectedValue}:{minuteOfShiftStartActionTimeComboBox.SelectedValue}");

            TimeOnly userDataShiftEndActionTime = TimeOnly.Parse($"{hourOfShiftEndActionTimeComboBox.SelectedValue}:{minuteOfShiftEndActionTimeComboBox.SelectedValue}");

            if (userDataShiftEndActionTime < userDataShiftStartActionTime)
            {
                timeOfShiftMistakeTextBlock.Visibility = Visibility.Visible;

                timeOfShiftMistakeTextBlock.Text = "Время конца смены не может быть меньше времени начала смены";

                hourOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                minuteOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                hourOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");
                minuteOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                return false;
            }

            else
            {
                timeOfShiftMistakeTextBlock.Visibility = Visibility.Hidden;

                timeOfShiftMistakeTextBlock.Text = "";

                hourOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                minuteOfShiftStartActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                hourOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
                minuteOfShiftEndActionTimeComboBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                return true;
            }
        }

        public static bool CheckProfilePhotoMistakes(TextBlock profilePhotoUriTextBlock, TextBlock profilePhotoUriMistakeTextBlock, string profilePhotoUriMistakeText)
        {
            if (!File.Exists(profilePhotoUriTextBlock.Text))
            {
                profilePhotoUriMistakeTextBlock.Visibility = Visibility.Visible;

                profilePhotoUriMistakeTextBlock.Text = profilePhotoUriMistakeText;

                return false;
            }

            else
            {
                profilePhotoUriMistakeTextBlock.Visibility = Visibility.Hidden;

                profilePhotoUriMistakeTextBlock.Text = "";

                return true;
            }
        }

        public static bool CheckPasswordMistakes(TextBox passwordTextBox, PasswordBox passwordPasswordBox, PasswordBox oldPasswordPasswordBox, PasswordBox newPasswordPasswordBox, PasswordBox repeatPasswordPasswordBox, TextBlock passwordMistakeTextBlock)
        {
            if (passwordTextBox != null)
            {
                if (FieldsViewManager.IsPasswordVisible)
                {
                    if (passwordTextBox.Text != repeatPasswordPasswordBox.Password && passwordTextBox.Text.Length < 8)
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароли не совпадают \n Пароль должен быть не короче 8 символов";

                        passwordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (passwordTextBox.Text.Length < 8)
                    {
                        passwordMistakeTextBlock.Text = "Пароль должен быть не короче 8 символов";

                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (passwordTextBox.Text != repeatPasswordPasswordBox.Password)
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароли не совпадают";

                        passwordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if(passwordTextBox.Text.Contains("123") || passwordTextBox.Text.Contains("qwerty"))
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароль должен быть уникальным";

                        passwordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Hidden;

                        passwordMistakeTextBlock.Text = "";

                        passwordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                        return true;
                    }
                }

                else
                {
                    if (passwordPasswordBox.Password != repeatPasswordPasswordBox.Password && passwordPasswordBox.Password.Length < 8)
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароли не совпадают \n Пароль должен быть не короче 8 символов";

                        passwordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (passwordPasswordBox.Password.Length < 8)
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароль должен быть не короче 8 символов";

                        passwordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (passwordPasswordBox.Password != repeatPasswordPasswordBox.Password)
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароли не совпадают";

                        passwordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (passwordPasswordBox.Password.Contains("123") || passwordPasswordBox.Password.Contains("qwerty"))
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароль должен быть уникальным";

                        passwordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Hidden;

                        passwordMistakeTextBlock.Text = "";

                        passwordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                        return true;
                    }
                }
            }

            else
            {
                if (newPasswordPasswordBox.Password.Length > 0 || oldPasswordPasswordBox.Password.Length > 0)
                {
                    if (newPasswordPasswordBox.Password.Length < 8)
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароль должен быть не короче 8 символов";

                        newPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (newPasswordPasswordBox.Password.Contains("123") || newPasswordPasswordBox.Password.Contains("qwerty"))
                    {
                        MessageBox.Show(newPasswordPasswordBox.Password.Distinct().Count().ToString());

                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Пароль должен быть уникальным";

                        newPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else if (oldPasswordPasswordBox.Password != CryptionManager.DecryptData(SectionsInstance.SectionsBinding.UserPassword))
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Visible;

                        passwordMistakeTextBlock.Text = "Неправильный пароль";

                        oldPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                        return false;
                    }

                    else
                    {
                        passwordMistakeTextBlock.Visibility = Visibility.Hidden;

                        passwordMistakeTextBlock.Text = "";

                        oldPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                        newPasswordPasswordBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

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