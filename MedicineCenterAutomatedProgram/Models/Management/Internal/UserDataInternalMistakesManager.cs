using MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal
{
    public class UserDataInternalMistakesManager
    {
        public static bool InternalUserDataMistakesHandler(string userDataLogin, string userDataLoginMailDomain, string userDataPassword)
        {
            foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT PatientId FROM Patients WHERE PatientLogin = '{userDataLogin}{userDataLoginMailDomain}' AND PatientPassword = '{userDataPassword}'"))
            {
                if(patient.PatientId != "")
                {
                    return false;
                }
            }

            foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT DoctorId FROM Doctors WHERE DoctorLogin = '{userDataLogin}{userDataLoginMailDomain}' AND DoctorPassword = '{userDataPassword}'"))
            {
                if(doctor.DoctorId != "")
                {
                    return false;
                }
            }

            return true;
        }

        public static bool InternalUserDataLoginMistakesHandler(string userDataLogin, string userDataLoginMailDomain, Border alertMessageBorder, TextBlock alertMessageTypeTextBlock, TextBlock alertMessageTextTextBlock)
        {
            if (UserDataSectionsInstance.User.UserPositionIsPatient)
            {
                foreach (var patient in DataResponseManager.PatientsJsonDataDeserialize($"SELECT PatientLogin FROM Patients WHERE PatientLogin = '{userDataLogin}{userDataLoginMailDomain}'"))
                {
                    if(patient.PatientLogin == $"{userDataLogin}{userDataLoginMailDomain}")
                    {
                        InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(alertMessageBorder, alertMessageTypeTextBlock, "Ошибка", alertMessageTextTextBlock, "Логин уже используется");

                        return false;
                    }
                }
            }

            if (UserDataSectionsInstance.User.UserPositionIsDoctor)
            {
                foreach (var doctor in DataResponseManager.DoctorsJsonDataDeserialize($"SELECT DoctorLogin FROM Doctors WHERE DoctorLogin = '{userDataLogin}{userDataLoginMailDomain}'"))
                {
                    if (doctor.DoctorLogin == $"{userDataLogin}{userDataLoginMailDomain}")
                    {
                        InteriorControlsInitializationManager.AlertMessageBorderItemsInitialization(alertMessageBorder, alertMessageTypeTextBlock, "Ошибка", alertMessageTextTextBlock, "Логин уже используется");

                        return false;
                    }
                }
            }

            return true;
        }
    }
}