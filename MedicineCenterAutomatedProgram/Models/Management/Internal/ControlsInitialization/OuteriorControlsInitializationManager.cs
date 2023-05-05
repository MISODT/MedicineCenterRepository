using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System.Collections.Generic;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization
{
    public class OuteriorControlsInitializationManager
    {
        public static List<string> LoginMailDomainComboBoxInitialization()
        {
            List<string> loginMailDomainsTitleList = new List<string>();

            loginMailDomainsTitleList.Clear();

            foreach (var mailDomain in DataResponseManager.MailDomainsJsonDataDeserialize("SELECT MailDomainTitle FROM MailDomains"))
            {
                loginMailDomainsTitleList.Add(mailDomain.MailDomainTitle);
            }

            return loginMailDomainsTitleList;
        }

        public static List<string> AddressCityComboBoxInitialization()
        {
            List<string> citiesTitleList = new List<string>();

            citiesTitleList.Clear();

            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize("SELECT DISTINCT(CityTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
            {
                citiesTitleList.Add(city.CityTitle);
            }

            return citiesTitleList;
        }

        public static string AddressCityComboBoxSelectedValueInitialization()
        {
            string cityTitle = "";

            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId AND AddressId = {UserDataSectionsInstance.User.UserAddressId}"))
            {
                cityTitle = city.CityTitle;
            }

            return cityTitle;
        }

        public static string AddressStreetComboBoxSelectedValueInitialization()
        {
            string streetTitle = "";

            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{UserDataSectionsInstance.User.AddressCityTitle}' AND AddressStreetId = StreetId AND AddressHouseId = HouseId AND AddressId = {UserDataSectionsInstance.User.UserAddressId}"))
            {
                streetTitle = street.StreetTitle;
            }

            return streetTitle;
        }

        public static string AddressHouseComboBoxSelectedValueInitialization()
        {
            string houseNumber = "";

            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseNumber) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{UserDataSectionsInstance.User.AddressCityTitle}' AND AddressStreetId = StreetId AND StreetTitle = '{UserDataSectionsInstance.User.AddressStreetTitle}' AND AddressHouseId = HouseId AND AddressId = {UserDataSectionsInstance.User.UserAddressId}"))
            {
                houseNumber = house.HouseNumber;
            }

            return houseNumber;
        }

        public static List<string> AddressStreetComboBoxInitialization()
        {
            List<string> streetsTitleList = new List<string>();

            streetsTitleList.Clear();

            if (UserDataSectionsInstance.User.AddressCityTitle == null)
            {
                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize("SELECT DISTINCT(StreetTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
                {
                    streetsTitleList.Add(street.StreetTitle);
                }
            }

            else
            {
                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{UserDataSectionsInstance.User.AddressCityTitle}' AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
                {
                    streetsTitleList.Add(street.StreetTitle);
                }
            }

            return streetsTitleList;
        }

        public static List<string> AddressHouseComboBoxInitialization()
        {
            List<string> housesNumbersList = new List<string>();

            housesNumbersList.Clear();

            if (UserDataSectionsInstance.User.AddressStreetTitle == null)
            {
                foreach (var house in DataResponseManager.HousesJsonDataDeserialize("SELECT DISTINCT(HouseNumber) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
                {
                    housesNumbersList.Add(house.HouseNumber);
                }
            }

            else
            {
                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseNumber) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{UserDataSectionsInstance.User.AddressCityTitle}' AND AddressStreetId = StreetId AND StreetTitle = '{UserDataSectionsInstance.User.AddressStreetTitle}' AND AddressHouseId = HouseId"))
                {
                    housesNumbersList.Add(house.HouseNumber);
                }
            }

            return housesNumbersList;
        }

        public static List<string> SchoolCityComboBoxInitialization()
        {
            List<string> schoolCitiesTitleList = new List<string>();

            schoolCitiesTitleList.Clear();

            foreach (var school in DataResponseManager.CitiesJsonDataDeserialize("SELECT DISTINCT(CityTitle) FROM Schools, Cities WHERE SchoolCityId = CityId"))
            {
                schoolCitiesTitleList.Add(school.CityTitle);
            }

            return schoolCitiesTitleList;
        }

        public static List<string> SchoolTypeComboBoxInitialization()
        {
            List<string> schoolTypesList = new List<string>();

            schoolTypesList.Clear();

            if (UserDataSectionsInstance.User.SchoolCityTitle == null)
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize("SELECT DISTINCT(SchoolType) FROM Schools, Cities WHERE SchoolCityId = CityId"))
                {
                    schoolTypesList.Add(school.SchoolType);
                }
            }

            else
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolType) FROM Schools, Cities WHERE SchoolCityId = CityId AND CityTitle = '{UserDataSectionsInstance.User.SchoolCityTitle}'"))
                {
                    schoolTypesList.Add(school.SchoolType);
                }
            }

            return schoolTypesList;
        }

        public static List<string> SchoolTitleComboBoxInitialization()
        {
            List<string> schoolTitlesList = new List<string>();

            schoolTitlesList.Clear();

            if(UserDataSectionsInstance.User.SchoolType == null)
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolTitle) FROM Schools, Cities WHERE SchoolCityId = CityId"))
                {
                    schoolTitlesList.Add(school.SchoolTitle);
                }
            }

            else
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolTitle) FROM Schools, Cities WHERE SchoolCityId = CityId AND SchoolType = '{UserDataSectionsInstance.User.SchoolType}' AND CityTitle = '{UserDataSectionsInstance.User.SchoolCityTitle}'"))
                {
                    schoolTitlesList.Add(school.SchoolTitle);
                }
            }

            return schoolTitlesList;
        }

        public static List<string> UniversityCityComboBoxInitialization()
        {
            List<string> universityCitiesTitleList = new List<string>();

            universityCitiesTitleList.Clear();

            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize("SELECT DISTINCT(CityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId"))
            {
                universityCitiesTitleList.Add(city.CityTitle);
            }

            return universityCitiesTitleList;
        }

        public static List<string> UniversityTypeComboBoxInitialization()
        {
            List<string> universityTypesList = new List<string>();

            universityTypesList.Clear();

            if(UserDataSectionsInstance.User.UniversityCityTitle == null)
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize("SELECT DISTINCT(UniversityType) FROM Universities, Cities WHERE UniversityCityId = CityId"))
                {
                    universityTypesList.Add(university.UniversityType);
                }
            }

            else
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT DISTINCT(UniversityType) FROM Universities, Cities WHERE UniversityCityId = CityId AND CityTitle = '{UserDataSectionsInstance.User.UniversityCityTitle}'"))
                {
                    universityTypesList.Add(university.UniversityType);
                }
            }

            return universityTypesList;
        }

        public static List<string> UniversityTitleComboBoxInitialization()
        {
            List<string> universityTitlesList = new List<string>();

            universityTitlesList.Clear();

            if(UserDataSectionsInstance.User.UniversityType == null)
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize("SELECT DISTINCT(UniversityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId"))
                {
                    universityTitlesList.Add(university.UniversityTitle);
                }
            }

            else
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT DISTINCT(UniversityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId AND UniversityType = '{UserDataSectionsInstance.User.UniversityType}' AND CityTitle = '{UserDataSectionsInstance.User.UniversityCityTitle}'"))
                {
                    universityTitlesList.Add(university.UniversityTitle);
                }
            }

            return universityTitlesList;
        }
    }
}