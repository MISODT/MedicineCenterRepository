using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionMedicineCardRecordUserControl : UserControl
    {
        private MedicineCardRecords medicineCardRecords;

        public UserMainInteractionMedicineCardRecordUserControl(MedicineCardRecords medicineCardRecord, Shifts shift, Patients patient, Doctors doctor, HealingDirections healingDirection, Diseases disease)
        {
            InitializeComponent();

            medicineCardRecords = medicineCardRecord;


            UserMainInteractionMedicineCardRecordUserControlIdTextBlock.Text = $"Запись №{medicineCardRecord.MedicineCardRecordId}";


            UserMainInteractionMedicineCardRecordUserControlShiftDateTextBlock.Text = DateOnly.Parse(shift.ShiftDate).ToLongDateString();

            UserMainInteractionMedicineCardRecordUserControlShiftTimeTextBlock.Text = $"{TimeOnly.Parse(shift.ShiftStartActionTime).ToShortTimeString()} - {TimeOnly.Parse(shift.ShiftEndActionTime).ToShortTimeString()}";


            if(patient != null)
            {
                UserMainInteractionMedicineCardRecordUserControlPatientFullNameTextBlock.Text = $"{patient.Surname} {patient.Name} {patient.Patronymic}";
            }

            if(doctor != null)
            {
                UserMainInteractionMedicineCardRecordUserControlPatientFullNameTextBlock.Text = $"{doctor.Surname} {doctor.Name} {doctor.Patronymic}";
            }


            UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBlock.Text = medicineCardRecord.MedicineCardRecordPatientStatement;


            UserMainInteractionMedicineCardRecordUserControlShiftHealingDirectionTextBlock.Text = $"Лечением занимался Врач {healingDirection.HealingDirectionTitle}";
        }

        private void UserMainInteractionMedicineCardRecordUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(SectionsInstance.Doctor == null)
            {
                UserMainInteractionMedicineCardEditRecordUserControlDiseasesButton.Visibility = Visibility.Collapsed;

                UserMainInteractionMedicineCardDeleteRecordUserControlDiseasesButton.Visibility = Visibility.Collapsed;

                UserMainInteractionMedicineCardRecordUserControlDiseasesComboBox.Visibility = Visibility.Collapsed;

                UserMainInteractionMedicineCardRecordUserControlDiseaseTextBlock.Visibility = Visibility.Visible;


                UserMainInteractionMedicineCardRecordUserControlDiseaseTextBlock.Text = $"Заключение врача о заболевании: {OuteriorControlsInitializationManager.DiseasesComboBoxSelectedValueInitialization(medicineCardRecords.MedicineCardRecordDiseaseId, null)}";
            }

            else
            {
                UserMainInteractionMedicineCardEditRecordUserControlDiseasesButton.Visibility = Visibility.Visible;

                UserMainInteractionMedicineCardDeleteRecordUserControlDiseasesButton.Visibility = Visibility.Visible;

                UserMainInteractionMedicineCardRecordUserControlDiseasesComboBox.Visibility = Visibility.Visible;

                UserMainInteractionMedicineCardRecordUserControlDiseaseTextBlock.Visibility = Visibility.Collapsed;


                UserMainInteractionMedicineCardRecordUserControlDiseasesComboBox.ItemsSource = OuteriorControlsInitializationManager.DiseasesComboBoxInitialization();

                UserMainInteractionMedicineCardRecordUserControlDiseasesComboBox.SelectedValue = OuteriorControlsInitializationManager.DiseasesComboBoxSelectedValueInitialization(medicineCardRecords.MedicineCardRecordId, null);
            }

            UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox.Visibility = Visibility.Hidden;

            UserMainInteractionMedicineCardAgreeRecordUserControlDiseasesButton.Visibility = Visibility.Collapsed;
        }

        private void UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox.Text))
            {
                UserMainInteractionMedicineCardAgreeRecordUserControlDiseasesButton.Visibility = Visibility.Visible;
            }

            else
            {
                UserMainInteractionMedicineCardAgreeRecordUserControlDiseasesButton.Visibility = Visibility.Collapsed;
            }
        }

        private void UserMainInteractionMedicineCardEditRecordUserControlDiseasesButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox.Visibility = Visibility.Visible;

            UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox.Text = UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBlock.Text;

            UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBlock.Visibility = Visibility.Hidden;
        }

        private void UserMainInteractionMedicineCardDeleteRecordUserControlDiseasesButton_Click(object sender, RoutedEventArgs e)
        {
            SectionsOperationsManager.RemoveMedicineCardRecordOperation(medicineCardRecords.MedicineCardRecordId);

            FrameManager.HomeFrame.Navigate(new UserMainInteractionMedicineCard("Пациенты"));
        }

        private void UserMainInteractionMedicineCardAgreeRecordUserControlDiseasesButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox.Text != medicineCardRecords.MedicineCardRecordPatientStatement)
            {
                SectionsOperationsManager.UpdateMedicineCardRecordOperation(medicineCardRecords.MedicineCardRecordId, UserMainInteractionMedicineCardRecordUserControlPatientStatementTextBox.Text, OuteriorControlsInitializationManager.DiseasesComboBoxSelectedValueInitialization(null, UserMainInteractionMedicineCardRecordUserControlDiseasesComboBox.SelectedValue.ToString()));

                FrameManager.HomeFrame.Navigate(new UserMainInteractionMedicineCard("Пациенты"));
            }
        }
    }
}