using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations
{
    public class OperationsManager
    {
        public static Patients? UserDataPatientAuthorizationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Id, ProfilePhotoUri, Name, Surname, DateOfBirth, Gender, AddressId, SchoolId, UniversityId, UniversityStartEducationYear, UniversityEndEducationYear, Login, Password FROM Patients WHERE Login = '{userDataLogin}{userDataLoginMailDomain}' AND Password = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
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

        public static void UserDataMainInteractionNewAppointmentOperation(string userId)
        {
            UserDataSectionsInstance.Appointment = new Appointments();

            if (UserDataSectionsInstance.Doctor != null)
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId FROM Shifts, Doctors WHERE Id = DoctorId AND DoctorId = {userId}"))
                {
                    WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Appointments (AppointmentStatus, AppointmentDescription, ShiftId, PatientId, DoctorId) VALUES ('{UserDataSectionsInstance.Appointment.AppointmentStatus}', '{UserDataSectionsInstance.Appointment.AppointmentDescription}', {shift.ShiftId}, NULL, {UserDataSectionsInstance.Doctor.Id});");
                }
            }

            if(UserDataSectionsInstance.Patient != null)
            {
                foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftId FROM Shifts, Doctors WHERE Id = DoctorId AND DoctorId = {userId}"))
                {
                    WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Appointments (AppointmentStatus, AppointmentDescription, ShiftId, PatientId, DoctorId) VALUES ('{UserDataSectionsInstance.Appointment.AppointmentStatus}', '{UserDataSectionsInstance.Appointment.AppointmentDescription}', {shift.ShiftId}, {UserDataSectionsInstance.Patient.Id}, NULL);");
                }
            }
        }
    }
}