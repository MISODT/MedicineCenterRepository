using System.Windows;

namespace MedicineCenterAutomatedProgram.Views.Windows.AlertWindows
{
    public partial class ErrorAlertWindow : Window
    {
        public ErrorAlertWindow(string errorAlertWindowText)
        {
            InitializeComponent();


            ErrorAlertWindowTextBlock.Text = errorAlertWindowText;
        }

        private void ErrorAlertWindowOKButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Opacity = 1;


            Close();
        }

        private void ErrorAlertWindow_Closed(object sender, System.EventArgs e) => Application.Current.MainWindow.Opacity = 1;
    }
}