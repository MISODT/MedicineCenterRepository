using MedicineCenterAutomatedProgram.Models.Management.Internal;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;

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
    }
}