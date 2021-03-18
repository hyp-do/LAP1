using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCourse : ContentPage
    {
        private readonly Term currentTerm = Globals.CurrentTerm;
        public AddCourse()
        {
            InitializeComponent();
        }

        private async void CourseSave_OnClicked(object sender, EventArgs e)
        {
            var addCourseConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            await addCourseConnection.CreateTableAsync<Course>();

            if ((Validation.IsFieldNull(courseName.Text)
                 && (Validation.IsFieldNull(instructorName.Text)
                     && (Validation.IsFieldNull(instructorEmail.Text)
                         && (Validation.IsFieldNull(instructorPhone.Text))))))
            {
                if (Validation.IsEmailValid(instructorEmail.Text))
                {
                    if (Validation.IsPhoneNumberValid(instructorPhone.Text))
                    {

                        if (courseStartDate.Date < courseEndDate.Date)
                        {
                            var course = new Course()
                            {
                                CourseName = courseName.Text,
                                CourseStartDate = courseStartDate.Date,
                                CourseEndDate = courseEndDate.Date,
                                TermId = currentTerm.TermId,
                                CourseStatus = courseStatus.SelectedItem.ToString(),
                                InstructorName = instructorName.Text,
                                InstructorEmail = instructorEmail.Text,
                                InstructorPhone = instructorPhone.Text,
                                CourseNotification = courseNotification.IsToggled
                            };

                            await addCourseConnection.InsertAsync(course);

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

        private void CourseNotification_OnToggled(object sender, ToggledEventArgs e)
        {
            
        }
    }
}