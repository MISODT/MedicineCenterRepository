using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations
{
    public class CredentialsUserDataOperationsManager
    {
        public static Patients? UserDataPatientAuthorizationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT PatientId, PatientName, PatientSurname, PatientDateOfBirth, PatientGender, PatientAddressId, PatientSchoolId, PatientUniversityId, PatientUniversityStartEducationYear, PatientUniversityEndEducationYear, PatientLogin, PatientPassword FROM Patients WHERE PatientLogin = '{userDataLogin}{userDataLoginMailDomain}' AND PatientPassword = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if(patient.PatientId != "")
                {
                    UserDataSectionsInstance.Patient = new Patients()
                    {
                        PatientId = patient.PatientId,
                        PatientName = patient.PatientName,
                        PatientSurname = patient.PatientSurname,
                        PatientPatronymic = patient.PatientPatronymic,
                        PatientDateOfBirth = patient.PatientDateOfBirth,
                        PatientGender = patient.PatientGender,
                        PatientAddressId = patient.PatientAddressId,
                        PatientSchoolId = patient.PatientSchoolId,
                        PatientUniversityId = patient.PatientUniversityId,
                        PatientUniversityStartEducationYear = patient.PatientUniversityStartEducationYear,
                        PatientUniversityEndEducationYear = patient.PatientUniversityEndEducationYear,
                        PatientLogin = patient.PatientLogin,
                        PatientPassword = patient.PatientPassword
                    };

                    return UserDataSectionsInstance.Patient;
                }
            }

            return null;
        }

        public static Doctors? UserDataDoctorAuthorizationOperation(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT DoctorId, DoctorName, DoctorSurname, DoctorDateOfBirth, DoctorGender, DoctorAddressId, DoctorSchoolId, DoctorUniversityId, DoctorUniversityStartEducationYear, DoctorUniversityEndEducationYear, DoctorLogin, DoctorPassword FROM Doctors WHERE DoctorLogin = '{userDataLogin}{userDataLoginMailDomain}' AND DoctorPassword = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if (doctor.DoctorId != "")
                {
                    UserDataSectionsInstance.Doctor = new Doctors()
                    {
                        DoctorId = doctor.DoctorId,
                        DoctorName = doctor.DoctorName,
                        DoctorSurname = doctor.DoctorSurname,
                        DoctorPatronymic = doctor.DoctorPatronymic,
                        DoctorDateOfBirth = doctor.DoctorDateOfBirth,
                        DoctorGender = doctor.DoctorGender,
                        DoctorAddressId = doctor.DoctorAddressId,
                        DoctorSchoolId = doctor.DoctorSchoolId,
                        DoctorUniversityId = doctor.DoctorUniversityId,
                        DoctorUniversityStartEducationYear = doctor.DoctorUniversityStartEducationYear,
                        DoctorUniversityEndEducationYear = doctor.DoctorUniversityEndEducationYear,
                        DoctorLogin = doctor.DoctorLogin,
                        DoctorPassword = doctor.DoctorPassword
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
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Patients (PatientName, PatientSurname, PatientPatronymic, PatientDateOfBirth, PatientGender, PatientAddressId, PatientSchoolId, PatientUniversityId, PatientUniversityStartEducationYear, PatientUniversityEndEducationYear, PatientLogin, PatientPassword) VALUES ('{UserDataSectionsInstance.User.UserName}', '{UserDataSectionsInstance.User.UserSurname}', '{UserDataSectionsInstance.User.UserPatronymic}', '{UserDataSectionsInstance.User.UserYearOfBirth}-{UserDataSectionsInstance.User.UserMonthOfBirthNumber}-{UserDataSectionsInstance.User.UserDayOfBirth}', '{UserDataSectionsInstance.User.UserGender}', {UserDataSectionsInstance.User.UserAddressId}, {UserDataSectionsInstance.User.UserSchoolId}, {UserDataSectionsInstance.User.UserUniversityId}, {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, {UserDataSectionsInstance.User.UserUniversityEndEducationYear}, '{userDataLogin}{userDataLoginMailDomain}', '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}');");
            }

            if (UserDataSectionsInstance.User.UserPositionIsDoctor)
            {
                WebResponseManager.ResponseFromRequestQuery($"INSERT INTO Doctors (DoctorName, DoctorSurname, DoctorPatronymic, DoctorDateOfBirth, DoctorGender, DoctorAddressId, DoctorSchoolId, DoctorUniversityId, DoctorUniversityStartEducationYear, DoctorUniversityEndEducationYear, DoctorLogin, DoctorPassword) VALUES ('{UserDataSectionsInstance.User.UserName}', '{UserDataSectionsInstance.User.UserSurname}', '{UserDataSectionsInstance.User.UserPatronymic}', '{UserDataSectionsInstance.User.UserYearOfBirth}-{UserDataSectionsInstance.User.UserMonthOfBirthNumber}-{UserDataSectionsInstance.User.UserDayOfBirth}', '{UserDataSectionsInstance.User.UserGender}', {UserDataSectionsInstance.User.UserAddressId}, {UserDataSectionsInstance.User.UserSchoolId}, {UserDataSectionsInstance.User.UserUniversityId}, {UserDataSectionsInstance.User.UserUniversityStartEducationYear}, {UserDataSectionsInstance.User.UserUniversityEndEducationYear}, '{userDataLogin}{userDataLoginMailDomain}', '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}');");
            }
        }
    }
}