using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations
{
    public class UserDataSectionsBinding
    {
        public bool UserPositionIsPatient { get; set; }

        public bool UserPositionIsDoctor { get; set; }

        public string UserId { get; set; }

        public string? UserProfilePhotoUri { get; set; } = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string UserPatronymic { get; set; }


        public int UserDayOfBirth { get; set; }

        public string UserMonthOfBirth { get; set; }

        public int UserMonthOfBirthNumber { get; set; }

        public int UserYearOfBirth { get; set; }

        public string UserDateOfBirth { get; set; }


        public bool UserGenderIsMale { get; set; }

        public bool UserGenderIsFemale { get; set; }

        public bool UserGenderIsUndefined { get; set; }

        public string UserGender { get; set; }


        public string UserAddressId { get; set; }


        public string AddressId { get; set; }

        public string AddressCityId { get; set; }

        public string AddressCityTitle { get; set; }

        public string AddressStreetId { get; set; }

        public string AddressStreetTitle { get; set; }

        public string AddressHouseId { get; set; }

        public string AddressHouseNumber { get; set; }


        public string? UserSchoolId { get; set; } = "NULL";

        public string? SchoolCityTitle { get; set; }

        public string? SchoolType { get; set; }

        public string? SchoolTitle { get; set; }


        public string? UserUniversityId { get; set; } = "NULL";

        public string? UniversityCityTitle { get; set; }

        public string? UniversityType { get; set; }

        public string? UniversityTitle { get; set; }


        public int UserUniversityStartEducationYear { get; set; }

        public int UserUniversityEndEducationYear { get; set; }

        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public void ExtractUserAddressData()
        {
            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT CityId FROM Cities WHERE CityTitle = '{AddressCityTitle}'"))
            {
                AddressCityId = city.CityId;
            }

            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT StreetId FROM Streets WHERE StreetTitle = '{AddressStreetTitle}'"))
            {
                AddressStreetId = street.StreetId;
            }

            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT HouseId FROM Houses WHERE HouseNumber = '{AddressHouseNumber}'"))
            {
                AddressHouseId = house.HouseId;
            }

            foreach (var address in DataResponseManager.AddressesJsonDataDeserialize($"SELECT AddressId FROM Addresses WHERE AddressCityId = {AddressCityId} AND AddressStreetId = {AddressStreetId} AND AddressHouseId = {AddressHouseId}"))
            {
                AddressId = address.AddressId;
            }

            UserAddressId = AddressId;
        }

        public void ExtractUserEducationData()
        {
            foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT SchoolId FROM Schools WHERE SchoolType = '{SchoolType}' AND SchoolTitle = '{SchoolTitle}'"))
            {
                UserSchoolId = school.SchoolId;
            }

            foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT UniversityId FROM Universities WHERE UniversityType = '{UniversityType}' AND UniversityTitle = '{UniversityTitle}'"))
            {
                UserUniversityId = university.UniversityId;
            }
        }
    }
}