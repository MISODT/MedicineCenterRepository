using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.SectionsOperations;
using MedicineCenterAutomatedProgram.Views.Pages;
using MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MedicineCenterAutomatedProgram
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetStartupFramePage();
        }

        private void HeaderGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }

            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetStartupFramePage()
        {
            FrameManager.MainWindowFrame = MainWindowFrame;

            if (UserDataSectionsRemember.RememberUserDataConfigExists())
            {
                FrameManager.MainWindowFrame.Navigate(new UserMainInteractionHomePage(UserDataSectionsRemember.RememberUserDataPatientUnseal(), UserDataSectionsRemember.RememberUserDataDoctorUnseal()));
            }

            else
            {
                MainWindowFrame.Navigate(new WelcomePage());
            }
        }
    }
}