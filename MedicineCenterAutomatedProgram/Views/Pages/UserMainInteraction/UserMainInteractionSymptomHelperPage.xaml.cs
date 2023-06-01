using MedicineCenterAutomatedProgram.Models.Management.External;
using MedicineCenterAutomatedProgram.Models.Management.Internal.ReceivingData;
using MedicineCenterAutomatedProgram.Models.Management.Internal.UserDataSections.Sections;
using MedicineCenterAutomatedProgram.Views.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MedicineCenterAutomatedProgram.Views.Pages.UserMainInteraction
{
    public partial class UserMainInteractionSymptomHelperPage : Page
    {
        private ObservableCollection<UserMainInteractionDiseaseUserControl> userMainInteractionSymptomHelperStartDiseaseUserControlCollection = new ObservableCollection<UserMainInteractionDiseaseUserControl>();

        private ObservableCollection<UserMainInteractionDiseaseUserControl> userMainInteractionSymptomHelperRunDiseaseUserControlCollection = new ObservableCollection<UserMainInteractionDiseaseUserControl>();


        private List<Diseases> diseasesList = new List<Diseases>();

        private List<HealingDirections> healingDirectionsList = new List<HealingDirections>();

        private List<Symptoms> symptomsList = new List<Symptoms>();

        public UserMainInteractionSymptomHelperPage()
        {
            InitializeComponent();
        }

        private void UserMainInteractionSymptomHelperItemsEmptyHandler()
        {
            if (userMainInteractionSymptomHelperRunDiseaseUserControlCollection.Count == 0)
            {
                UserMainInteractionSymptomHelperItemsEmptyTextBlock.Visibility = Visibility.Visible;
            }

            else
            {
                UserMainInteractionSymptomHelperItemsEmptyTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void UserMainInteractionUserMainInteractionSymptomHelperStartItemsControlInitialization()
        {
            foreach (var disease in DataResponseManager.DiseasesJsonDataDeserialize("SELECT DiseaseId, DiseaseTitle, DiseaseDescription, DiseaseHealingDirectionId, DiseaseSymptomId FROM Diseases"))
            {
                foreach (var healingDirection in DataResponseManager.HealingDirectionsJsonDataDeserialize($"SELECT HealingDirectionId, HealingDirectionTitle FROM HealingDirections, Diseases WHERE HealingDirectionId = DiseaseHealingDirectionId AND DiseaseId = {disease.DiseaseId}"))
                {
                    foreach (var symptom in DataResponseManager.SymptomsJsonDataDeserialize($"SELECT SymptomId, SymptomValue FROM Symptoms, Diseases WHERE SymptomId = DiseaseSymptomId AND DiseaseId = {disease.DiseaseId}"))
                    {
                        userMainInteractionSymptomHelperStartDiseaseUserControlCollection.Add(new UserMainInteractionDiseaseUserControl(disease, symptom, healingDirection));

                        symptomsList.Add(symptom);
                    }

                    healingDirectionsList.Add(healingDirection);
                }

                diseasesList.Add(disease);
            }
        }

        private void UserMainInteractionUserMainInteractionSymptomHelperRunItemsControlInitialization()
        {
            foreach(var disease in diseasesList)
            {
                foreach (var healingDirection in healingDirectionsList)
                {
                    foreach (var symptom in symptomsList)
                    {
                        if(disease.DiseaseHealingDirectionId == healingDirection.HealingDirectionId && disease.DiseaseSymptomId == symptom.SymptomId)
                        {
                            if (disease.DiseaseTitle.ToLower().Contains(UserMainInteractionSymptomHelperSearchTextBox.Text.ToLower()) || disease.DiseaseTitle.ToUpper().Contains(UserMainInteractionSymptomHelperSearchTextBox.Text.ToUpper()) || symptom.SymptomValue.ToLower().Contains(UserMainInteractionSymptomHelperSearchTextBox.Text.ToLower()) || symptom.SymptomValue.ToUpper().Contains(UserMainInteractionSymptomHelperSearchTextBox.Text.ToUpper()))
                            {
                                userMainInteractionSymptomHelperRunDiseaseUserControlCollection.Add(new UserMainInteractionDiseaseUserControl(disease, symptom, healingDirection));
                            }
                        }
                    }
                }
            }

            UserMainInteractionSymptomHelperItemsControl.ItemsSource = userMainInteractionSymptomHelperRunDiseaseUserControlCollection;
        }

        private void UserMainInteractionSymptomHelperPage_Loaded(object sender, RoutedEventArgs e)
        {
            UserMainInteractionUserMainInteractionSymptomHelperStartItemsControlInitialization();

            UserMainInteractionSymptomHelperItemsControl.ItemsSource = userMainInteractionSymptomHelperStartDiseaseUserControlCollection;


            UserMainInteractionSymptomHelperItemsEmptyTextBlock.Visibility = Visibility.Hidden;
        }

        private void UserMainInteractionSymptomHelperSearchTextBox_TextChanged(object sender, TextChangedEventArgs e) 
        {
            FieldsViewManager.ChangeTextBoxUnclearedView(UserMainInteractionSymptomHelperSearchTextBox, UserMainInteractionSymptomHelperSearchTextBoxHintAssist);

            if(string.IsNullOrWhiteSpace(UserMainInteractionSymptomHelperSearchTextBox.Text) && userMainInteractionSymptomHelperRunDiseaseUserControlCollection.Count == 0)
            {
                UserMainInteractionSymptomHelperItemsControl.ItemsSource = userMainInteractionSymptomHelperStartDiseaseUserControlCollection;

                UserMainInteractionSymptomHelperItemsEmptyTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void UserMainInteractionSymptomHelperSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(UserMainInteractionSymptomHelperSearchTextBox.Text))
            {
                userMainInteractionSymptomHelperRunDiseaseUserControlCollection.Clear();

                UserMainInteractionUserMainInteractionSymptomHelperRunItemsControlInitialization();

                UserMainInteractionSymptomHelperItemsEmptyHandler();
            }
        }
    }
}