using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionShiftUserControl : UserControl
    {
        private Shifts shifts = new Shifts();

        public UserMainInteractionShiftUserControl(Shifts shift, HealingDirections healingDirection, Cities city, Streets street, Houses house)
        {
            InitializeComponent();


            shifts.ShiftId = shift.ShiftId;


            shifts.ShiftDate = shift.ShiftDate;

            shifts.ShiftStartActionTime = shift.ShiftStartActionTime;

            shifts.ShiftEndActionTime = shift.ShiftEndActionTime;


            shifts.DoctorId = shift.DoctorId;


            shifts.ShiftHealingDirectionId = shift.ShiftHealingDirectionId;


            shifts.ShiftHospitalAddressId = shift.ShiftHospitalAddressId;


            UserMainInteractionShiftUserControlShiftDateTextBlock.Text = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

            UserMainInteractionShiftUserControlShiftTimeTextBlock.Text = $"{TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString()} - {TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString()}";

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
            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId FROM Shifts, Appointments WHERE ShiftId = AppointmentShiftId AND ShiftId = {shifts.ShiftId}"))
            {
                if (shift.ShiftId == shifts.ShiftId)
                {
                    return true;
                }
            }

            return false;
        }

        private void UserMainInteractionShiftUserControlEditShiftButton_Click(object sender, RoutedEventArgs e) 
        {

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionShiftOperationsPage(shifts));
        }

        private void UserMainInteractionShiftUserControlDeleteShiftButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsDataOperations.UserDataRemoveShiftOperation(shifts.ShiftId);

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));
        }
    }
}