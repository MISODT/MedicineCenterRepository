using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;

namespace MedicineCenterAutomatedProgram.Models.Management.UserDataMistakesManager
{
    public class InternalMistakesManager
    {
        public static bool CheckDataMistakes(string login, string loginMailDomain, string password, string errorAlertWindowText)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Login, Password FROM Patients WHERE Login = '{login}{loginMailDomain}' AND Password = '{CryptionManager.EncryptData(password)}'"))
            {
                if (patient.Login == $"{login}{loginMailDomain}" && patient.Password == $"{CryptionManager.EncryptData(password)}")
                {
                    return true;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Login, Password FROM Doctors WHERE Login = '{login}{loginMailDomain}' AND Password = '{CryptionManager.EncryptData(password)}'"))
            {
                if (doctor.Login == $"{login}{loginMailDomain}" && doctor.Password == $"{CryptionManager.EncryptData(password)}")
                {
                    return true;
                }
            }

            InteriorControlsInitializationManager.InitializeErrorAlertWindow(errorAlertWindowText);

            return false;
        }

        public static bool CheckLoginMistakes(string login, string loginMailDomain, string errorAlertWindowText)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Login FROM Patients WHERE Login = '{login}{loginMailDomain}'"))
            {
                if (patient.Login == $"{login}{loginMailDomain}")
                {
                    InteriorControlsInitializationManager.InitializeErrorAlertWindow(errorAlertWindowText);

                    return false;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Login FROM Doctors WHERE Login = '{login}{loginMailDomain}'"))
            {
                if (doctor.Login == $"{login}{loginMailDomain}")
                {
                    InteriorControlsInitializationManager.InitializeErrorAlertWindow(errorAlertWindowText);

                    return false;
                }
            }

            return true;
        }

        public static bool CheckUserReferencesMistakes()
        {
            string userId = "";

            if (SectionsInstance.Patient != null)
            {
                foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Id FROM Patients, Appointments WHERE Id = PatientId AND Id = {SectionsInstance.Patient.Id}"))
                {
                    userId = patient.Id;
                }
            }

            if(SectionsInstance.Doctor != null)
            {
                foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id FROM Doctors, Appointments WHERE Id = DoctorId AND Id = {SectionsInstance.Doctor.Id}"))
                {
                    userId = doctor.Id;
                }

                foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Id FROM Doctors, Shifts WHERE Id = DoctorId AND Id = {SectionsInstance.Doctor.Id}"))
                {
                    userId = doctor.Id;
                }
            }

            if(userId == "")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}