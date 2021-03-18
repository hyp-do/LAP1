using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourse : ContentPage
    {
        private readonly Course currentCourse = Globals.CurrentCourse;
        public EditCourse()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            courseName.Text = currentCourse.CourseName;
            courseStartDate.Date = currentCourse.CourseStartDate;
            courseEndDate.Date = currentCourse.CourseEndDate;
            courseStatus.SelectedItem = currentCourse.CourseStatus.ToString();
            instructorName.Text = currentCourse.InstructorName;
            instructorEmail.Text = currentCourse.InstructorEmail;
            instructorPhone.Text = currentCourse.InstructorPhone;
            courseNotification.IsToggled = currentCourse.CourseNotification;

            base.OnAppearing();
        }

        private async void CourseSave_OnClicked(object sender, EventArgs e)
        {
            var course = new Course()
            {
                CourseId = currentCourse.CourseId,
                TermId = currentCourse.TermId,
                CourseName = courseName.Text,
                CourseStartDate = courseStartDate.Date,
                CourseEndDate = courseEndDate.Date,
                CourseStatus = courseStatus.SelectedItem.ToString(),
                InstructorName = instructorName.Text,
                InstructorEmail = instructorEmail.Text,
                InstructorPhone = instructorPhone.Text,
                CourseNotification = courseNotification.IsToggled
            };

            var addCourseConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            if ((Validation.IsFieldNull(course.CourseName)
                 && (Validation.IsFieldNull(course.InstructorName)
                     && (Validation.IsFieldNull(course.InstructorEmail)
                         && (Validation.IsFieldNull(course.InstructorPhone))))))
            {
                if (Validation.IsEmailValid(instructorEmail.Text))
                {
                    if (Validation.IsPhoneNumberValid(instructorPhone.Text))
                    {

                        if (course.CourseStartDate < course.CourseEndDate)
                        {
                            await addCourseConnection.UpdateAsync(course);

                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("ERROR", "The Start Date must be before the End Date", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "The phone number is not in a valid format. Please try again.",
                            "OK");
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "The email address is not valid, please check it.", "OK");
                }

            }
            else
            {
                await DisplayAlert("ERROR", "There is an empty field, check all fields before submitting.", "OK");
            }


        }
    }
}