﻿using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionAppointmentReceiveUserControl : UserControl
    {
        private Appointments appointments;

        private Shifts shifts;

        public UserMainInteractionAppointmentReceiveUserControl(Appointments appointment, Shifts shift, Patients patient, HealingDirections healingDirection, Cities city, Streets street, Houses house)
        {
            InitializeComponent();


            appointments = appointment;


            shifts = shift;


            UserMainInteractionAppointmentReceiveUserControlShiftDateTextBlock.Text = shift.ShiftDate;

            UserMainInteractionAppointmentReceiveUserControlShiftTimeTextBlock.Text = $"{shift.ShiftStartActionTime} - {shift.ShiftEndActionTime}";


            UserMainInteractionAppointmentReceiveUserControlPatientFullNameTextBlock.Text = $"Пациент: {patient.Surname} {patient.Name} {patient.Patronymic}";


            UserMainInteractionAppointmentReceiveUserControlShiftHealingDirectionTitleTextBlock.Text = healingDirection.HealingDirectionTitle;


            UserMainInteractionAppointmentReceiveUserControlShiftHospitalAddressTextBlock.Text = $"г. {city.CityTitle}, ул. {street.StreetTitle} д. {house.HouseNumber}";
        }

        private void UserMainInteractionAppointmentReceiveUserControlAgreeAppointmentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SectionsOperationsManager.UpdateAppointmentReceivingStatus(appointments.AppointmentId, "Утверждён");

            foreach(var medicineCard in DataResponseManager.MedicineCardsJsonDataDeserialize($"SELECT MedicineCardId FROM MedicineCards, Appointments, Shifts WHERE AppointmentShiftId = ShiftId AND ShiftId = {shifts.ShiftId} AND Appointments.Patientid = MedicineCards.PatientId OR Appointments.DoctorId = MedicineCards.DoctorId"))
            {
                SectionsOperationsManager.NewMedicineCardRecordOperation("", "1", shifts.ShiftId, medicineCard.MedicineCardId);
            }

            FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsReceivingPage());
        }

        private void UserMainInteractionAppointmentReceiveUserControlDisagreeAppointmentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SectionsOperationsManager.UpdateAppointmentReceivingStatus(appointments.AppointmentId, "Не утверждён");

            FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsReceivingPage());
        }
    }
}