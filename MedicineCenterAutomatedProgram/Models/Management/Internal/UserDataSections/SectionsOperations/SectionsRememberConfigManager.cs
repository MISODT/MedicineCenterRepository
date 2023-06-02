using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System;
using System.IO;
using System.Text.Json;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations
{
    public class SectionsRememberConfigManager
    {
        public static string GetRememberConfigPath(string configFileName)
        {
            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string configFilePath = desktopFolderPath + @"\" + configFileName;

            return configFilePath;
        }

        public static bool IsRememberConfigExists()
        {
            if (File.Exists(GetRememberConfigPath("patient_config.json")) || File.Exists(GetRememberConfigPath("doctor_config.json")))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static void RemoveRememberConfig()
        {
            if (IsRememberConfigExists())
            {
                File.Delete(GetRememberConfigPath("patient_config.json"));

                File.Delete(GetRememberConfigPath("doctor_config.json"));
            }
        }

        public static void SealToRememberConfig(Patients patient, Doctors doctor)
        {
            string jsonValueString = "";

            if (patient != null)
            {
                jsonValueString = JsonSerializer.Serialize(patient);

                File.WriteAllText(GetRememberConfigPath("patient_config.json"), jsonValueString);

                File.SetAttributes(GetRememberConfigPath("patient_config.json"), FileAttributes.Hidden);
            }

            if(doctor != null)
            {
                jsonValueString = JsonSerializer.Serialize(doctor);

                File.WriteAllText(GetRememberConfigPath("doctor_config.json"), jsonValueString);

                File.SetAttributes(GetRememberConfigPath("doctor_config.json"), FileAttributes.Hidden);
            }
        }

        public static Patients? UnsealPatientFromRememberConfig()
        {
            if (IsRememberConfigExists())
            {
                if(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}" + @"\" + "patient_config.json"))
                {
                    string configFileText = File.ReadAllText(GetRememberConfigPath("patient_config.json"));

                    Patients patient = JsonSerializer.Deserialize<Patients>(configFileText);

                    SectionsInstance.Patient = new Patients()
                    {
                        Id = patient.Id,
                        ProfilePhotoUri = patient.ProfilePhotoUri,
                        Name = patient.Name,
                        Surname = patient.Surname,
                        Patronymic = patient.Patronymic,
                        DateOfBirth = patient.DateOfBirth,
                        Gender = patient.Gender,
                        AddressId = patient.AddressId,
                        SchoolId = patient.SchoolId,
                        UniversityId = patient.UniversityId,
                        UniversityStartEducationYear = patient.UniversityStartEducationYear,
                        UniversityEndEducationYear = patient.UniversityEndEducationYear,
                        Login = patient.Login,
                        Password = patient.Password
                    };

                    return SectionsInstance.Patient;
                }

                else
                {
                    return null;
                }
            }

            else
            {
                return null;
            }
        }

        public static Doctors? UnsealDoctorFromRememberConfig()
        {
            if (IsRememberConfigExists())
            {
                if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}" + @"\" + "doctor_config.json"))
                {
                    string colfigFileText = File.ReadAllText(GetRememberConfigPath("doctor_config.json"));

                    Doctors doctor = JsonSerializer.Deserialize<Doctors>(colfigFileText);

                    SectionsInstance.Doctor = new Doctors()
                    {
                        Id = doctor.Id,
                        ProfilePhotoUri = doctor.ProfilePhotoUri,
                        Name = doctor.Name,
                        Surname = doctor.Surname,
                        Patronymic = doctor.Patronymic,
                        DateOfBirth = doctor.DateOfBirth,
                        Gender = doctor.Gender,
                        AddressId = doctor.AddressId,
                        SchoolId = doctor.SchoolId,
                        UniversityId = doctor.UniversityId,
                        UniversityStartEducationYear = doctor.UniversityStartEducationYear,
                        UniversityEndEducationYear = doctor.UniversityEndEducationYear,
                        Login = doctor.Login,
                        Password = doctor.Password
                    };

                    return SectionsInstance.Doctor;
                }

                else
                {
                    return null;
                }
            }

            else
            {
                return null;
            }
        }
    }
}