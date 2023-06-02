using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations
{
    public class SectionsInstance
    {
        public static Patients? Patient { get; set; }

        public static Doctors? Doctor { get; set; }


        public static SectionsBindingManager SectionsBinding { get; set; } = new SectionsBindingManager();
    }
}