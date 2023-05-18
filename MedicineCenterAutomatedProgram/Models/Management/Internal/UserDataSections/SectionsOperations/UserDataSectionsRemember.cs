using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System;
using System.IO;
using System.Text.Json;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations
{
    public class UserDataSectionsRemember
    {
        public static string RememberUserDataConfigPath(string configFileName)
        {
            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string configFilePath = desktopFolderPath + @"\" + configFileName;

            return configFilePath;
        }

        public static bool RememberUserDataConfigExists()
        {
            if (File.Exists(RememberUserDataConfigPath("patient_config.json")) || File.Exists(RememberUserDataConfigPath("doctor_config.json")))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static void RememberUserDataConfigRemove()
        {
            if (RememberUserDataConfigExists())
            {
                File.Delete(RememberUserDataConfigPath("patient_config.json"));

                File.Delete(RememberUserDataConfigPath("doctor_config.json"));
            }
        }

        public static void RememberUserDataSeal(Patients patient, Doctors doctor)
        {
            string jsonValueString = "";

            if (patient != null)
            {
                jsonValueString = JsonSerializer.Serialize(patient);

                File.WriteAllText(RememberUserDataConfigPath("patient_config.json"), jsonValueString);

                File.SetAttributes(RememberUserDataConfigPath("patient_config.json"), FileAttributes.Hidden);
            }

            if(doctor != null)
            {
                jsonValueString = JsonSerializer.Serialize(doctor);

                File.WriteAllText(RememberUserDataConfigPath("doctor_config.json"), jsonValueString);

                File.SetAttributes(RememberUserDataConfigPath("doctor_config.json"), FileAttributes.Hidden);
            }
        }

        public static Patients? RememberUserDataPatientUnseal()
        {
            if (RememberUserDataConfigExists())
            {
                if(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}" + @"\" + "patient_config.json"))
                {
                    string configFileText = File.ReadAllText(RememberUserDataConfigPath("patient_config.json"));

                    Patients patient = JsonSerializer.Deserialize<Patients>(configFileText);

                    UserDataSectionsInstance.Patient = new Patients()
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

                    return UserDataSectionsInstance.Patient;
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

        public static Doctors? RememberUserDataDoctorUnseal()
        {
            if (RememberUserDataConfigExists())
            {
                if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}" + @"\" + "doctor_config.json"))
                {
                    string colfigFileText = File.ReadAllText(RememberUserDataConfigPath("doctor_config.json"));

                    Doctors doctor = JsonSerializer.Deserialize<Doctors>(colfigFileText);

                    UserDataSectionsInstance.Doctor = new Doctors()
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

                    return UserDataSectionsInstance.Doctor;
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