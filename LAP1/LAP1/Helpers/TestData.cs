using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using LAP1.Database;
using Xamarin.Forms;

namespace LAP1.Helpers
{
    
    public class TestData
    {
        private SQLiteAsyncConnection _addTestDataConnection;

        public void AddTestData()
        {
            var testUser = new User()
            {
                UserId = 1,
                StudentId = 5501,
                Password = "Test"
            };

            var testTerm = new Term()
            {
                TermTitle = "Term 1",
                TermId = 1,
                TermStartDate = DateTime.Today,
                TermEndDate = DateTime.Today.AddDays(90),
            };

            var testCourse = new Course()
            {
                CourseId = 1,
                TermId = 1,
                CourseName = "C971 Mobile App. Dev.",
                CourseStartDate = DateTime.Today,
                CourseEndDate = DateTime.Today.AddDays(30),
                CourseStatus = "Active",
                InstructorName = "James Mundy",
                InstructorPhone = "3865555555",
                InstructorEmail = "jmund11@wgu.edu",
                CourseNotification = true,
            };

            var testPAAssessment = new Assessment()
            {
                CourseId = 1,
                AssessmentId = 1,
                AssessmentTitle = "LAP 1",
                AssessmentStartDate = DateTime.Today,
                AssessmentEndDate = DateTime.Today.AddDays(25),
                AssessmentNotification = true,
            };

            var testOAAssessment = new Assessment()
            {
                CourseId = 1,
                AssessmentId = 2,
                AssessmentTitle = "LAP 2",
                AssessmentStartDate = DateTime.Today,
                AssessmentEndDate = DateTime.Today.AddDays(25),
                AssessmentNotification = true,
            };

            var testNote = new CourseNotes()
            {
                CourseId = 1,
                NotesTitle = "Test Note",
                Note = "XAML (Extensible Application Markup Language) is used as the mark up language, which dictates how the application will be displayed for WPF and Xamarin.Forms. "
            };

            _addTestDataConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            _addTestDataConnection.CreateTableAsync<User>();
            _addTestDataConnection.CreateTableAsync<Term>();
            _addTestDataConnection.CreateTableAsync<Course>();
            _addTestDataConnection.CreateTableAsync<Assessment>();
            _addTestDataConnection.CreateTableAsync<CourseNotes>();

            _addTestDataConnection.InsertAsync(testUser);
            _addTestDataConnection.InsertAsync(testTerm);
            _addTestDataConnection.InsertAsync(testCourse);
            _addTestDataConnection.InsertAsync(testPAAssessment);
            _addTestDataConnection.InsertAsync(testOAAssessment);
            _addTestDataConnection.InsertAsync(testNote);
        }
    }
}
