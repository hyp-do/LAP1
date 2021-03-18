using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentPage : ContentPage
    {
       // public static Assessment currentAssessment = new Assessment();
        private readonly Course currentCourse = Globals.CurrentCourse;
        private readonly SQLiteAsyncConnection _assessmentConnection;
        private ObservableCollection<Assessment> _assessments;

        public AssessmentPage()
        {
            InitializeComponent();

            _assessmentConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            courseLabel.Text = currentCourse.CourseName + " Assessments";

            await _assessmentConnection.CreateTableAsync<Assessment>();

            var assessmentList = await _assessmentConnection.Table<Assessment>()
                .Where(a => a.CourseId.Equals(currentCourse.CourseId))
                .ToListAsync();

            _assessments = new ObservableCollection<Assessment>(assessmentList);
            assessmentView.ItemsSource = _assessments;

            base.OnAppearing();
        }

        private async void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            await _assessmentConnection.CreateTableAsync<Assessment>();

            var assessmentList = await _assessmentConnection.Table<Assessment>()
                .Where(a => a.CourseId.Equals(currentCourse.CourseId))
                .ToListAsync();

            if (!(assessmentList.Count >= 2))
            {
                await Navigation.PushAsync(new AddAssessment());
            }
            else
            {
                await DisplayAlert("ERROR",
                    "The maximum number of assessments already exists for this course (2). Please edit or delete an existing assessment.", "OK");
            }
        }

        private async void AssessmentView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Globals.CurrentAssessment = (Assessment)e.SelectedItem;
            await Navigation.PushAsync(new DisplayAssessment());
        }
    }
}