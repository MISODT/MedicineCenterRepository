﻿using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System.Collections.Generic;
using System.Text.Json;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData
{
    public class DataResponseManager
    {
        public static List<Symptoms> SymptomsJsonDataDeserialize(string symptomsDataQuery)
        {
            var symptomsDataList = JsonSerializer.Deserialize<List<Symptoms>>(WebResponseManager.GetJsonResponseFromRequestQuery(symptomsDataQuery));

            return symptomsDataList;
        }

        public static List<Diseases> DiseasesJsonDataDeserialize(string diseasesDataQuery)
        {
            var diseasesDataList = JsonSerializer.Deserialize<List<Diseases>>(WebResponseManager.GetJsonResponseFromRequestQuery(diseasesDataQuery));

            return diseasesDataList;
        }

        public static List<HospitalAddresses> HospitalAddressesJsonDataDeserialize(string hospitalAddressesDataQuery)
        {
            var hospitalAddressesDataList = JsonSerializer.Deserialize<List<HospitalAddresses>>(WebResponseManager.GetJsonResponseFromRequestQuery(hospitalAddressesDataQuery));

            return hospitalAddressesDataList;
        }

        public static List<Appointments> AppointmentsJsonDataDeserialize(string appointmentsDataQuery)
        {
            var appointmentsDataList = JsonSerializer.Deserialize<List<Appointments>>(WebResponseManager.GetJsonResponseFromRequestQuery(appointmentsDataQuery));

            return appointmentsDataList;
        }

        public static List<Shifts> ShiftsJsonDataDeserialize(string shiftsDataQuery)
        {
            var shiftsDataList = JsonSerializer.Deserialize<List<Shifts>>(WebResponseManager.GetJsonResponseFromRequestQuery(shiftsDataQuery));

            return shiftsDataList;
        }

        public static List<HealingDirections> HealingDirectionsJsonDataDeserialize(string healingDirectionsDataQuery)
        {
            var healingDirectionsDataList = JsonSerializer.Deserialize<List<HealingDirections>>(WebResponseManager.GetJsonResponseFromRequestQuery(healingDirectionsDataQuery));

            return healingDirectionsDataList;
        }

        public static List<Patients> PatientsJsonDataDeserialize(string patientDataQuery)
        {
            var patientsDataList = JsonSerializer.Deserialize<List<Patients>>(WebResponseManager.GetJsonResponseFromRequestQuery(patientDataQuery));

            return patientsDataList;
        }

        public static List<Doctors> DoctorsJsonDataDeserialize(string doctorDataQuery)
        {
            var doctorsDataList = JsonSerializer.Deserialize<List<Doctors>>(WebResponseManager.GetJsonResponseFromRequestQuery(doctorDataQuery));

            return doctorsDataList;
        }

        public static List<MailDomains> MailDomainsJsonDataDeserialize(string mailDomainDataQuery)
        {
            var mailDomainsDataList = JsonSerializer.Deserialize<List<MailDomains>>(WebResponseManager.GetJsonResponseFromRequestQuery(mailDomainDataQuery));

            return mailDomainsDataList;
        }

        public static List<Cities> CitiesJsonDataDeserialize(string cityDataQuery)
        {
            var citiesDataList = JsonSerializer.Deserialize<List<Cities>>(WebResponseManager.GetJsonResponseFromRequestQuery(cityDataQuery));

            return citiesDataList;
        }

        public static List<Streets> StreetsJsonDataDeserialize(string streetDataQuery)
        {
            var streetsDataList = JsonSerializer.Deserialize<List<Streets>>(WebResponseManager.GetJsonResponseFromRequestQuery(streetDataQuery));

            return streetsDataList;
        }

        public static List<Houses> HousesJsonDataDeserialize(string houseDataQuery)
        {
            var housesDataList = JsonSerializer.Deserialize<List<Houses>>(WebResponseManager.GetJsonResponseFromRequestQuery(houseDataQuery));

            return housesDataList;
        }

        public static List<Addresses> AddressesJsonDataDeserialize(string addressDataQuery)
        {
            var addressesDataList = JsonSerializer.Deserialize<List<Addresses>>(WebResponseManager.GetJsonResponseFromRequestQuery(addressDataQuery));

            return addressesDataList;
        }

        public static List<Schools> SchoolsJsonDataDeserialize(string schoolDataQuery)
        {
            var schoolsDataList = JsonSerializer.Deserialize<List<Schools>>(WebResponseManager.GetJsonResponseFromRequestQuery(schoolDataQuery));

            return schoolsDataList;
        }

        public static List<Universities> UniversitiesJsonDataDeserialize(string universityDataQuery)
        {
            var universitiesDataList = JsonSerializer.Deserialize<List<Universities>>(WebResponseManager.GetJsonResponseFromRequestQuery(universityDataQuery));

            return universitiesDataList;
        }

        public static List<MedicineCards> MedicineCardsJsonDataDeserialize(string medicineCardsDataQuery)
        {
            var medicineCardsDataList = JsonSerializer.Deserialize<List<MedicineCards>>(WebResponseManager.GetJsonResponseFromRequestQuery(medicineCardsDataQuery));

            return medicineCardsDataList;
        }

        public static List<MedicineCardRecords> MedicineCardRecordsJsonDataDeserialize(string medicineCardRecordsDataQuery)
        {
            var medicineCardRecordsDataList = JsonSerializer.Deserialize<List<MedicineCardRecords>>(WebResponseManager.GetJsonResponseFromRequestQuery(medicineCardRecordsDataQuery));

            return medicineCardRecordsDataList;
        }
    }
}