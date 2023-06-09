﻿using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using System;
using System.Collections.Generic;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.ControlsInitialization
{
    public class OuteriorControlsInitializationManager
    {
        public static List<string> DiseasesComboBoxInitialization()
        {
            List<string> diseases = new List<string>();

            foreach (var disease in DataResponseManager.DiseasesJsonDataDeserialize("SELECT DiseaseTitle FROM Diseases"))
            {
                diseases.Add(disease.DiseaseTitle);
            }

            return diseases;
        }

        public static string DiseasesComboBoxSelectedValueInitialization(string medicineCardRecordId, string diseaseTitle)
        {
            if (medicineCardRecordId != null)
            {
                foreach (var disease in DataResponseManager.DiseasesJsonDataDeserialize($"SELECT DiseaseTitle FROM Diseases, MedicineCardRecords WHERE MedicineCardRecordDiseaseId = DiseaseId AND MedicineCardRecordId = {medicineCardRecordId}"))
                {
                    return disease.DiseaseTitle;
                }
            }

            else
            {
                foreach (var disease in DataResponseManager.DiseasesJsonDataDeserialize($"SELECT DiseaseId, DiseaseTitle FROM Diseases WHERE DiseaseTitle = '{diseaseTitle}'"))
                {
                    return disease.DiseaseId;
                }
            }

            return "";
        }

        public static List<string> HealingDirectionComboBoxInitialization()
        {
            List<string> healingDirections = new List<string>();

            foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize("SELECT HealingDirectionTitle FROM HealingDirections"))
            {
                healingDirections.Add(healingDirection.HealingDirectionTitle);
            }

            return healingDirections;
        }

        public static string HealingDirectionComboBoxSelectedValueInitialization(string shiftId, string healingDirectionTitle)
        {
            if(shiftId != null)
            {
                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionTitle FROM HealingDirections, Shifts WHERE ShiftHealingDirectionId = HealingDirectionId AND ShiftId = {shiftId}"))
                {
                    return healingDirection.HealingDirectionTitle;
                }
            }

            else
            {
                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionId FROM HealingDirections WHERE HealingDirectionTitle = '{healingDirectionTitle}'"))
                {
                    return healingDirection.HealingDirectionId;
                }
            }

            return "";
        }

        public static List<string> HospitalAddressComboBoxInitialization(string shiftId)
        {
            List<string> hospitalAddresses = new List<string>();

            if(shiftId == null)
            {
                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityId), CityTitle FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId"))
                {
                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetId), StreetTitle FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND CityId = {city.CityId}"))
                    {
                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseId), HouseNumber FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND CityId = {city.CityId} AND StreetId = {street.StreetId}"))
                        {
                            hospitalAddresses.Add($" г. {city.CityTitle}, ул. {street.StreetTitle}, д. {house.HouseNumber} ");
                        }
                    }
                }
            }

            else
            {
                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityId), CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftId = {shiftId}"))
                {
                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetId), StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND CityId = {city.CityId} AND ShiftId = {shiftId}"))
                    {
                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseId), HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND CityId = {city.CityId} AND StreetId = {street.StreetId} AND ShiftId = {shiftId}"))
                        {
                            hospitalAddresses.Add($" г. {city.CityTitle}, ул. {street.StreetTitle}, д. {house.HouseNumber} ");
                        }
                    }
                }
            }

            return hospitalAddresses;
        }

        public static string HospitalAddressSelectedValueInitialization(string shiftHospitalAddressId, string shiftHospitalAddressValue)
        {
            if (shiftHospitalAddressId == null)
            {
                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityId) FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND CityTitle = '{FieldsViewManager.ChangeAddressFormatView(shiftHospitalAddressValue, "г. ", ",")}'"))
                {
                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetId) FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND CityId = {city.CityId} AND StreetTitle = '{FieldsViewManager.ChangeAddressFormatView(shiftHospitalAddressValue, "ул. ", ",")}'"))
                    {
                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseId) FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND CityId = {city.CityId} AND StreetId = {street.StreetId} AND HouseNumber = '{FieldsViewManager.ChangeAddressFormatView(shiftHospitalAddressValue, "д. ", " ")}'"))
                        {
                            foreach (var address in DataResponseManager.HospitalAddressesJsonDataDeserialize($"SELECT DISTINCT(HospitalAddressId) FROM HospitalAddresses, Cities, Streets, Houses WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND CityId = {city.CityId} AND StreetId = {street.StreetId} AND HouseId = {house.HouseId}"))
                            {
                                return address.HospitalAddressId;
                            }
                        }
                    }
                }
            }

            else
            {
                foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityId), CityTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND ShiftHospitalAddressId = {shiftHospitalAddressId}"))
                {
                    foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetId), StreetTitle FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND CityId = {city.CityId} AND ShiftHospitalAddressId = {shiftHospitalAddressId}"))
                    {
                        foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseId), HouseNumber FROM HospitalAddresses, Cities, Streets, Houses, Shifts WHERE HospitalAddressCityId = CityId AND HospitalAddressStreetId = StreetId AND HospitalAddressHouseId = HouseId AND ShiftHospitalAddressId = HospitalAddressId AND CityId = {city.CityId} AND StreetId = {street.StreetId} AND ShiftHospitalAddressId = {shiftHospitalAddressId}"))
                        {
                            return $" г. {city.CityTitle}, ул. {street.StreetTitle}, д. {house.HouseNumber} ";
                        }
                    }
                }
            }

            return "";
        }

        public static List<string> AppointmentDateComboBoxValueInitialization(string shiftId)
        {
            List<string> shiftDates = new List<string>();

            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftDate FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shiftId}"))
            {
                shiftDates.Add($"{DateOnly.Parse(shift.ShiftDate)}");
            }

            return shiftDates;
        }

        public static List<string> AppointmentTimeComboBoxValueInitialization(string shiftId)
        {
            List<string> shiftTimes = new List<string>();

            foreach (var shift in DataResponseManager.ShiftsJsonDataDeserialize($"SELECT ShiftStartActionTime, ShiftEndActionTime FROM Doctors, Shifts WHERE Id = DoctorId AND ShiftId = {shiftId}"))
            {
                shiftTimes.Add($"с {TimeOnly.Parse(shift.ShiftStartActionTime)} до {TimeOnly.Parse(shift.ShiftEndActionTime)}");
            }

            return shiftTimes;
        }

        public static string AddressCityComboBoxSelectedValueInitialization()
        {
            string cityTitle = "";

            foreach (var city in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId AND AddressId = {SectionsInstance.SectionsBinding.UserAddressId}"))
            {
                cityTitle = city.CityTitle;
            }

            return cityTitle;
        }

        public static string AddressStreetComboBoxSelectedValueInitialization()
        {
            string streetTitle = "";

            foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{SectionsInstance.SectionsBinding.AddressCityTitle}' AND AddressStreetId = StreetId AND AddressHouseId = HouseId AND AddressId = {SectionsInstance.SectionsBinding.UserAddressId}"))
            {
                streetTitle = street.StreetTitle;
            }

            return streetTitle;
        }

        public static string AddressHouseComboBoxSelectedValueInitialization()
        {
            string houseNumber = "";

            foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseNumber) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{SectionsInstance.SectionsBinding.AddressCityTitle}' AND AddressStreetId = StreetId AND StreetTitle = '{SectionsInstance.SectionsBinding.AddressStreetTitle}' AND AddressHouseId = HouseId AND AddressId = {SectionsInstance.SectionsBinding.UserAddressId}"))
            {
                houseNumber = house.HouseNumber;
            }

            return houseNumber;
        }

        public static string SchoolCityComboBoxSelectedValueInitialization()
        {
            string schoolCityTitle = "";

            if(SectionsInstance.SectionsBinding.UserSchoolId != null)
            {
                foreach (var school in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityTitle) FROM Schools, Cities WHERE SchoolCityId = CityId AND SchoolId = {SectionsInstance.SectionsBinding.UserSchoolId}"))
                {
                    schoolCityTitle = school.CityTitle;
                }

                return schoolCityTitle;
            }

            else
            {
                return "";
            }
        }

        public static string SchoolTypeComboBoxSelectedValueInitialization()
        {
            string schoolType = "";

            if (SectionsInstance.SectionsBinding.UserSchoolId != null)
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolType) FROM Schools, Cities WHERE SchoolCityId = CityId AND SchoolId = {SectionsInstance.SectionsBinding.UserSchoolId}"))
                {
                    schoolType = school.SchoolType;
                }

                return schoolType;
            }

            else
            {
                return "";
            }
        }

        public static string SchoolTitleComboBoxSelectedValueInitialization()
        {
            string schoolTitle = "";

            if (SectionsInstance.SectionsBinding.UserSchoolId != null)
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolTitle) FROM Schools, Cities WHERE SchoolCityId = CityId AND SchoolId = {SectionsInstance.SectionsBinding.UserSchoolId}"))
                {
                    schoolTitle = school.SchoolTitle;
                }

                return schoolTitle;
            }

            else
            {
                return "";
            }
        }

        public static string UniversityCityComboBoxSelectedValueInitialization()
        {
            string universityCityTitle = "";

            if (SectionsInstance.SectionsBinding.UserUniversityId != null)
            {
                foreach (var university in DataResponseManager.CitiesJsonDataDeserialize($"SELECT DISTINCT(CityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId AND UniversityId = {SectionsInstance.SectionsBinding.UserUniversityId}"))
                {
                    universityCityTitle = university.CityTitle;
                }

                return universityCityTitle;
            }

            else
            {
                return "";
            }
        }

        public static string UniversityTypeComboBoxSelectedValueInitialization()
        {
            string universityType = "";

            if (SectionsInstance.SectionsBinding.UserUniversityId != null)
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT DISTINCT(UniversityType) FROM Universities, Cities WHERE UniversityCityId = CityId AND UniversityId = {SectionsInstance.SectionsBinding.UserUniversityId}"))
                {
                    universityType = university.UniversityType;
                }

                return universityType;
            }

            else
            {
                return "";
            }
        }

        public static string UniversityTitleComboBoxSelectedValueInitialization()
        {
            string universityTitle = "";

            if (SectionsInstance.SectionsBinding.UserUniversityId != null)
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT DISTINCT(UniversityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId AND UniversityId = {SectionsInstance.SectionsBinding.UserUniversityId}"))
                {
                    universityTitle = university.UniversityTitle;
                }

                return universityTitle;
            }

            else
            {
                return "";
            }
        }

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

        public static List<string> AddressStreetComboBoxInitialization()
        {
            List<string> streetsTitleList = new List<string>();

            streetsTitleList.Clear();

            if (SectionsInstance.SectionsBinding.AddressCityTitle == null)
            {
                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize("SELECT DISTINCT(StreetTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
                {
                    streetsTitleList.Add(street.StreetTitle);
                }
            }

            else
            {
                foreach (var street in DataResponseManager.StreetsJsonDataDeserialize($"SELECT DISTINCT(StreetTitle) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{SectionsInstance.SectionsBinding.AddressCityTitle}' AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
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

            if (SectionsInstance.SectionsBinding.AddressStreetTitle == null)
            {
                foreach (var house in DataResponseManager.HousesJsonDataDeserialize("SELECT DISTINCT(HouseNumber) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND AddressStreetId = StreetId AND AddressHouseId = HouseId"))
                {
                    housesNumbersList.Add(house.HouseNumber);
                }
            }

            else
            {
                foreach (var house in DataResponseManager.HousesJsonDataDeserialize($"SELECT DISTINCT(HouseNumber) FROM Addresses, Cities, Streets, Houses WHERE AddressCityId = CityId AND CityTitle = '{SectionsInstance.SectionsBinding.AddressCityTitle}' AND AddressStreetId = StreetId AND StreetTitle = '{SectionsInstance.SectionsBinding.AddressStreetTitle}' AND AddressHouseId = HouseId"))
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

            if (SectionsInstance.SectionsBinding.SchoolCityTitle == null)
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize("SELECT DISTINCT(SchoolType) FROM Schools, Cities WHERE SchoolCityId = CityId"))
                {
                    schoolTypesList.Add(school.SchoolType);
                }
            }

            else
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolType) FROM Schools, Cities WHERE SchoolCityId = CityId AND CityTitle = '{SectionsInstance.SectionsBinding.SchoolCityTitle}'"))
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

            if(SectionsInstance.SectionsBinding.SchoolType == null)
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolTitle) FROM Schools, Cities WHERE SchoolCityId = CityId"))
                {
                    schoolTitlesList.Add(school.SchoolTitle);
                }
            }

            else
            {
                foreach (var school in DataResponseManager.SchoolsJsonDataDeserialize($"SELECT DISTINCT(SchoolTitle) FROM Schools, Cities WHERE SchoolCityId = CityId AND SchoolType = '{SectionsInstance.SectionsBinding.SchoolType}' AND CityTitle = '{SectionsInstance.SectionsBinding.SchoolCityTitle}'"))
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

            if(SectionsInstance.SectionsBinding.UniversityCityTitle == null)
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize("SELECT DISTINCT(UniversityType) FROM Universities, Cities WHERE UniversityCityId = CityId"))
                {
                    universityTypesList.Add(university.UniversityType);
                }
            }

            else
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT DISTINCT(UniversityType) FROM Universities, Cities WHERE UniversityCityId = CityId AND CityTitle = '{SectionsInstance.SectionsBinding.UniversityCityTitle}'"))
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

            if(SectionsInstance.SectionsBinding.UniversityType == null)
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize("SELECT DISTINCT(UniversityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId"))
                {
                    universityTitlesList.Add(university.UniversityTitle);
                }
            }

            else
            {
                foreach (var university in DataResponseManager.UniversitiesJsonDataDeserialize($"SELECT DISTINCT(UniversityTitle) FROM Universities, Cities WHERE UniversityCityId = CityId AND UniversityType = '{SectionsInstance.SectionsBinding.UniversityType}' AND CityTitle = '{SectionsInstance.SectionsBinding.UniversityCityTitle}'"))
                {
                    universityTitlesList.Add(university.UniversityTitle);
                }
            }

            return universityTitlesList;
        }
    }
}