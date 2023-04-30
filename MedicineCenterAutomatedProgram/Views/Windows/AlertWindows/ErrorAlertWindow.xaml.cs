using System.Windows;

namespace MedicineCenterAutomatedProgram.Views.Windows.AlertWindows
{
    public partial class ErrorAlertWindow : Window
    {
        public ErrorAlertWindow(string ErrorAlertWindowText)
        {
            InitializeComponent();

            ErrorAlertWindowTextBlock.Text = ErrorAlertWindowText;
        }

        private void ErrorAlertWindowOKButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}