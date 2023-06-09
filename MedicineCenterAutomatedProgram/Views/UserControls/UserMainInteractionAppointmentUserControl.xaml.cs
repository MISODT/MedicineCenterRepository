﻿using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionAppointmentUserControl : UserControl
    {
        private Appointments appointments;

        public UserMainInteractionAppointmentUserControl(Appointments appointment, Shifts shift, Doctors doctor, HealingDirections healingDirection, Cities city, Streets street, Houses house)
        {
            InitializeComponent();


            appointments = appointment;


            UserMainInteractionAppointmentUserControlShiftDateTextBlock.Text = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

            UserMainInteractionAppointmentUserControlShiftTimeTextBlock.Text = $"{TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString()} - {TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString()}";


            UserMainInteractionAppointmentUserControlDoctorFullNameTextBlock.Text = $"Врач: {doctor.Surname} {doctor.Name} {doctor.Patronymic}";


            UserMainInteractionAppointmentUserControlShiftHealingDirectionTitleTextBlock.Text = healingDirection.HealingDirectionTitle;


            UserMainInteractionAppointmentUserControlState(appointment.AppointmentStatus);


            if (!string.IsNullOrWhiteSpace(appointment.AppointmentDescription))
            {
                UserMainInteractionAppointmentUserControlAppointmentDescriptionTextBlock.Text = $"Описание: {appointment.AppointmentDescription}";
            }


            UserMainInteractionAppointmentUserControlAppointmentHospitalAddressTextBlock.Text = $"г. {city.CityTitle}, ул. {street.StreetTitle} д. {house.HouseNumber}";

            UserMainInteractionAppointmentUserControlAppointmentStatusTextBlock.Text = $"Статус: {appointment.AppointmentStatus}";
        }

        private void UserMainInteractionAppointmentUserControlState(string appointmentStatus)
        {
            if (appointmentStatus == "Отправлен")
            {
                UserMainInteractionAppointmentUserControlBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#888");

                UserMainInteractionAppointmentUserControlEditAppointmentButton.Visibility = Visibility.Visible;

                UserMainInteractionAppointmentUserControlDeleteAppointmentButton.Visibility = Visibility.Visible;
            }

            if (appointmentStatus == "Утверждён")
            {
                UserMainInteractionAppointmentUserControlBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0A5");

                UserMainInteractionAppointmentUserControlEditAppointmentButton.Visibility = Visibility.Hidden;

                UserMainInteractionAppointmentUserControlDeleteAppointmentButton.Visibility = Visibility.Hidden;
            }

            if (appointmentStatus == "Не утверждён")
            {
                UserMainInteractionAppointmentUserControlBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#A00");

                UserMainInteractionAppointmentUserControlEditAppointmentButton.Visibility = Visibility.Hidden;

                UserMainInteractionAppointmentUserControlDeleteAppointmentButton.Visibility = Visibility.Hidden;
            }
        }

        private void UserMainInteractionAppointmentUserControlEditAppointmentButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentOperationsPage(appointments));

        private void UserMainInteractionAppointmentUserControlDeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            SectionsOperationsManager.RemoveAppointmentOperation(appointments.AppointmentId);

            FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));
        }
    }
}