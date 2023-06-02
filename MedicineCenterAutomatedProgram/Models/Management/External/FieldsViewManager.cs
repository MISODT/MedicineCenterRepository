using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Models.Management.External
{
    public class FieldsViewManager
    {
        public static bool IsPasswordVisible { get; set; } = false;

        public static bool IsSortingMinButtonClicked { get; set; } = false;

        public static bool IsSortingMaxButtonClicked { get; set; } = false;


        public static void ChangeSortingMinButtonFieldView(Button sortingMinButton, Button sortingMaxButton)
        {
            if (!IsSortingMinButtonClicked)
            {
                sortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                IsSortingMinButtonClicked = true;

                IsSortingMaxButtonClicked = false;

                sortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }

            else
            {
                sortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                IsSortingMinButtonClicked = false;

                IsSortingMaxButtonClicked = true;

                sortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }
        }

        public static void ChangeSortingMaxButtonFieldView(Button sortingMaxButton, Button sortingMinButton)
        {
            if (!IsSortingMaxButtonClicked)
            {
                sortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                IsSortingMaxButtonClicked = true;

                IsSortingMinButtonClicked = false;

                sortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }

            else
            {
                sortingMaxButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                IsSortingMaxButtonClicked = false;

                IsSortingMinButtonClicked = true;

                sortingMinButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }
        }

        public static void ChangeTextBoxClearedView(TextBox clearedTextBox, TextBlock clearedTextBoxHintAssist, Button clearedTextBoxButton)
        {
            if (clearedTextBox.Text != "")
            {
                clearedTextBoxHintAssist.Visibility = Visibility.Hidden;

                clearedTextBoxButton.Visibility = Visibility.Visible;
            }

            else
            {
                clearedTextBoxHintAssist.Visibility = Visibility.Visible;

                clearedTextBoxButton.Visibility = Visibility.Hidden;
            }
        }

        public static void ChangeTextBoxUnclearedView(TextBox unclearedTextBox, TextBlock unclearedTextBoxHintAssist)
        {
            if (unclearedTextBox.Text != "")
            {
                unclearedTextBoxHintAssist.Visibility = Visibility.Hidden;
            }

            else
            {
                unclearedTextBoxHintAssist.Visibility = Visibility.Visible;
            }
        }

        public static void ChangeComboBoxView(ComboBox comboBox, TextBlock comboBoxHintAssistTextBlock)
        {
            if (comboBox.SelectedItem != null)
            {
                comboBoxHintAssistTextBlock.Visibility = Visibility.Hidden;
            }

            else
            {
                comboBoxHintAssistTextBlock.Visibility = Visibility.Visible;
            }
        }

        public static void ChangePasswordView(TextBox passwordTextBox, TextBlock passwordHintAssistTextBlock, PasswordBox passwordPasswordBox, Button clearPasswordButton, Button changePasswordVisabilityButton)
        {
            if (IsPasswordVisible)
            {
                if (passwordTextBox.Text != "")
                {
                    passwordHintAssistTextBlock.Visibility = Visibility.Hidden;

                    clearPasswordButton.Visibility = Visibility.Visible;
                }

                else
                {
                    passwordHintAssistTextBlock.Visibility = Visibility.Visible;

                    clearPasswordButton.Visibility = Visibility.Hidden;
                }

                changePasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }

            else
            {
                if (passwordPasswordBox.Password != "")
                {
                    passwordHintAssistTextBlock.Visibility = Visibility.Hidden;

                    clearPasswordButton.Visibility = Visibility.Visible;
                }

                else
                {
                    passwordHintAssistTextBlock.Visibility = Visibility.Visible;

                    clearPasswordButton.Visibility = Visibility.Hidden;
                }

                changePasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }
        }

        public static void ChangePasswordVisibility(TextBox passwordTextBox, PasswordBox passwordPasswordBox, Button changePasswordVisabilityButton)
        {
            if (!IsPasswordVisible)
            {
                passwordTextBox.Text = passwordPasswordBox.Password;

                passwordTextBox.Visibility = Visibility.Visible;

                passwordPasswordBox.Visibility = Visibility.Hidden;

                changePasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                IsPasswordVisible = true;
            }

            else
            {
                passwordPasswordBox.Password = passwordTextBox.Text;
                passwordTextBox.Visibility = Visibility.Hidden;
                passwordPasswordBox.Visibility = Visibility.Visible;

                changePasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                IsPasswordVisible = false;
            }
        }

        public static void ChangeHiddenPasswordView(TextBlock passwordHintAssistTextBlock, PasswordBox passwordPasswordBox, Button clearPasswordButton)
        {
            if (passwordPasswordBox.Password != "")
            {
                passwordHintAssistTextBlock.Visibility = Visibility.Hidden;

                clearPasswordButton.Visibility = Visibility.Visible;
            }

            else
            {
                passwordHintAssistTextBlock.Visibility = Visibility.Visible;

                clearPasswordButton.Visibility = Visibility.Hidden;
            }
        }

        public static string ChangeAddressFormatView(string addressValue, string addressValueStart, string addressValueEnd)
        {
            int startIndex, endIndex;

            if (addressValue.Contains(addressValueStart) && addressValue.Contains(addressValueEnd))
            {
                startIndex = addressValue.IndexOf(addressValueStart, 0) + addressValueStart.Length;

                endIndex = addressValue.IndexOf(addressValueEnd, startIndex);

                return addressValue.Substring(startIndex, endIndex - startIndex);
            }

            return "";
        }
    }
}