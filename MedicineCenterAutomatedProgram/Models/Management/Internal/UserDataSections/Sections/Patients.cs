namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections
{
    public class Patients
    {
        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientSurname { get; set; }

        public string PatientPatronymic { get; set; }

        public string PatientDateOfBirth { get; set; }

        public string? PatientGender { get; set; }

        public string PatientAddressId { get; set; }

        public string? PatientSchoolId { get; set; }

        public string? PatientUniversityId { get; set; }

        public string? PatientUniversityStartEducationYear { get; set; }

        public string? PatientUniversityEndEducationYear { get; set; }

        public string PatientLogin { get; set; }

        public string PatientPassword { get; set; }
    }
}