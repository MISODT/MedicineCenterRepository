using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal
{
    public class UserDataInternalMistakesManager
    {
        public static bool InternalUserDataMistakesHandler(string userDataLogin, string userDataLoginMailDomain, string userDataPassword, string errorAlertWindowText)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Login, Password FROM Patients WHERE Login = '{userDataLogin}{userDataLoginMailDomain}' AND Password = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if (patient.Login == $"{userDataLogin}{userDataLoginMailDomain}" && patient.Password == $"{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}")
                {
                    return true;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Login, Password FROM Doctors WHERE Login = '{userDataLogin}{userDataLoginMailDomain}' AND Password = '{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}'"))
            {
                if (doctor.Login == $"{userDataLogin}{userDataLoginMailDomain}" && doctor.Password == $"{UserDataCryptionManager.UserDataEncrypt(userDataPassword)}")
                {
                    return true;
                }
            }

            InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(errorAlertWindowText);

            return false;
        }

        public static bool InternalUserDataLoginMistakesHandler(string userDataLogin, string userDataLoginMailDomain, string errorAlertWindowText)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT Login FROM Patients WHERE Login = '{userDataLogin}{userDataLoginMailDomain}'"))
            {
                if (patient.Login == $"{userDataLogin}{userDataLoginMailDomain}")
                {
                    InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(errorAlertWindowText);

                    return false;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT Login FROM Doctors WHERE Login = '{userDataLogin}{userDataLoginMailDomain}'"))
            {
                if (doctor.Login == $"{userDataLogin}{userDataLoginMailDomain}")
                {
                    InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(errorAlertWindowText);

                    return false;
                }
            }

            return true;
        }
    }
}