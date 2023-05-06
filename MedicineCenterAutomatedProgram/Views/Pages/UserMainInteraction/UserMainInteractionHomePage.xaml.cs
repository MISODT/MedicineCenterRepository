using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
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

        private void UserMainInteractionHomePage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserDataProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserDataSectionsInstance.Patient != null)
            {
                UserDataSectionsInstance.User = new UserDataSectionsBinding()
                {
                    UserId = UserDataSectionsInstance.Patient.Id,
                    UserProfilePhotoUri = UserDataSectionsInstance.Patient.ProfilePhotoUri,
                    UserName = UserDataSectionsInstance.Patient.Name,
                    UserSurname = UserDataSectionsInstance.Patient.Surname,
                    UserPatronymic = UserDataSectionsInstance.Patient.Patronymic,
                    UserGender = UserDataSectionsInstance.Patient.Gender,
                    UserAddressId = UserDataSectionsInstance.Patient.AddressId
                };
            }

            if (UserDataSectionsInstance.Doctor != null)
            {
                UserDataSectionsInstance.User = new UserDataSectionsBinding()
                {
                    UserId = UserDataSectionsInstance.Doctor.Id,
                    UserProfilePhotoUri = UserDataSectionsInstance.Doctor.ProfilePhotoUri,
                    UserName = UserDataSectionsInstance.Doctor.Name,
                    UserSurname = UserDataSectionsInstance.Doctor.Surname,
                    UserPatronymic = UserDataSectionsInstance.Doctor.Patronymic,
                    UserGender = UserDataSectionsInstance.Doctor.Gender,
                    UserAddressId = UserDataSectionsInstance.Doctor.AddressId
                };
            }

            FrameManager.MainFrame.Navigate(new UserMainInteractionProfilePage(UserDataSectionsInstance.User));
        }

        private void UserDataOutProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataSectionsRemember.RememberUserDataConfigRemove();

            FrameManager.MainFrame.Navigate(new WelcomePage());
        }
    }
}