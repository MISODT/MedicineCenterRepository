using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations
{
    public class UserDataSectionsInstance
    {
        public static Patients? Patient { get; set; }

        public static Doctors? Doctor { get; set; }


        public static Appointments? Appointment { get; set; }


        public static UserDataSectionsBinding User { get; set; } = new UserDataSectionsBinding();
    }
}