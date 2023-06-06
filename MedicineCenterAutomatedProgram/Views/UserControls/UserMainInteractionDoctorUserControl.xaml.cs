using MedicineCenterAutomatedProgram.Models.Management;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using NPOI.POIFS.Properties;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionDoctorUserControl : UserControl
    {
        public UserMainInteractionDoctorUserControl(Doctors doctor, HealingDirections healingDirection)
        {
            InitializeComponent();


            DataContext = doctor;


            if (doctor.ProfilePhotoUri != "/Resources/DefaultImages/DefaultUserDataProfilePhotoImage.png")
            {
                UserDataProfilePhoto.ImageSource = ByteImageValuesManager.GetImageFromBytes(doctor.ProfilePhotoUri);
            }

            else
            {
                Uri userImageUri = new Uri(doctor.ProfilePhotoUri, UriKind.RelativeOrAbsolute);

                UserDataProfilePhoto.ImageSource = new BitmapImage(userImageUri);
            }


            UserDataHealingDirectionTitleTextBlock.Text = healingDirection.HealingDirectionTitle;
        }
    }
}