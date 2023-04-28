using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections;
using System.Windows;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations
{
    public class CredentialsUserDataOperationsManager
    {
        public static string UserDataAuthorizationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach(var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT PatientId FROM Patients WHERE PatientLogin = '{userDataLogin}{userDataLoginMailDomain}' AND PatientPassword = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                return patient.PatientId;
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT DoctorId FROM Doctors WHERE DoctorLogin = '{userDataLogin}{userDataLoginMailDomain}' AND DoctorPassword = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                return doctor.DoctorId;
            }

            return "";
        }

        public static void UserDataRegistrationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            if (UserDataSectionsInstance.User.UserPositionIsPatient)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Patients (PatientName, PatientSurname, PatientPatronymic, PatientDateOfBirth, PatientGender, PatientAddressId, PatientSchoolId, PatientUniversityId, PatientUniversityStartEducationYear, PatientUniversityEndEducationYear, PatientLogin, PatientPassword) VALUES ('{UserDataSectionsInstance.User.UserName}', '{UserDataSectionsInstance.User.UserSurname}', '{UserDataSectionsInstance.User.UserPatronymic}', '{UserDataSectionsInstance.User.UserYearOfBirth}-{UserDataSectionsInstance.User.UserMonthOfBirthNumber}-{UserDataSectionsInstance.User.UserDayOfBirth}', '{UserDataSectionsInstance.User.UserGender}', {UserDataSectionsInstance.User.UserAddressId}, {UserDataSectionsInstance.User.UserSchoolId}, {UserDataSectionsInstance.User.UserUniversityId}, {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, {UserDataSectionsInstance.User.UserUniversityEndEducationYear}, '{userDataLogin}{userDataLoginMailDomain}', '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}');");
            }

            if (UserDataSectionsInstance.User.UserPositionIsDoctor)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Doctors (DoctorName, DoctorSurname, DoctorPatronymic, DoctorDateOfBirth, DoctorGender, DoctorAddressId, DoctorSchoolId, DoctorUniversityId, DoctorUniversityStartEducationYear, DoctorUniversityEndEducationYear, DoctorLogin, DoctorPassword) VALUES ('{UserDataSectionsInstance.User.UserName}', '{UserDataSectionsInstance.User.UserSurname}', '{UserDataSectionsInstance.User.UserPatronymic}', '{UserDataSectionsInstance.User.UserYearOfBirth}-{UserDataSectionsInstance.User.UserMonthOfBirthNumber}-{UserDataSectionsInstance.User.UserDayOfBirth}', '{UserDataSectionsInstance.User.UserGender}', {UserDataSectionsInstance.User.UserAddressId}, {UserDataSectionsInstance.User.UserSchoolId}, {UserDataSectionsInstance.User.UserUniversityId}, {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, {UserDataSectionsInstance.User.UserUniversityEndEducationYear}, '{userDataLogin}{userDataLoginMailDomain}', '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}');");
            }
        }
    }
}