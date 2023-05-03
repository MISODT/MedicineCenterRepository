using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System;
using System.IO;
using System.Text.Json;

namespace MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations
{
    public class UserDataSectionsRemember
    {
        public static string RememberUserDataConfigPath()
        {
            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string configFileName = "user_config.json";

            string configFilePath = desktopFolderPath + @"\" + configFileName;

            return configFilePath;
        }

        public static bool RememberUserDataConfigExists()
        {
            if (File.Exists(RememberUserDataConfigPath()))
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
            File.Delete(RememberUserDataConfigPath());
        }

        public static void RememberUserDataSeal(Patients patient, Doctors doctor)
        {
            string jsonValueString = "";

            if (patient != null)
            {
                jsonValueString = JsonSerializer.Serialize(patient);
            }

            if(doctor != null)
            {
                jsonValueString = JsonSerializer.Serialize(doctor);
            }

            File.WriteAllText(RememberUserDataConfigPath(), jsonValueString);

            File.SetAttributes(RememberUserDataConfigPath(), FileAttributes.Hidden);
        }

        public static Patients? RememberUserDataPatientUnseal()
        {
            if (RememberUserDataConfigExists())
            {
                string configFileText = File.ReadAllText(RememberUserDataConfigPath());

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

        public static Doctors? RememberUserDataDoctorUnseal()
        {
            if (RememberUserDataConfigExists())
            {
                string colfigFileText = File.ReadAllText(RememberUserDataConfigPath());

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
    }
}