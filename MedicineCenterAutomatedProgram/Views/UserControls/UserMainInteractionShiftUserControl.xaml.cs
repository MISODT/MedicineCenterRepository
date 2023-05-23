using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionShiftUserControl : UserControl
    {
        private string shiftId;

        public UserMainInteractionShiftUserControl(Shifts shift, HealingDirections healingDirection, Cities city, Streets street, Houses house)
        {
            InitializeComponent();

            shiftId = shift.ShiftId;

            UserMainInteractionShiftUserControlShiftDateTextBlock.Text = shift.ShiftDate;

            UserMainInteractionShiftUserControlShiftTimeTextBlock.Text = $"{shift.ShiftStartActionTime} - {shift.ShiftEndActionTime}";

            UserMainInteractionShiftUserControlShiftHealingDirectionTitleTextBlock.Text = healingDirection.HealingDirectionTitle;

            UserMainInteractionShiftUserControlShiftHospitalAddressTextBlock.Text = $"г. {city.CityTitle}, ул. {street.StreetTitle} д. {house.HouseNumber}";

            if (DateTime.Parse(shift.ShiftDate) > DateTime.Now.Date && !UserMainInteractionIsShiftAppointmentExists())
            {
                UserMainInteractionShiftUserControlEditShiftButton.Visibility = Visibility.Visible;

                UserMainInteractionShiftUserControlDeleteShiftButton.Visibility = Visibility.Visible;
            }

            else
            {
                UserMainInteractionShiftUserControlEditShiftButton.Visibility = Visibility.Hidden;

                UserMainInteractionShiftUserControlDeleteShiftButton.Visibility = Visibility.Hidden;
            }
        }

        private bool UserMainInteractionIsShiftAppointmentExists()
        {
            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId FROM Shifts, Appointments WHERE ShiftId = AppointmentShiftId AND ShiftId = {shiftId}"))
            {
                if (shift.ShiftId == shiftId)
                {
                    return true;
                }
            }

            return false;
        }

        private void UserMainInteractionShiftUserControlEditShiftButton_Click(object sender, RoutedEventArgs e)
        {
            //FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionShiftOperationsPage(shiftId));
        }

        private void UserMainInteractionShiftUserControlDeleteShiftButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsDataOperations.UserDataRemoveShiftOperation(shiftId);

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));
        }
    }
}