using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift
{
    public partial class UserMainInteractionShiftOperationsPage : Page
    {
        private Shifts shifts;

        public UserMainInteractionShiftOperationsPage(Shifts shift)
        {
            InitializeComponent();

            if(shift != null)
            {
                shifts = shift;


                DateOnly shiftDateOnly = DateOnly.Parse(shift.ShiftDate);

                TimeOnly shiftStartActionTimeOnly = TimeOnly.Parse(shifts.ShiftStartActionTime);

                TimeOnly shiftEndActionTimeOnly = TimeOnly.Parse(shifts.ShiftEndActionTime);


                UserMainInteractionShiftOperationsDayOfShiftComboBox.SelectedValue = shiftDateOnly.Day;

                UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex = shiftDateOnly.Month - 1;

                UserMainInteractionShiftOperationsYearOfShiftComboBox.SelectedValue = shiftDateOnly.Year;


                UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox.SelectedValue = shiftStartActionTimeOnly.Hour;

                UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox.SelectedValue = shiftStartActionTimeOnly.Minute.ToString("D2");

                UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox.SelectedValue = shiftEndActionTimeOnly.Hour;

                UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox.SelectedValue = shiftEndActionTimeOnly.Minute.ToString("D2");


                UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.SelectedValue = OuteriorControlsInitializationManager.HealingDirectionComboBoxSelectedValueInitialization(shift.ShiftId, null);

                UserMainInteractionShiftOperationsHospitalOfShiftComboBox.SelectedValue = OuteriorControlsInitializationManager.HospitalAddressSelectedValueInitialization(shift.ShiftHospitalAddressId, null);
            }

            else
            {
                UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox.SelectedIndex = 0;

                UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox.SelectedIndex = 0;

                UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox.SelectedIndex = 0;

                UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox.SelectedIndex = 0;


                UserMainInteractionShiftOperationsHospitalOfShiftComboBox.SelectedIndex = 0;


                UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.SelectedIndex = 0;
            }
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

            InteriorControlsInitializationManager.InitializeDayComboBox(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1);
            InteriorControlsInitializationManager.InitializeMonthComboBox(UserMainInteractionShiftOperationsMonthOfShiftComboBox);
            InteriorControlsInitializationManager.InitializeYearComboBox(UserMainInteractionShiftOperationsYearOfShiftComboBox, DateTime.Now.Year);

            InteriorControlsInitializationManager.InitializeHourComboBox(UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox);
            InteriorControlsInitializationManager.InitializeMinuteComboBox(UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox);

            InteriorControlsInitializationManager.InitializeHourComboBox(UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox);
            InteriorControlsInitializationManager.InitializeMinuteComboBox(UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox);

            UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.ItemsSource = OuteriorControlsInitializationManager.HealingDirectionComboBoxInitialization();

            UserMainInteractionShiftOperationsHospitalOfShiftComboBox.ItemsSource = OuteriorControlsInitializationManager.HospitalAddressComboBoxInitialization(null);
        }

        private void UserMainInteractionShiftOperationsDayOfShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsDayOfShiftComboBoxHintAssist);

            UserMainInteractionAcceptButtonState();
        }

        private void UserMainInteractionShiftOperationsMonthOfShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserMainInteractionShiftOperationsMonthOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBoxHintAssist);

            InteriorControlsInitializationManager.InitializeDayComboBox(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1);

            SectionsInstance.SectionsBinding.UserMonthOfBirthNumber = UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1;

            UserMainInteractionAcceptButtonState();
        }

        private void UserMainInteractionShiftOperationsYearOfShiftMistakeTextBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FieldsViewManager.ChangeComboBoxView(UserMainInteractionShiftOperationsYearOfShiftComboBox, UserMainInteractionShiftOperationsYearOfShiftComboBoxHintAssist);

            UserMainInteractionAcceptButtonState();
        }

        private void UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UserMainInteractionAcceptButtonState();

        private void UserMainInteractionAcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if(shifts == null)
            {
                if (ExternalMistakesManager.CheckDateOfShiftMistakes(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox, UserMainInteractionShiftOperationsYearOfShiftComboBox, UserMainInteractionShiftOperationsShiftDateMistakeTextBlock) && 
                    ExternalMistakesManager.CheckTimeOfShiftMistakes(UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox, UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox, UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox, UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox, UserMainInteractionShiftOperationsShiftTimeMistakeTextBlock))
                {
                    SectionsOperationsManager.NewShiftOperation($"{UserMainInteractionShiftOperationsYearOfShiftComboBox.SelectedValue}-{UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1}-{UserMainInteractionShiftOperationsDayOfShiftComboBox.SelectedValue}", $"{UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox.SelectedValue}:{UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox.SelectedValue}", $"{UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox.SelectedValue}:{UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox.SelectedValue}", OuteriorControlsInitializationManager.HealingDirectionComboBoxSelectedValueInitialization(null, UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.SelectedValue.ToString()), OuteriorControlsInitializationManager.HospitalAddressSelectedValueInitialization(null, UserMainInteractionShiftOperationsHospitalOfShiftComboBox.SelectedValue.ToString()));

                    FrameManager.HomeFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));
                }
            }

            else
            {
                if (ExternalMistakesManager.CheckDateOfShiftMistakes(UserMainInteractionShiftOperationsDayOfShiftComboBox, UserMainInteractionShiftOperationsMonthOfShiftComboBox, UserMainInteractionShiftOperationsYearOfShiftComboBox, UserMainInteractionShiftOperationsShiftDateMistakeTextBlock) &&
                    ExternalMistakesManager.CheckTimeOfShiftMistakes(UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox, UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox, UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox, UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox, UserMainInteractionShiftOperationsShiftTimeMistakeTextBlock))
                {
                    SectionsOperationsManager.UpdateShiftOperation($"{shifts.ShiftId}", $"{UserMainInteractionShiftOperationsYearOfShiftComboBox.SelectedValue}-{UserMainInteractionShiftOperationsMonthOfShiftComboBox.SelectedIndex + 1}-{UserMainInteractionShiftOperationsDayOfShiftComboBox.SelectedValue}", $"{UserMainInteractionShiftOperationsHourOfShiftStartActionTimeComboBox.SelectedValue}:{UserMainInteractionShiftOperationsMinuteOfShiftStartActionTimeComboBox.SelectedValue}", $"{UserMainInteractionShiftOperationsHourOfShiftEndActionTimeComboBox.SelectedValue}:{UserMainInteractionShiftOperationsMinuteOfShiftEndActionTimeComboBox.SelectedValue}", OuteriorControlsInitializationManager.HealingDirectionComboBoxSelectedValueInitialization(null, UserMainInteractionShiftOperationsHealingDirectionTitleOfShiftComboBox.SelectedValue.ToString()), OuteriorControlsInitializationManager.HospitalAddressSelectedValueInitialization(null, UserMainInteractionShiftOperationsHospitalOfShiftComboBox.SelectedValue.ToString()));

                    FrameManager.HomeFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));
                }
            }
            
        }
    }
}