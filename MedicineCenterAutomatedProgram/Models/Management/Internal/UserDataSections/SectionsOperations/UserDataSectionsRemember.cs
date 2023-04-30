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
                    PatientId = patient.PatientId,
                    PatientName = patient.PatientName,
                    PatientSurname = patient.PatientSurname,
                    PatientPatronymic = patient.PatientPatronymic,
                    PatientDateOfBirth = patient.PatientDateOfBirth,
                    PatientGender = patient.PatientGender,
                    PatientAddressId = patient.PatientAddressId,
                    PatientSchoolId = patient.PatientSchoolId,
                    PatientUniversityId = patient.PatientUniversityId,
                    PatientUniversityStartEducationYear = patient.PatientUniversityStartEducationYear,
                    PatientUniversityEndEducationYear = patient.PatientUniversityEndEducationYear,
                    PatientLogin = patient.PatientLogin,
                    PatientPassword = patient.PatientPassword
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
                    DoctorId = doctor.DoctorId,
                    DoctorName = doctor.DoctorName,
                    DoctorSurname = doctor.DoctorSurname,
                    DoctorPatronymic = doctor.DoctorPatronymic,
                    DoctorDateOfBirth = doctor.DoctorDateOfBirth,
                    DoctorGender = doctor.DoctorGender,
                    DoctorAddressId = doctor.DoctorAddressId,
                    DoctorSchoolId = doctor.DoctorSchoolId,
                    DoctorUniversityId = doctor.DoctorUniversityId,
                    DoctorUniversityStartEducationYear = doctor.DoctorUniversityStartEducationYear,
                    DoctorUniversityEndEducationYear = doctor.DoctorUniversityEndEducationYear,
                    DoctorLogin = doctor.DoctorLogin,
                    DoctorPassword = doctor.DoctorPassword
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