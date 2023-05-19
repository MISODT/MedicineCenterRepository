using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction.UserMainInteractionAppointment;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionHomePage : Page
    {
        public UserMainInteractionHomePage(Patients patient, Doctors doctor)
        {
            InitializeComponent();

            DataContext = patient;
            DataContext = doctor;
        }

        private void SetStartupFramePage()
        {
            FrameManager.UserMainInteractionHomePageFrame = UserMainInteractionHomePageFrame;

            FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));
        }

        private void UserMainInteractionHomePage_Loaded(object sender, RoutedEventArgs e)
        {
            SetStartupFramePage();

            if (UserDataSectionsInstance.Patient != null)
            {
                if (!File.Exists(UserDataSectionsInstance.Patient.ProfilePhotoUri))
                {
                    UserDataSectionsInstance.User.UserProfilePhotoUri = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
                }
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                if (!File.Exists(UserDataSectionsInstance.Doctor.ProfilePhotoUri))
                {
                    UserDataSectionsInstance.User.UserProfilePhotoUri = "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png";
                }
            }
        }

        private void UserDataEditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserDataSectionsInstance.Patient != null)
            {
                UserDataSectionsInstance.User = new UserDataSectionsBinding()
                {
                    UserPositionIsPatient = true,
                    UserId = UserDataSectionsInstance.Patient.Id,
                    UserProfilePhotoUri = UserDataSectionsInstance.Patient.ProfilePhotoUri,
                    UserName = UserDataSectionsInstance.Patient.Name,
                    UserSurname = UserDataSectionsInstance.Patient.Surname,
                    UserPatronymic = UserDataSectionsInstance.Patient.Patronymic,
                    UserDateOfBirth = UserDataSectionsInstance.Patient.DateOfBirth,
                    UserGender = UserDataSectionsInstance.Patient.Gender,
                    UserAddressId = UserDataSectionsInstance.Patient.AddressId,
                    UserSchoolId = UserDataSectionsInstance.Patient.SchoolId,
                    UserUniversityId = UserDataSectionsInstance.Patient.UniversityId,
                    UserUniversityStartEducationYear = Convert.ToInt32(UserDataSectionsInstance.Patient.UniversityStartEducationYear),
                    UserUniversityEndEducationYear = Convert.ToInt32(UserDataSectionsInstance.Patient.UniversityEndEducationYear),
                    UserLogin = UserDataSectionsInstance.Patient.Login,
                    UserPassword = UserDataSectionsInstance.Patient.Password
                };
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                UserDataSectionsInstance.User = new UserDataSectionsBinding()
                {
                    UserPositionIsDoctor = true,
                    UserId = UserDataSectionsInstance.Doctor.Id,
                    UserProfilePhotoUri = UserDataSectionsInstance.Doctor.ProfilePhotoUri,
                    UserName = UserDataSectionsInstance.Doctor.Name,
                    UserSurname = UserDataSectionsInstance.Doctor.Surname,
                    UserPatronymic = UserDataSectionsInstance.Doctor.Patronymic,
                    UserDateOfBirth = UserDataSectionsInstance.Doctor.DateOfBirth,
                    UserGender = UserDataSectionsInstance.Doctor.Gender,
                    UserAddressId = UserDataSectionsInstance.Doctor.AddressId,
                    UserSchoolId = UserDataSectionsInstance.Doctor.SchoolId,
                    UserUniversityId = UserDataSectionsInstance.Doctor.UniversityId,
                    UserUniversityStartEducationYear = Convert.ToInt32(UserDataSectionsInstance.Doctor.UniversityStartEducationYear),
                    UserUniversityEndEducationYear = Convert.ToInt32(UserDataSectionsInstance.Doctor.UniversityEndEducationYear),
                    UserLogin = UserDataSectionsInstance.Doctor.Login,
                    UserPassword = UserDataSectionsInstance.Doctor.Password
                };
            }

            FrameManager.MainWindowFrame.Navigate(new UserMainInteractionProfilePage(UserDataSectionsInstance.User));
        }

        private void UserDataOutProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsRemember.RememberUserDataConfigRemove();

            FrameManager.MainWindowFrame.Navigate(new WelcomePage());
        }

        private void MainInteractionHomeSectionNewAppointmentButton_Click(object sender, RoutedEventArgs e) => FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionNewAppointmentPage());

        private void MainInteractionHomeSectionMyAppointmentsButton_Click(object sender, RoutedEventArgs e) => FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionAppointmentsPage("Текущие"));

        private void MainInteractionHomeSectionAppointmentsHistoryButton_Click(object sender, RoutedEventArgs e) => FrameManager.UserMainInteractionHomePageFrame.Navigate(new UserMainInteractionAppointmentsPage("Старые"));

        private void MainInteractionHomeSectionNewShiftButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainInteractionHomeSectionMyShiftsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainInteractionHomeSectionShiftsHistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}