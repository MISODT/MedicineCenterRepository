﻿using MedicineCenterAutomatedProgram.Models.Management;
using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataOperations;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionShift;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionHomePage : Page
    {
        public UserMainInteractionHomePage(Patients patient, Doctors doctor)
        {
            InitializeComponent();

            if(patient != null)
            {
                DataContext = patient;


                if (patient.ProfilePhotoUri != "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png")
                {
                    UserDataProfilePhotoImage.ImageSource = ByteImageValuesManager.GetImageFromBytes(patient.ProfilePhotoUri);
                }


                MainInteractionHomeSectionsDoctorReceiving.Visibility = Visibility.Hidden;



                MainInteractionHomeSectionsDoctorGrid.Visibility = Visibility.Hidden;


                MainInteractionHomeSectionPatientMedicineCardsButton.Visibility = Visibility.Collapsed;
            }

            if(doctor != null)
            {
                DataContext = doctor;


                if (doctor.ProfilePhotoUri != "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png")
                {
                    UserDataProfilePhotoImage.ImageSource = ByteImageValuesManager.GetImageFromBytes(doctor.ProfilePhotoUri);
                }


                MainInteractionHomeSectionsDoctorReceiving.Visibility = Visibility.Visible;



                MainInteractionHomeSectionsDoctorGrid.Visibility = Visibility.Visible;


                MainInteractionHomeSectionPatientMedicineCardsButton.Visibility = Visibility.Visible;
            }
        }

        private void SetStartupFramePage()
        {
            FrameManager.HomeFrame = UserMainInteractionHomePageFrame;

            if(SectionsInstance.Patient != null)
            {
                FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));
            }

            if(SectionsInstance.Doctor != null)
            {
                FrameManager.HomeFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));
            }
        }

        private void UserMainInteractionHomePage_Loaded(object sender, RoutedEventArgs e)
        {
            SetStartupFramePage();

            if (SectionsInstance.Patient != null)
            {
                if (!File.Exists(SectionsInstance.Patient.ProfilePhotoUri))
                {
                    SectionsInstance.SectionsBinding.UserProfilePhotoUri = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
                }
            }

            if (SectionsInstance.Doctor != null)
            {
                if (!File.Exists(SectionsInstance.Doctor.ProfilePhotoUri))
                {
                    SectionsInstance.SectionsBinding.UserProfilePhotoUri = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
                }
            }

            SectionsOperationsManager.NewMedicineCardOperation();
        }

        private void UserDataEditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if(SectionsInstance.Patient != null)
            {
                string patientSchoolId = "";

                string patientUniversityId = "";

                if(SectionsInstance.Patient.SchoolId == null)
                {
                    patientSchoolId = "NULL";
                }

                else
                {
                    patientSchoolId = SectionsInstance.Patient.SchoolId;
                }

                if(SectionsInstance.Patient.UniversityId == null)
                {
                   patientUniversityId = "NULL";
                }

                else
                {
                    patientUniversityId = SectionsInstance.Patient.UniversityId;
                }

                SectionsInstance.SectionsBinding = new SectionsBindingManager()
                {
                    UserPositionIsDoctor = true,
                    UserId = SectionsInstance.Patient.Id,
                    UserProfilePhotoUri = SectionsInstance.Patient.ProfilePhotoUri,
                    UserName = SectionsInstance.Patient.Name,
                    UserSurname = SectionsInstance.Patient.Surname,
                    UserPatronymic = SectionsInstance.Patient.Patronymic,
                    UserDateOfBirth = SectionsInstance.Patient.DateOfBirth,
                    UserGender = SectionsInstance.Patient.Gender,
                    UserAddressId = SectionsInstance.Patient.AddressId,
                    UserSchoolId = patientSchoolId,
                    UserUniversityId = patientUniversityId,
                    UserUniversityStartEducationYear = Convert.ToInt32(SectionsInstance.Patient.UniversityStartEducationYear),
                    UserUniversityEndEducationYear = Convert.ToInt32(SectionsInstance.Patient.UniversityEndEducationYear),
                    UserLogin = SectionsInstance.Patient.Login,
                    UserPassword = SectionsInstance.Patient.Password
                };
            }

            if (SectionsInstance.Doctor != null)
            {
                string doctorSchoolId = "";

                string doctorUniversityId = "";

                if (SectionsInstance.Doctor.SchoolId == null)
                {
                    doctorSchoolId = "NULL";
                }

                else
                {
                    doctorSchoolId = SectionsInstance.Doctor.SchoolId;
                }

                if (SectionsInstance.Doctor.UniversityId == null)
                {
                    doctorUniversityId = "NULL";
                }

                else
                {
                    doctorUniversityId = SectionsInstance.Doctor.UniversityId;
                }

                SectionsInstance.SectionsBinding = new SectionsBindingManager()
                {
                    UserPositionIsDoctor = true,
                    UserId = SectionsInstance.Doctor.Id,
                    UserProfilePhotoUri = SectionsInstance.Doctor.ProfilePhotoUri,
                    UserName = SectionsInstance.Doctor.Name,
                    UserSurname = SectionsInstance.Doctor.Surname,
                    UserPatronymic = SectionsInstance.Doctor.Patronymic,
                    UserDateOfBirth = SectionsInstance.Doctor.DateOfBirth,
                    UserGender = SectionsInstance.Doctor.Gender,
                    UserAddressId = SectionsInstance.Doctor.AddressId,
                    UserSchoolId = doctorSchoolId,
                    UserUniversityId = doctorUniversityId,
                    UserUniversityStartEducationYear = Convert.ToInt32(SectionsInstance.Doctor.UniversityStartEducationYear),
                    UserUniversityEndEducationYear = Convert.ToInt32(SectionsInstance.Doctor.UniversityEndEducationYear),
                    UserLogin = SectionsInstance.Doctor.Login,
                    UserPassword = SectionsInstance.Doctor.Password
                };
            }

            FrameManager.MainWindowFrame.Navigate(new UserMainInteractionProfilePage(SectionsInstance.SectionsBinding));
        }

        private void UserDataOutProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SectionsRememberConfigManager.RemoveRememberConfig();

            FrameManager.MainWindowFrame.Navigate(new WelcomePage());
        }

        private void MainInteractionHomeSectionNewAppointmentButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentOperationsPage(null));

        private void MainInteractionHomeSectionMyAppointmentsButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));

        private void MainInteractionHomeSectionAppointmentsHistoryButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsPage("Старые"));

        private void MainInteractionHomeSectionMyMedicineCardButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionMedicineCard("Моя"));

        private void MainInteractionHomeSectionPatientMedicineCardsButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionMedicineCard("Пациентов"));

        private void MainInteractionHomeSectionSymptomHelperButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionSymptomHelperPage());

        private void MainInteractionHomeSectionAppointmentReceivingButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionAppointmentsReceivingPage());

        private void MainInteractionHomeSectionNewShiftButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionShiftOperationsPage(null));

        private void MainInteractionHomeSectionMyShiftsButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionShiftsPage("Текущие"));

        private void MainInteractionHomeSectionShiftsHistoryButton_Click(object sender, RoutedEventArgs e) => FrameManager.HomeFrame.Navigate(new UserMainInteractionShiftsPage("Старые"));
    }
}