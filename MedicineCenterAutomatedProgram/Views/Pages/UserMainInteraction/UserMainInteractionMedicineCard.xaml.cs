using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionMedicineCard : Page
    {
        private ObservableCollection<UserMainInteractionMedicineCardRecordUserControl> userMainInteractionMedicineCardRecordUserControlCollection = new ObservableCollection<UserMainInteractionMedicineCardRecordUserControl>();

        private string userMainInteractionMedicineCardParameterValue;

        public UserMainInteractionMedicineCard(string userMainInteractionMedicineCardParameter)
        {
            InitializeComponent();


            userMainInteractionMedicineCardParameterValue = userMainInteractionMedicineCardParameter;
        }

        private void UserMainInteractionMedicineCardRecordItemsControlInitialization()
        {
            string userMainInteractionPatientId = "";

            string userMainInteractionDoctorId = "";


            string userMainInteractionMedicineCardParameter = "";


            if(SectionsInstance.Patient == null)
            {
                if (userMainInteractionMedicineCardParameterValue == "Моя")
                {
                    userMainInteractionMedicineCardParameter = $"AND MedicineCards.DoctorId = {SectionsInstance.Doctor.Id}";
                }

                if(userMainInteractionMedicineCardParameterValue == "Пациентов")
                {
                    userMainInteractionMedicineCardParameter = $"AND MedicineCards.DoctorId = {SectionsInstance.Doctor.Id} AND MedicineCardRecordShiftId = ShiftId AND Shifts.DoctorId = {SectionsInstance.Doctor.Id}";
                }
            }

            if(SectionsInstance.Doctor == null)
            {
                userMainInteractionMedicineCardParameter = $"AND MedicineCards.PatientId = {SectionsInstance.Patient.Id}";
            }


            foreach (var medicineCardRecord in DataResponseManager.MedicineCardRecordsJsonDataDeserialize($"SELECT DISTINCT(MedicineCardRecordId), MedicineCardRecordPatientStatement, MedicineCardRecordDiseaseId, MedicineCardRecordShiftId, MedicineCardRecordMedicineCardId FROM MedicineCardRecords, MedicineCards, Patients, Doctors, Shifts WHERE MedicineCardRecordMedicineCardId = MedicineCardId {userMainInteractionMedicineCardParameter}"))
            {
                foreach(var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Patients, MedicineCards, MedicineCardRecords WHERE PatientId = Id AND MedicineCardRecordMedicineCardId = MedicineCardId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                {
                    if(patient.Id != "")
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, DoctorId, ShiftHealingDirectionId FROM Shifts, MedicineCardRecords WHERE MedicineCardRecordShiftId = ShiftId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionId, HealingDirectionTitle FROM HealingDirections, MedicineCardRecords, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND MedicineCardRecordShiftId = ShiftId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                            {
                                foreach (var disease in DataResponseManager.DiseasesJsonDataDeserialize($"SELECT DiseaseId, DiseaseTitle FROM Diseases, MedicineCardRecords WHERE MedicineCardRecordDiseaseId = DiseaseId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                                {
                                    userMainInteractionMedicineCardRecordUserControlCollection.Add(new UserMainInteractionMedicineCardRecordUserControl(userMainInteractionMedicineCardParameterValue, medicineCardRecord, shift, patient, null, healingDirection, disease));
                                }
                            }
                        }
                    }
                }

                foreach(var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, Name, Surname, Patronymic FROM Doctors, MedicineCards, MedicineCardRecords WHERE DoctorId = Id AND MedicineCardRecordMedicineCardId = MedicineCardId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                {
                    if(doctor.Id != "")
                    {
                        foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId, ShiftDate, ShiftStartActionTime, ShiftEndActionTime, DoctorId, ShiftHealingDirectionId FROM Shifts, MedicineCardRecords WHERE MedicineCardRecordShiftId = ShiftId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                        {
                            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionId, HealingDirectionTitle FROM HealingDirections, MedicineCardRecords, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND MedicineCardRecordShiftId = ShiftId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                            {
                                foreach (var disease in DataResponseManager.DiseasesJsonDataDeserialize($"SELECT DiseaseId, DiseaseTitle FROM Diseases, MedicineCardRecords WHERE MedicineCardRecordDiseaseId = DiseaseId AND MedicineCardRecordId = {medicineCardRecord.MedicineCardRecordId}"))
                                {
                                    userMainInteractionMedicineCardRecordUserControlCollection.Add(new UserMainInteractionMedicineCardRecordUserControl(userMainInteractionMedicineCardParameterValue, medicineCardRecord, shift, null, doctor, healingDirection, disease));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UserMainInteractionMedicineCard_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionMedicineCardRecordItemsControlInitialization();

            UserMainInteractionMedicineCardRecordsItemsControl.ItemsSource = userMainInteractionMedicineCardRecordUserControlCollection;
        }
    }
}