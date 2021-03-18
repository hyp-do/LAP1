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
    public partial class TermPage : ContentPage
    {
        private Course currentCourse = Globals.CurrentCourse;
        private Term currentTerm = Globals.CurrentTerm;
        private SQLiteAsyncConnection _courseConnection;
        private SQLiteAsyncConnection _termTitleConnection;
        private SQLiteAsyncConnection _courseSearchConnection;
        private ObservableCollection<Course> _courses;

        public TermPage()
        {
            InitializeComponent();

            _termTitleConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _courseConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _courseSearchConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            //await _termTitleConnection.CreateTableAsync<Term>();

            var term = await _termTitleConnection.Table<Term>()
                .Where(t => t.TermId.Equals(currentTerm.TermId))
                .FirstAsync();

            TermLabel.Text = term.TermTitle;

            await _courseConnection.CreateTableAsync<Course>();

            var courseList = await _courseConnection.Table<Course>()
                .Where(c => c.TermId.Equals(currentTerm.TermId))
                .ToListAsync();

            // NOTE TO SELF: This also works. 
            //var courseList =
            //    await _courseConnection.QueryAsync<Course>(
            //        $"SELECT * FROM Course WHERE TermId = '{MainPage.currentTerm.TermId}'");



            _courses = new ObservableCollection<Course>(courseList);
            courseView.ItemsSource = _courses;

            base.OnAppearing();
        }

        private async void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourse());
        }

        private async void CourseView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Globals.CurrentCourse = (Course)e.SelectedItem;
            await Navigation.PushAsync(new CoursePage());
            //await Navigation.PushAsync(new CourseInfo(courseSelected));
        }

        private async  void EditTermButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage());
        }

        private async void DropTermButton_OnClicked(object sender, EventArgs e)
        {
            var deleteTerm = await DisplayAlert("Warning", "Do you want to drop this term?", "Yes", "No");
            if (deleteTerm)
            {
                await _courseConnection.DeleteAsync(currentTerm);
                await Navigation.PopAsync();
            }
            //TODO Delete all Assessments and Notes from Courses as well. 
            // These two threw exceptions. SQL statements are syntactically correct. Not sure why. More research
            //var deleteCourses = await _courseConnection.QueryAsync<Course>($"DELETE * FROM COURSE WHERE TermId = '{MainPage.currentTerm.TermId}'");

            //var deleteTerm =
            //    await _courseConnection.QueryAsync<Term>(
            //        $"DELETE * FROM Term WHERE TermId = '{MainPage.currentTerm.TermId}'");

            // Tried to pass a list into DeleteAsync, no worky. Revisit idea later
            //var coursesToDelete = await _courseConnection.Table<Course>()
            //    .Where(c => c.TermId.Equals(MainPage.currentTerm.TermId))
            //    .ToListAsync();

            //await _courseConnection.DeleteAsync<Course>(coursesToDelete);

            //var termToDelete = await _courseConnection.Table<Term>()
            //    .Where(t => t.TermId.Equals(MainPage.currentTerm.TermId))
            //    .ToListAsync();

            //await _courseConnection.DeleteAsync<Term>(termToDelete);

        }

        private async void CourseSearchEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            await _courseSearchConnection.CreateTableAsync<Course>();

            var courseSearchList = await _courseSearchConnection.Table<Course>()
                .Where(c => c.CourseId.Equals(currentCourse.CourseId))
                .ToListAsync();

            var courseFoundList = new List<Course>();

            bool courseFound = false;

            if (!String.IsNullOrEmpty(CourseSearchEntry.Text))
            {
                foreach (Course course in courseSearchList)
                {
                    if (course.CourseName.ToUpper().Contains(CourseSearchEntry.Text.ToUpper()))
                    {
                        courseFoundList.Add(course);
                        courseFound = true;
                    }
                }
            }

            if (courseFound)
            {
                courseView.ItemsSource = courseFoundList;
            }
            else
            {
                courseView.ItemsSource = courseSearchList;
            }
        }

        private async void DisplayTermReport_Activated(object sender, EventArgs e)
        {
            await _courseConnection.CreateTableAsync<Course>();

            var courseList = await _courseConnection.Table<Course>()
                .Where(c => c.TermId.Equals(currentTerm.TermId))
                .ToListAsync();

            var courseCount = courseList.Count();

            if (courseCount > 0)
            {
                await Navigation.PushAsync(new TermReport());
            }
            else
            {
                await DisplayAlert("Information", "In order to generate a Term Report you need to at least add 1 course.", "OK");

            }
        }
    }
}