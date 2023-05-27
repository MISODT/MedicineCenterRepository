﻿using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace MedicineCenterAutomatedProgram.Models.Management.External
{
    public class UserDataFieldsViewManager
    {
        public static bool IsUserDataPasswordVisible { get; set; } = false;

        public static bool IsUserMainInteractionVariablesSortingMinButtonClicked { get; set; } = false;

        public static bool IsUserMainInteractionVariablesSortingMaxButtonClicked { get; set; } = false;

        public static void UserMainInteractionVariablesSortingMinButtonFieldVisibilityOptions(Button userMainInteractionVariablesSortingMinButtonField, Button userMainInteractionVariablesSortingMaxButtonField)
        {
            if (!IsUserMainInteractionVariablesSortingMinButtonClicked)
            {
                userMainInteractionVariablesSortingMinButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                IsUserMainInteractionVariablesSortingMinButtonClicked = true;

                IsUserMainInteractionVariablesSortingMaxButtonClicked = false;

                userMainInteractionVariablesSortingMaxButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }

            else
            {
                userMainInteractionVariablesSortingMinButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                IsUserMainInteractionVariablesSortingMinButtonClicked = false;

                IsUserMainInteractionVariablesSortingMaxButtonClicked = true;

                userMainInteractionVariablesSortingMaxButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }
        }

        public static void UserMainInteractionVariablesSortingMaxButtonFieldVisibilityOptions(Button userMainInteractionVariablesSortingMaxButtonField, Button userMainInteractionVariablesSortingMinButtonField)
        {
            if (!IsUserMainInteractionVariablesSortingMaxButtonClicked)
            {
                userMainInteractionVariablesSortingMaxButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                IsUserMainInteractionVariablesSortingMaxButtonClicked = true;

                IsUserMainInteractionVariablesSortingMinButtonClicked = false;

                userMainInteractionVariablesSortingMinButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }

            else
            {
                userMainInteractionVariablesSortingMaxButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                IsUserMainInteractionVariablesSortingMaxButtonClicked = false;

                IsUserMainInteractionVariablesSortingMinButtonClicked = true;

                userMainInteractionVariablesSortingMinButtonField.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }
        }

        public static void UserDataTextBoxFieldClearedVisibilityOptions(TextBox userDataFieldTextBox, TextBlock userDataFieldTextBoxHintAssist, Button clearDataFieldButton)
        {
            if (userDataFieldTextBox.Text != "")
            {
                userDataFieldTextBoxHintAssist.Visibility = Visibility.Hidden;

                clearDataFieldButton.Visibility = Visibility.Visible;
            }

            else
            {
                userDataFieldTextBoxHintAssist.Visibility = Visibility.Visible;

                clearDataFieldButton.Visibility = Visibility.Hidden;
            }
        }

        public static void UserDataTextBoxFieldUnclearedVisibilityOptions(TextBox userDataFieldTextBox, TextBlock userDataFieldTextBoxHintAssist)
        {
            if (userDataFieldTextBox.Text != "")
            {
                userDataFieldTextBoxHintAssist.Visibility = Visibility.Hidden;
            }

            else
            {
                userDataFieldTextBoxHintAssist.Visibility = Visibility.Visible;
            }
        }

        public static void UserDataComboBoxFieldVisibilityOptions(ComboBox userDataFieldComboBox, TextBlock userDataFieldComboBoxHintAssistTextBlock)
        {
            if (userDataFieldComboBox.SelectedItem != null)
            {
                userDataFieldComboBoxHintAssistTextBlock.Visibility = Visibility.Hidden;
            }

            else
            {
                userDataFieldComboBoxHintAssistTextBlock.Visibility = Visibility.Visible;
            }
        }

        public static void UserDataPasswordFieldVisibilityOptions(TextBox userDataPasswordTextBox, TextBlock userDataPasswordHintAssist, PasswordBox userDataPasswordPasswordBox, Button clearDataPasswordButton, Button changeDataPasswordVisabilityButton)
        {
            if (IsUserDataPasswordVisible)
            {
                if (userDataPasswordTextBox.Text != "")
                {
                    userDataPasswordHintAssist.Visibility = Visibility.Hidden;

                    clearDataPasswordButton.Visibility = Visibility.Visible;
                }

                else
                {
                    userDataPasswordHintAssist.Visibility = Visibility.Visible;

                    clearDataPasswordButton.Visibility = Visibility.Hidden;
                }

                changeDataPasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");
            }

            else
            {
                if (userDataPasswordPasswordBox.Password != "")
                {
                    userDataPasswordHintAssist.Visibility = Visibility.Hidden;

                    clearDataPasswordButton.Visibility = Visibility.Visible;
                }

                else
                {
                    userDataPasswordHintAssist.Visibility = Visibility.Visible;

                    clearDataPasswordButton.Visibility = Visibility.Hidden;
                }

                changeDataPasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");
            }
        }

        public static void ChangeUserDataPasswordVisibility(TextBox userCredentialsDataPasswordTextBox, PasswordBox userCredentialsDataPasswordPasswordBox, Button changeCredentialsDataPasswordVisabilityButton)
        {
            if (!IsUserDataPasswordVisible)
            {
                userCredentialsDataPasswordTextBox.Text = userCredentialsDataPasswordPasswordBox.Password;

                userCredentialsDataPasswordTextBox.Visibility = Visibility.Visible;

                userCredentialsDataPasswordPasswordBox.Visibility = Visibility.Hidden;

                changeCredentialsDataPasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5AF");

                IsUserDataPasswordVisible = true;
            }

            else
            {
                userCredentialsDataPasswordPasswordBox.Password = userCredentialsDataPasswordTextBox.Text;
                userCredentialsDataPasswordTextBox.Visibility = Visibility.Hidden;
                userCredentialsDataPasswordPasswordBox.Visibility = Visibility.Visible;

                changeCredentialsDataPasswordVisabilityButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDD");

                IsUserDataPasswordVisible = false;
            }
        }

        public static void UserDataHiddenPasswordFieldVisibilityOptions(TextBlock userDataPasswordHintAssist, PasswordBox userDataPasswordPasswordBox, Button clearDataPasswordButton)
        {
            if (userDataPasswordPasswordBox.Password != "")
            {
                userDataPasswordHintAssist.Visibility = Visibility.Hidden;

                clearDataPasswordButton.Visibility = Visibility.Visible;
            }

            else
            {
                userDataPasswordHintAssist.Visibility = Visibility.Visible;

                clearDataPasswordButton.Visibility = Visibility.Hidden;
            }
        }
    }
}