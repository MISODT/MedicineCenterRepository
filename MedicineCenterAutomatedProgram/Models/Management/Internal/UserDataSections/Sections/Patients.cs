namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections
{
    public class Patients
    {
        public string Id { get; set; }

        public string? ProfilePhotoUri { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string AddressId { get; set; }

        public string? SchoolId { get; set; }

        public string? UniversityId { get; set; }

        public string? UniversityStartEducationYear { get; set; }

        public string? UniversityEndEducationYear { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}