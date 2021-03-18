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
    public partial class AddCourseNotes : ContentPage
    {
        private Course currentCourse = Globals.CurrentCourse;
        public AddCourseNotes()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            CourseLabel.Text = currentCourse.CourseName + " Notes";
            base.OnAppearing();
        }

        private async void NoteSaveButton_OnClicked(object sender, EventArgs e)
        {
            var note = new CourseNotes()
            {
                CourseId = currentCourse.CourseId,
                NotesTitle = NoteTitleEntry.Text,
                Note = NoteEditor.Text,
            };

            var addNoteConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            await addNoteConnection.CreateTableAsync<CourseNotes>();

            if ((Validation.IsFieldNull(note.NotesTitle)) && (Validation.IsFieldNull(note.Note)))
            {
                await addNoteConnection.InsertAsync(note);

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", "There is an empty field, check all fields before submitting.", "OK");
            }
        }
    }
}