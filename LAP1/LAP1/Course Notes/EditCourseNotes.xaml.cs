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
    public partial class EditCourseNotes : ContentPage
    {
        private readonly CourseNotes currentCourseNotes = Globals.CurrentCourseNotes;
        private readonly Course currentCourse = Globals.CurrentCourse;
        private readonly User currentUser = Globals.CurrentUser;
        private SQLiteAsyncConnection _editNotesConnection;

        public EditCourseNotes()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            CourseLabel.Text = currentCourse.CourseName;
            NoteTitleEntry.Text = currentCourseNotes.NotesTitle;
            NoteEditor.Text = currentCourseNotes.Note;

            base.OnAppearing();
        }

        private async void NoteEditSaveButton_OnClicked(object sender, EventArgs e)
        {
            var note = new CourseNotes()
            {
                NotesId = currentCourseNotes.NotesId,
                CourseId = currentCourse.CourseId,
                NotesTitle = NoteTitleEntry.Text,
                Note = NoteEditor.Text,
            };

            _editNotesConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            await _editNotesConnection.CreateTableAsync<CourseNotes>();

            if ((Validation.IsFieldNull(note.NotesTitle)) && (Validation.IsFieldNull(note.Note)))
            {
                await _editNotesConnection.UpdateAsync(note);

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", "There is an empty field, check all fields before submitting.", "OK");
            }

        }
    }
}