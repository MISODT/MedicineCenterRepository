using ControlzEx.Standard;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System.Windows;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations
{
    public class UserDataSectionsDataOperations
    {
        public static Patients? UserDataPatientAuthorizationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password FROM Patients WHERE Login = '{userDataLogin}{userDataLoginMailDomain}' AND Password = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if(patient.Id != "")
                {
                    UserDataSectionsInstance.Patient = new Patients()
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

                    return UserDataSectionsInstance.Patient;
                }
            }

            return null;
        }

        public static Doctors? UserDataDoctorAuthorizationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password FROM Doctors WHERE Login = '{userDataLogin}{userDataLoginMailDomain}' AND Password = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if (doctor.Id != "")
                {
                    UserDataSectionsInstance.Doctor = new Doctors()
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

                    return UserDataSectionsInstance.Doctor;
                }
            }

            return null;
        }

        public static void UserDataRegistrationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            if (UserDataSectionsInstance.User.UserPositionIsPatient)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Patients (ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password) VALUES ('{UserDataSectionsInstance.User.UserProfilePhotoUri}', '{UserDataSectionsInstance.User.UserName}', '{UserDataSectionsInstance.User.UserSurname}', '{UserDataSectionsInstance.User.UserPatronymic}', '{UserDataSectionsInstance.User.UserYearOfBirth}-{UserDataSectionsInstance.User.UserMonthOfBirthNumber}-{UserDataSectionsInstance.User.UserDayOfBirth}', '{UserDataSectionsInstance.User.UserGender}', {UserDataSectionsInstance.User.UserAddressId}, {UserDataSectionsInstance.User.UserSchoolId}, {UserDataSectionsInstance.User.UserUniversityId}, {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, {UserDataSectionsInstance.User.UserUniversityEndEducationYear}, '{userDataLogin}{userDataLoginMailDomain}', '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}');");
            }

            if (UserDataSectionsInstance.User.UserPositionIsDoctor)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Doctors (ProfilePhotoUri, Name, Surname, Patronymic, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password) VALUES ('{UserDataSectionsInstance.User.UserProfilePhotoUri}', '{UserDataSectionsInstance.User.UserName}', '{UserDataSectionsInstance.User.UserSurname}', '{UserDataSectionsInstance.User.UserPatronymic}', '{UserDataSectionsInstance.User.UserYearOfBirth}-{UserDataSectionsInstance.User.UserMonthOfBirthNumber}-{UserDataSectionsInstance.User.UserDayOfBirth}', '{UserDataSectionsInstance.User.UserGender}', {UserDataSectionsInstance.User.UserAddressId}, {UserDataSectionsInstance.User.UserSchoolId}, {UserDataSectionsInstance.User.UserUniversityId}, {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, {UserDataSectionsInstance.User.UserUniversityEndEducationYear}, '{userDataLogin}{userDataLoginMailDomain}', '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}');");
            }
        }

        public static void UserDataUpdateOperation()
        {
            if (UserDataSectionsInstance.User.UserPositionIsPatient)
            {
                WebResponseManager.ResponseFromRequestQuery($"UPDATE Patients SET ProfilePhotoUri = '{UserDataSectionsInstance.User.UserProfilePhotoUri}', Name = '{UserDataSectionsInstance.User.UserName}', Surname = '{UserDataSectionsInstance.User.UserSurname}', Patronymic = '{UserDataSectionsInstance.User.UserPatronymic}', Gender = '{UserDataSectionsInstance.User.UserGender}', AddressId = {UserDataSectionsInstance.User.UserAddressId}, SchoolId = {UserDataSectionsInstance.User.UserSchoolId}, UniversityId = {UserDataSectionsInstance.User.UserUniversityId}, UniversityStartEducationYear = {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, UniversityEndEducationYear = {UserDataSectionsInstance.User.UserUniversityEndEducationYear} WHERE Id = {UserDataSectionsInstance.User.UserId}");
            }

            if (UserDataSectionsInstance.User.UserPositionIsDoctor)
            {
                WebResponseManager.ResponseFromRequestQuery($"UPDATE Doctors SET ProfilePhotoUri = '{UserDataSectionsInstance.User.UserProfilePhotoUri}', Name = '{UserDataSectionsInstance.User.UserName}', Surname = '{UserDataSectionsInstance.User.UserSurname}', Patronymic = '{UserDataSectionsInstance.User.UserPatronymic}', Gender = '{UserDataSectionsInstance.User.UserGender}', AddressId = {UserDataSectionsInstance.User.UserAddressId}, SchoolId = {UserDataSectionsInstance.User.UserSchoolId}, UniversityId = {UserDataSectionsInstance.User.UserUniversityId}, UniversityStartEducationYear = {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, UniversityEndEducationYear = {UserDataSectionsInstance.User.UserUniversityEndEducationYear} WHERE Id = {UserDataSectionsInstance.User.UserId}");
            }
        }

        public static void UserDataMainInteractionUpdateAppointmentOperation(string appointmentId, string shiftId, string appointmentDescription)
        {
            WebResponseManager.ResponseFromRequestQuery($"UPDATE Appointments SET AppointmentDescription = '{appointmentDescription}', AppointmentShiftId = {shiftId} WHERE AppointmentId = {appointmentId}");
        }

        public static void UserDataMainInteractionNewAppointmentOperation(string shiftId, string appointmentDescription)
        {
            if(UserDataSectionsInstance.Patient != null)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Appointments (AppointmentStatus, AppointmentDescription, AppointmentShiftId, PatientId, DoctorId) VALUES ('Отправлен', '{appointmentDescription}', {shiftId}, {UserDataSectionsInstance.Patient.Id}, NULL);");
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Appointments (AppointmentStatus, AppointmentDescription, AppointmentShiftId, PatientId, DoctorId) VALUES ('Отправлен', '{appointmentDescription}', {shiftId}, NULL, {UserDataSectionsInstance.Doctor.Id});");
            }
        }

        public static void UserDataMainInteractionRemoveAppointmentOperation(string appointmentId)
        {
            WebResponseManager.ResponseFromRequestQuery($"DELETE FROM Appointments WHERE AppointmentId = {appointmentId}");
        }

        public static void UserDataMainInteractionUpdateShiftOperation(string shiftId, string shiftDate, string shiftStartActionTime, string shiftEndActionTime, string shiftHealingDirectionId, string shiftHospitalAddressId)
        {
            WebResponseManager.ResponseFromRequestQuery($"UPDATE Shifts SET ShiftDate = '{shiftDate}', ShiftStartActionTime = '{shiftStartActionTime}', ShiftEndActionTime = '{shiftEndActionTime}', ShiftHealingDirectionId = {shiftHealingDirectionId}, DoctorId = {UserDataSectionsInstance.Doctor.Id}, ShiftHospitalAddressId = {shiftHospitalAddressId} WHERE ShiftId = {shiftId}");
        }

        public static void UserDataRemoveShiftOperation(string shiftId)
        {
            WebResponseManager.ResponseFromRequestQuery($"DELETE FROM Shifts WHERE ShiftId = {shiftId}");
        }

        public static void UserDataMainInteractionNewShiftOperation(string shiftDate, string shiftStartActionTime, string shiftEndActionTime, string shiftHealingDirectionId, string shiftHospitalAddressId)
        {
            WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Shifts (ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId) VALUES ('{shiftDate}', '{shiftStartActionTime}', '{shiftEndActionTime}', {shiftHealingDirectionId}, {UserDataSectionsInstance.Doctor.Id}, {shiftHospitalAddressId});");

            MessageBox.Show($"INSERT INTO Shifts (ShiftDate, ShiftStartActionTime, ShiftEndActionTime, ShiftHealingDirectionId, DoctorId, ShiftHospitalAddressId) VALUES ('{shiftDate}', '{shiftStartActionTime}', '{shiftEndActionTime}', {shiftHealingDirectionId}, {UserDataSectionsInstance.Doctor.Id}, {shiftHospitalAddressId});");
        }
    }
}