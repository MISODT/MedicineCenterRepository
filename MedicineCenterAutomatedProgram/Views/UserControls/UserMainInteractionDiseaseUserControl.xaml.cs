using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.UserControls
{
    public partial class UserMainInteractionDiseaseUserControl : UserControl
    {
        private bool userMainInteractionDiseaseUserControlAdvancedDiseaseInfoGridIsVisible = true;

        public UserMainInteractionDiseaseUserControl(Diseases disease, Symptoms symptom, HealingDirections healingDirection)
        {
            InitializeComponent();


            UserMainInteractionDiseaseUserControlTitleTextBlock.Text = $"Болезнь: {disease.DiseaseTitle}";


            UserMainInteractionDiseaseUserControlHealingDirectionTitleTextBlock.Text = $"Лечением занимается Врач {healingDirection.HealingDirectionTitle}";


            UserMainInteractionDiseaseUserControlDescriptionTextBlock.Text = disease.DiseaseDescription;


            UserMainInteractionDiseaseUserControlSymptomValueTextBlock.Text = $"При болезни присутствуют следующие симптомы: {symptom.SymptomValue}.";
        }

        private void UserMainInteractionDiseaseUserControlAdvancedDiseaseInfoGridVisibilityOptions()
        {
            if (!userMainInteractionDiseaseUserControlAdvancedDiseaseInfoGridIsVisible)
            {
                UserMainInteractionDiseaseUserControlAdvancedDiseaseInfoGrid.Visibility = Visibility.Collapsed;

                userMainInteractionDiseaseUserControlAdvancedDiseaseInfoGridIsVisible = true;
            }

            else
            {
                UserMainInteractionDiseaseUserControlAdvancedDiseaseInfoGrid.Visibility = Visibility.Visible;

                userMainInteractionDiseaseUserControlAdvancedDiseaseInfoGridIsVisible = false;
            }
        }

        private void UserMainInteractionDiseaseUserControl_Loaded(object sender, RoutedEventArgs e) => UserMainInteractionDiseaseUserControlAdvancedDiseaseInfoGrid.Visibility = Visibility.Collapsed;

        private void UserMainInteractionDiseaseUserControlInfoButton_Click(object sender, RoutedEventArgs e) => UserMainInteractionDiseaseUserControlAdvancedDiseaseInfoGridVisibilityOptions();
    }
}