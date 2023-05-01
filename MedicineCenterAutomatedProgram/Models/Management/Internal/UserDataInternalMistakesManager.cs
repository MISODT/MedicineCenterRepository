using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal
{
    public class UserDataInternalMistakesManager
    {
        public static bool InternalUserDataMistakesHandler(string userDataLogin, string userDataLoginMailDomain, string userDataPassword, string errorAlertWindowText)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT PatientLogin, PatientPassword FROM Patients WHERE PatientLogin = '{userDataLogin}{userDataLoginMailDomain}' AND PatientPassword = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if (patient.PatientLogin == $"{userDataLogin}{userDataLoginMailDomain}" && patient.PatientPassword == $"{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}")
                {
                    return true;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT DoctorLogin, DoctorPassword FROM Doctors WHERE DoctorLogin = '{userDataLogin}{userDataLoginMailDomain}' AND DoctorPassword = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if (doctor.DoctorLogin == $"{userDataLogin}{userDataLoginMailDomain}" && doctor.DoctorPassword == $"{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}")
                {
                    return true;
                }
            }

            InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(errorAlertWindowText);

            return false;
        }

        public static bool InternalUserDataLoginMistakesHandler(string userDataLogin, string userDataLoginMailDomain, string errorAlertWindowText)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT PatientLogin FROM Patients WHERE PatientLogin = '{userDataLogin}{userDataLoginMailDomain}'"))
            {
                if (patient.PatientLogin == $"{userDataLogin}{userDataLoginMailDomain}")
                {
                    InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(errorAlertWindowText);

                    return false;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT DoctorLogin FROM Doctors WHERE DoctorLogin = '{userDataLogin}{userDataLoginMailDomain}'"))
            {
                if (doctor.DoctorLogin == $"{userDataLogin}{userDataLoginMailDomain}")
                {
                    InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(errorAlertWindowText);

                    return false;
                }
            }

            return true;
        }
    }
}