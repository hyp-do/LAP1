using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public partial class TermReport : ContentPage
    {
        private Course currentCourse = Globals.CurrentCourse;
        private Term currentTerm = Globals.CurrentTerm;
        private SQLiteAsyncConnection _reportActiveConnection;

        public TermReport()
        {
            InitializeComponent();

            _reportActiveConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
           
        }

        protected override async void OnAppearing()
        {


            var courseActiveList = await _reportActiveConnection.Table<Course>()
                .Where(c => c.TermId.Equals(currentTerm.TermId))
                .ToListAsync();

            var activeCount = 0;
            var inactiveCount = 0;
            var completedCount = 0;

            foreach (Course course in courseActiveList)
            {
                if (course.CourseStatus.Equals("Active"))
                {
                    activeCount++;
                }
            
                if (course.CourseStatus.Equals("Inactive"))
                {
                    inactiveCount++;
                }
            
                if (course.CourseStatus.Equals("Completed"))
                {
                    completedCount++;
                }
            }

            var termTitle = currentTerm.TermTitle;

            var termStartDate = currentTerm.TermStartDate;
            var termEndDate = currentTerm.TermEndDate;

            var daysLeftInTerm = ((termEndDate - termStartDate));

            termReportLabel.Text = $"{termTitle} Report";

            timeInTermLeftLabel.Text = $"{daysLeftInTerm.Days} days left in term";

            activeCoursesLabel.Text = activeCount.ToString();

            inactiveCourseLabel.Text = inactiveCount.ToString();

            completedCoursesLabel.Text = completedCount.ToString();

            base.OnAppearing();
        }
    }
}