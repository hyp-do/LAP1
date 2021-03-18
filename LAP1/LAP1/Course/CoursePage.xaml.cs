using System;
using System.Collections.Generic;
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
    public partial class CoursePage : ContentPage
    {
        private Course currentCourse = Globals.CurrentCourse;
        private SQLiteAsyncConnection _courseConnection;
        
        public CoursePage()
        {
            InitializeComponent();

            _courseConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {

            await _courseConnection.CreateTableAsync<Course>();

            var course = await _courseConnection.Table<Course>()
                .Where(c => c.CourseId.Equals(currentCourse.CourseId))
                .FirstAsync();

            courseName.Text = course.CourseName;
            statusLabel.Text = course.CourseStatus.ToString();
            startDateLabel.Text = course.CourseStartDate.ToString("MM/dd/yyyy");
            endDateLabel.Text = course.CourseEndDate.ToString("MM/dd/yyyy");
            nameLabel.Text = course.InstructorName;
            emailLabel.Text = course.InstructorEmail;
            phoneLabel.Text = course.InstructorPhone;
            courseNotification.IsToggled = course.CourseNotification;

            base.OnAppearing();
        }

        private async void AssessmentsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentPage());
        }

        private async void CourseNotesButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseNotesPage());
        }

        private async void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCourse());
        }

        private async void DropCourseButton_OnClicked(object sender, EventArgs e)
        {
            var deleteCourse = await DisplayAlert("Warning", "Do you want to drop this course?", "Yes", "No");
            if (deleteCourse)
            {
                await _courseConnection.DeleteAsync(currentCourse);
                await Navigation.PopAsync();
            }
        }


    }
}