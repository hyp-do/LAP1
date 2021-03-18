using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using LAP1.Helpers;
using Plugin.LocalNotifications;
using SQLite;
using Xamarin.Forms;

namespace LAP1
{

    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _termConnection;
        private SQLiteAsyncConnection _notificationConnection;
        private SQLiteAsyncConnection _termSearchConnection;
        private ObservableCollection<Term> _terms;
        private bool isFirstRun;


        public MainPage()
        {
            InitializeComponent();

            _termConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _notificationConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _termSearchConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {

            await _termConnection.CreateTableAsync<Term>();

            List<Term> termsList = await _termConnection.Table<Term>().ToListAsync();

            if (!termsList.Any())
            {
                var testData = new TestData();
                testData.AddTestData();

                termsList = await _termConnection.Table<Term>().ToListAsync();

            }

            _terms = new ObservableCollection<Term>(termsList);
            termView.ItemsSource = _terms;

            await _notificationConnection.CreateTableAsync<Course>();
            await _notificationConnection.CreateTableAsync<Assessment>();
            var courseList = await _notificationConnection.Table<Course>().ToListAsync();
            var assessmentList = await _notificationConnection.Table<Assessment>().ToListAsync();

            int courseNotificationId = 0;
            int assessmentNotificationId = 0;

            foreach (Course course in courseList)
            {
                courseNotificationId++;
                if (course.CourseNotification)
                {
                    if (course.CourseStartDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("WGU Reminder", $"Your WGU course {course.CourseName} starts today!", courseNotificationId);
                    }

                    if (course.CourseEndDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("WGU Reminder", $"Your WGU Course {course.CourseName} ends today!", courseNotificationId);
                    }
                }
            }

            foreach (Assessment assessment in assessmentList)
            {
                assessmentNotificationId++;
                if (assessment.AssessmentNotification)
                {
                    if (assessment.AssessmentStartDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("WGU Reminder", $"Assessment {assessment.AssessmentTitle} starts today!", assessmentNotificationId);
                    }

                    if (assessment.AssessmentEndDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("WGU Reminder", $"Assessment {assessment.AssessmentTitle} ends today!", assessmentNotificationId);
                    }
                }
            }


            base.OnAppearing();
        }

        private async void AddTerm_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermPage());
        }

        private async void Notes_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonalNotesPage());
        }

        private void TermView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Globals.CurrentTerm = (Term)e.Item;
        }

        private async void TermView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Globals.CurrentTerm = (Term)e.SelectedItem;

            await Navigation.PushAsync(new TermPage());

        }

        private async void TermSearchEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            await _termSearchConnection.CreateTableAsync<Term>();

            var termSearchList = await _termSearchConnection.Table<Term>().ToListAsync();

            var termFoundList = new List<Term>();

            bool termFound = false;

            if (!String.IsNullOrEmpty(TermSearchEntry.Text))
            {
                foreach (Term term in termSearchList)
                {
                    if (term.TermTitle.ToUpper().Contains(TermSearchEntry.Text.ToUpper()))
                    {
                        termFoundList.Add(term);
                        termFound = true;
                    }
                }
            }

            if (termFound)
            {
                termView.ItemsSource = termFoundList;
            }
            else
            {
                termView.ItemsSource = termSearchList; 
            }
        }
    }
}
