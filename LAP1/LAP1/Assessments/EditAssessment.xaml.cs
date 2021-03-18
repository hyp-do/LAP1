using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssessment : ContentPage
    {
        private string AssessmentType;
        private Assessment currentAssessment = Globals.CurrentAssessment;
        public EditAssessment()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            
            assessmentTitle.Text = currentAssessment.AssessmentTitle;
            assessmentStartDate.Date = currentAssessment.AssessmentStartDate.Date;
            assessmentEndDate.Date = currentAssessment.AssessmentEndDate;

            if (currentAssessment.Type.Contains("OA"))
            {
                oASwitch.IsToggled = true;

            }
            else if (currentAssessment.Type.Contains("PA"))
            {

                pASwitch.IsToggled = true;
               
            }

            assessmentNotifications.IsToggled = currentAssessment.AssessmentNotification;


            base.OnAppearing();
        }

        private async void EditAssessmentSaveButton_OnClicked(object sender, EventArgs e)
        {
            var assessment = new Assessment()
            {
                AssessmentId = currentAssessment.AssessmentId,
                CourseId = currentAssessment.CourseId,
                AssessmentTitle = assessmentTitle.Text,
                AssessmentStartDate = assessmentStartDate.Date,
                AssessmentEndDate = assessmentEndDate.Date,
                Type = AssessmentType,
                AssessmentNotification = assessmentNotifications.IsToggled

            };

            var editAssessmentConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            if (oASwitch.IsToggled || pASwitch.IsToggled)
            {

                if (Validation.IsFieldNull(assessment.AssessmentTitle))
                {
                    if (assessment.AssessmentStartDate < assessment.AssessmentEndDate)
                    {
                        await editAssessmentConnection.UpdateAsync(assessment);

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "The Start Date must be before the End Date", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "There is an empty field, check all fields before submitting.", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "An Assessment Type must be selected.", "OK");
            }

        }

        private void OASwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (oASwitch.IsToggled)
            {
                pASwitch.IsEnabled = false;
                AssessmentType = "OA";
            }

            if (oASwitch.IsToggled == false)
            {
                pASwitch.IsEnabled = true;
            }
        }

        private void PASwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (pASwitch.IsToggled)
            {
                oASwitch.IsEnabled = false;
                AssessmentType = "PA";
            }

            if (pASwitch.IsToggled == false)
            {
                oASwitch.IsEnabled = true;
            }
        }
    }
}