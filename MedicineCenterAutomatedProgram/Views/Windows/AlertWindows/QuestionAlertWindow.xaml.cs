using System.Windows;

namespace MedicineCenterAutomatedProgram.Views.Windows.AlertWindows
{
    public partial class QuestionAlertWindow : Window
    {
        public string questionAlertWindowAnswerValue = "";

        public QuestionAlertWindow()
        {
            InitializeComponent();
        }

        private void QuestionAlertWindowYesButton_Click(object sender, RoutedEventArgs e)
        {
            questionAlertWindowAnswerValue = "Да";


            Close();


            Application.Current.MainWindow.Opacity = 1;
        }

        private void QuestionAlertWindowNoButton_Click(object sender, RoutedEventArgs e)
        {
            questionAlertWindowAnswerValue = "Нет";


            Close();


            Application.Current.MainWindow.Opacity = 1;
        }

        private void QuestionAlertWindow_Closed(object sender, System.EventArgs e) 
        {
            Application.Current.MainWindow.Opacity = 1;


            if(questionAlertWindowAnswerValue != "Да")
            {
                questionAlertWindowAnswerValue = "Нет";
            }
        }
    }
}