using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift
{
    public partial class UserMainInteractionShiftOperationsPage : Page
    {
        public UserMainInteractionShiftOperationsPage()
        {
            InitializeComponent();
        }

        private void UserMainInteractionAcceptButtonState()
        {
            if (UserMainInteractionShiftOperationsDayOfShiftComboBox.SelectedItem != null && UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedItem != null && UserMainInteractionShiftOperationsYearOfShiftComboBox.SelectedItem != null)
            {
                UserMainInteractionAcceptButton.IsEnabled = true;
            }

            else
            {
                UserMainInteractionAcceptButton.IsEnabled = false;
            }
        }

        private void UserMainInteractionShiftOperationsPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionShiftOperationsShiftDateMistakeTextBlock.Visibility = Visibility.Hidden;

            UserMainInteractionShiftOperationsShiftTimeMistakeTextBlock.Visibility = Visibility.Hidden;

            InteriorControlsInitializationManager.DayComboBoxInitialization(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1);
            InteriorControlsInitializationManager.MonthComboBoxInitialization(UserMainInteractionShiftOperationsMonthOfShiftComboBox);
            InteriorControlsInitializationManager.YearComboBoxInitialization(UserMainInteractionShiftOperationsYearOfShiftComboBox, DateTime.Now.Year);

            InteriorControlsInitializationManager.HourComboBoxInitialization(UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox);
            InteriorControlsInitializationManager.MinuteComboBoxInitialization(UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox);

            InteriorControlsInitializationManager.HourComboBoxInitialization(UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox);
            InteriorControlsInitializationManager.MinuteComboBoxInitialization(UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox);

            UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.ItemsSource = OuteriorControlsInitializationManager.HealingDirectionComboBoxInitialization();

            UserMainInteractionShiftOperationsHospitalOfShiftComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(null);
        }

        private void UserMainInteractionShiftOperationsDayOfShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsDayOfShiftComboBoxHintAssist);

            UserMainInteractionAcceptButtonState();
        }

        private void UserMainInteractionShiftOperationsMonthOfShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserMainInteractionShiftOperationsMonthOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBoxHintAssist);

            InteriorControlsInitializationManager.DayComboBoxInitialization(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1);

            InteriorControlsInitializationManager.MonthNumberComboBoxInitialization(UserMainInteractionShiftOperationsMonthOfShiftComboBox);

            UserMainInteractionAcceptButtonState();
        }

        private void UserMainInteractionShiftOperationsYearOfShiftMistakeTextBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDataFieldsViewManager.UserDataComboBoxFieldVisibilityOptions(UserMainInteractionShiftOperationsYearOfShiftComboBox, UserMainInteractionShiftOperationsYearOfShiftComboBoxHintAssist);

            UserMainInteractionAcceptButtonState();
        }

        private void UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionAcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserDataExternalMistakesManager.ExternalUserDataDateOfShiftMistakesHandler(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox, UserMainInteractionShiftOperationsYearOfShiftComboBox, UserMainInteractionShiftOperationsShiftDateMistakeTextBlock) && 
                UserDataExternalMistakesManager.ExternalUserDataTimeOfShiftMistakesHandler(UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox, UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox, UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox, UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox, UserMainInteractionShiftOperationsShiftTimeMistakeTextBlock))
            {
                UserDataSectionsDataOperations.UserDataMainInteractionNewShiftOperation($"{UserMainInteractionShiftOperationsYearOfShiftComboBox.SelectedValue}-{UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1}-{UserMainInteractionShiftOperationsDayOfShiftComboBox.SelectedValue}", $"{UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox.SelectedValue}:{UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox.SelectedValue}", $"{UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox.SelectedValue}:{UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox.SelectedValue}", OuteriorControlsInitializationManager.HealingDirectionComboBoxSelectedValueInitialization(null, UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.SelectedValue.ToString()), OuteriorControlsInitializationManager.HospitalAddressSelectedValueInitialization(null, UserMainInteractionShiftOperationsHospitalOfShiftComboBox.SelectedValue.ToString()));

                FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));
            }
        }
    }
}