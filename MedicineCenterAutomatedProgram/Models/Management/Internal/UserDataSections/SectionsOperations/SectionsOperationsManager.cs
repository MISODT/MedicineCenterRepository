﻿using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations
{
    public class SectionsOperationsManager
    {
        public static Patients? AuthorizePatientOperation(string login, string loginMailDomain, string password)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password FROM Patients WHERE Login = '{login}{loginMailDomain}' AND Password = '{CryptionManager.EncryptData(password)}'"))
            {
                if(patient.Id != "")
                {
                    SectionsInstance.Patient = new Patients()
                    {
                        Id = patient.Id,
                        ProfilePhotoUri = patient.ProfilePhotoUri,
                        Name = patient.Name,
                        Surname = patient.Surname,
                        Patronymic = patient.Patronymic,
                        DateOfBirth = patient.DateOfBirth,
                        Gender = patient.Gender,
                        AddressId = patient.AddressId,
                        SchoolId = patient.SchoolId,
                        UniversityId = patient.UniversityId,
                        UniversityStartEducationYear = patient.UniversityStartEducationYear,
                        UniversityEndEducationYear = patient.UniversityEndEducationYear,
                        Login = patient.Login,
                        Password = patient.Password
                    };

                    return SectionsInstance.Patient;
                }
            }

            return null;
        }

        public static Doctors? AuthorizeDoctorOperation(string login, string loginMailDomain, string password)
        {
            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password FROM Doctors WHERE Login = '{login}{loginMailDomain}' AND Password = '{CryptionManager.EncryptData(password)}'"))
            {
                if (doctor.Id != "")
                {
                    SectionsInstance.Doctor = new Doctors()
                    {
                        Id = doctor.Id,
                        ProfilePhotoUri = doctor.ProfilePhotoUri,
                        Name = doctor.Name,
                        Surname = doctor.Surname,
                        Patronymic = doctor.Patronymic,
                        DateOfBirth = doctor.DateOfBirth,
                        Gender = doctor.Gender,
                        AddressId = doctor.AddressId,
                        SchoolId = doctor.SchoolId,
                        UniversityId = doctor.UniversityId,
                        UniversityStartEducationYear = doctor.UniversityStartEducationYear,
                        UniversityEndEducationYear = doctor.UniversityEndEducationYear,
                        Login = doctor.Login,
                        Password = doctor.Password
                    };

                    return SectionsInstance.Doctor;
                }
            }

            return null;
        }

        public static void RegisterUserOperation(string login, string loginMailDomain, string password)
        {
            MySqlConnection mySqlConnection = new MySqlConnection("Server=88.87.92.66;Database=MedicineCenter;User=any;Password=HFuzTOaG5M");

            string query = "";

            if (SectionsInstance.SectionsBinding.UserPositionIsPatient)
            {
                query = $"INSERT INTO Patients (ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password) VALUES ('{SectionsInstance.SectionsBinding.UserProfilePhotoUri}', '{SectionsInstance.SectionsBinding.UserName}', '{SectionsInstance.SectionsBinding.UserSurname}', '{SectionsInstance.SectionsBinding.UserPatronymic}', '{SectionsInstance.SectionsBinding.UserYearOfBirth}-{SectionsInstance.SectionsBinding.UserMonthOfBirthNumber}-{SectionsInstance.SectionsBinding.UserDayOfBirth}', '{SectionsInstance.SectionsBinding.UserGender}', {SectionsInstance.SectionsBinding.UserAddressId}, {SectionsInstance.SectionsBinding.UserSchoolId}, {SectionsInstance.SectionsBinding.UserUniversityId}, {SectionsInstance.SectionsBinding.UserUniversityStartEducationYear}, {SectionsInstance.SectionsBinding.UserUniversityEndEducationYear}, '{login}{loginMailDomain}', '{CryptionManager.EncryptData(password)}');";
            }

            if (SectionsInstance.SectionsBinding.UserPositionIsDoctor)
            {
                query = $"INSERT INTO Doctors (ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password) VALUES ('{SectionsInstance.SectionsBinding.UserProfilePhotoUri}', '{SectionsInstance.SectionsBinding.UserName}', '{SectionsInstance.SectionsBinding.UserSurname}', '{SectionsInstance.SectionsBinding.UserPatronymic}', '{SectionsInstance.SectionsBinding.UserYearOfBirth}-{SectionsInstance.SectionsBinding.UserMonthOfBirthNumber}-{SectionsInstance.SectionsBinding.UserDayOfBirth}', '{SectionsInstance.SectionsBinding.UserGender}', {SectionsInstance.SectionsBinding.UserAddressId}, {SectionsInstance.SectionsBinding.UserSchoolId}, {SectionsInstance.SectionsBinding.UserUniversityId}, {SectionsInstance.SectionsBinding.UserUniversityStartEducationYear}, {SectionsInstance.SectionsBinding.UserUniversityEndEducationYear}, '{login}{loginMailDomain}', '{CryptionManager.EncryptData(password)}');";
            }

            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);

            if(mySqlConnection.State == ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }

            mySqlCommand.ExecuteNonQuery();

            mySqlCommand.Dispose();
        }

        public static void UpdateUserOperation()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("Server=88.87.92.66;Database=MedicineCenter;User=any;Password=HFuzTOaG5M");

            string query = "";

            if (SectionsInstance.Patient != null)
            {
                query = $"UPDATE Patients SET ProfilePhotoUri = '{SectionsInstance.SectionsBinding.UserProfilePhotoUri}', Name = '{SectionsInstance.SectionsBinding.UserName}', Surname = '{SectionsInstance.SectionsBinding.UserSurname}', Patronymic = '{SectionsInstance.SectionsBinding.UserPatronymic}', Gender = '{SectionsInstance.SectionsBinding.UserGender}', AddressId = {SectionsInstance.SectionsBinding.UserAddressId}, SchoolId = {SectionsInstance.SectionsBinding.UserSchoolId}, UniversityId = {SectionsInstance.SectionsBinding.UserUniversityId}, UniversityStartEducationYear = {SectionsInstance.SectionsBinding.UserUniversityStartEducationYear}, UniversityEndEducationYear = {SectionsInstance.SectionsBinding.UserUniversityEndEducationYear} WHERE Id = {SectionsInstance.SectionsBinding.UserId}";
            }

            if (SectionsInstance.Doctor != null)
            {
                query = $"UPDATE Doctors SET ProfilePhotoUri = '{SectionsInstance.SectionsBinding.UserProfilePhotoUri}', Name = '{SectionsInstance.SectionsBinding.UserName}', Surname = '{SectionsInstance.SectionsBinding.UserSurname}', Patronymic = '{SectionsInstance.SectionsBinding.UserPatronymic}', Gender = '{SectionsInstance.SectionsBinding.UserGender}', AddressId = {SectionsInstance.SectionsBinding.UserAddressId}, SchoolId = {SectionsInstance.SectionsBinding.UserSchoolId}, UniversityId = {SectionsInstance.SectionsBinding.UserUniversityId}, UniversityStartEducationYear = {SectionsInstance.SectionsBinding.UserUniversityStartEducationYear}, UniversityEndEducationYear = {SectionsInstance.SectionsBinding.UserUniversityEndEducationYear} WHERE Id = {SectionsInstance.SectionsBinding.UserId}";
            }

            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);

            if (mySqlConnection.State == ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }

            mySqlCommand.ExecuteNonQuery();

            mySqlCommand.Dispose();
        }

        public static void UpdateAppointmentOperation(string appointmentId, string shiftId, string appointmentDescription)
        {
            WebResponseManager.ResponseFromRequestQuery($"UPDATE Appointments SET AppointmentDescription = '{appointmentDescription}', AppointmentShiftId = {shiftId} WHERE AppointmentId = {appointmentId}");
        }

        public static void NewAppointmentOperation(string shiftId, string appointmentDescription)
        {
            if(SectionsInstance.Patient != null)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Appointments (AppointmentStatus, AppointmentDescription, AppointmentShiftId, PatientId, DoctorId) VALUES ('Отправлен', '{appointmentDescription}', {shiftId}, {SectionsInstance.Patient.Id}, NULL);");
            }

            if (SectionsInstance.Doctor != null)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Appointments (AppointmentStatus, AppointmentDescription, AppointmentShiftId, PatientId, DoctorId) VALUES ('Отправлен', '{appointmentDescription}', {shiftId}, NULL, {SectionsInstance.Doctor.Id});");
            }
        }

        public static void RemoveAppointmentOperation(string appointmentId)
        {
            WebResponseManager.ResponseFromRequestQuery($"DELETE FROM Appointments WHERE AppointmentId = {appointmentId}");
        }

        public static void UpdateShiftOperation(string shiftId, string shiftDate, string shiftStartActionTime, string shiftEndActionTime, string shiftHealingDirectionId, string shiftHospitalAddressId)
        {
            WebResponseManager.ResponseFromRequestQuery($"UPDATE Shifts SET ShiftDate = '{shiftDate}', ShiftStartActionTime = '{shiftStartActionTime}', ShiftEndActionTime = '{shiftEndActionTime}', ShiftHealingDirectionId = {shiftHealingDirectionId}, DoctorId = {SectionsInstance.Doctor.Id}, ShiftHospitalAddressId = {shiftHospitalAddressId} WHERE ShiftId = {shiftId}");
        }

        public static void RemoveShiftOperation(string shiftId)
        {
            WebResponseManager.ResponseFromRequestQuery($"DELETE FROM Shifts WHERE ShiftId = {shiftId}");
        }

        public static void NewShiftOperation(string shiftDate, string shiftStartActionTime, string shiftEndActionTime, string shiftHealingDirectionId, string shiftHospitalAddressId)
        {
            WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Shifts (ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId) VALUES ('{shiftDate}', '{shiftStartActionTime}', '{shiftEndActionTime}', {shiftHealingDirectionId}, {SectionsInstance.Doctor.Id}, {shiftHospitalAddressId});");
        }

        public static void UpdateAppointmentReceivingStatus(string appointmentId, string appointmentStatus)
        {
            WebResponseManager.ResponseFromRequestQuery($"UPDATE Appointments SET AppointmentStatus = '{appointmentStatus}' WHERE AppointmentId = {appointmentId}");
        }

        public static void RemoveUserOperation(string userId)
        {
            if(SectionsInstance.Patient != null)
            {
                foreach(var medicineCard in DataResponseManager.MedicineCardsJsonDataDeserialize($"SELECT MedicineCardId FROM MedicineCards WHERE PatientId = {userId}"))
                {
                    WebResponseManager.ResponseFromRequestQuery($"DELETE FROM MedicineCardRecords WHERE MedicineCardRecordMedicineCardId = {medicineCard.MedicineCardId}");

                    WebResponseManager.ResponseFromRequestQuery($"DELETE FROM MedicineCards WHERE PatientId = {userId}");


                    WebResponseManager.ResponseFromRequestQuery($"DELETE FROM Patients WHERE Id = {userId}");
                }
            }
            
            if(SectionsInstance.Doctor != null)
            {
                foreach (var medicineCard in DataResponseManager.MedicineCardsJsonDataDeserialize($"SELECT MedicineCardId FROM MedicineCards WHERE DoctorId = {userId}"))
                {
                    WebResponseManager.ResponseFromRequestQuery($"DELETE FROM MedicineCardRecords WHERE MedicineCardRecordMedicineCardId = {medicineCard.MedicineCardId}");

                    WebResponseManager.ResponseFromRequestQuery($"DELETE FROM MedicineCards WHERE DoctorId = {userId}");


                    WebResponseManager.ResponseFromRequestQuery($"DELETE FROM Doctors WHERE Id = {userId}");
                }
            }
        }

        public static void NewMedicineCardOperation()
        {
            string medicineCardId = "";

            if(SectionsInstance.Patient != null)
            {
                foreach(var medicineCard in DataResponseManager.MedicineCardsJsonDataDeserialize($"SELECT MedicineCardId FROM MedicineCards WHERE PatientId = {SectionsInstance.Patient.Id}"))
                {
                    medicineCardId = medicineCard.MedicineCardId;
                }

                if(medicineCardId == "")
                {
                    WebResponseManager.ResponseFromRequestQuery($"INSERT INTO MedicineCards(PatientId, DoctorId) VALUES ({SectionsInstance.Patient.Id}, NULL)");
                }
            }

            if(SectionsInstance.Doctor != null)
            {
                foreach (var medicineCard in DataResponseManager.MedicineCardsJsonDataDeserialize($"SELECT MedicineCardId FROM MedicineCards WHERE DoctorId = {SectionsInstance.Doctor.Id}"))
                {
                    medicineCardId = medicineCard.MedicineCardId;
                }

                if (medicineCardId == "")
                {
                    WebResponseManager.ResponseFromRequestQuery($"INSERT INTO MedicineCards(PatientId, DoctorId) VALUES (NULL, {SectionsInstance.Doctor.Id})");
                }
            }
        }

        public static void NewMedicineCardRecordOperation(string medicineCardRecordPatientStatement, string medicineCardRecordDiseaseId, string medicineCardRecordShiftId, string medicineCardRecordMedicineCardId)
        {
            WebResponseManager.ResponseFromRequestQuery($"INSERT INTO MedicineCardRecords (MedicineCardRecordPatientStatement, MedicineCardRecordDiseaseId, MedicineCardRecordShiftId, MedicineCardRecordMedicineCardId) VALUES ('{medicineCardRecordPatientStatement}', {medicineCardRecordDiseaseId}, {medicineCardRecordShiftId}, {medicineCardRecordMedicineCardId});");
        }

        public static void UpdateMedicineCardRecordOperation(string medicineCardRecordId, string medicineCardRecordPatientStatement, string medicineCardRecordDiseaseId)
        {
            WebResponseManager.ResponseFromRequestQuery($"UPDATE MedicineCardRecords SET MedicineCardRecordPatientStatement = '{medicineCardRecordPatientStatement}', MedicineCardRecordDiseaseId = {medicineCardRecordDiseaseId} WHERE MedicineCardRecordId = {medicineCardRecordId}");
        }

        public static void RemoveMedicineCardRecordOperation(string medicineCardRecordId)
        {
            WebResponseManager.ResponseFromRequestQuery($"DELETE FROM MedicineCardRecords WHERE MedicineCardRecordsId = {medicineCardRecordId}");
        }
    }
}