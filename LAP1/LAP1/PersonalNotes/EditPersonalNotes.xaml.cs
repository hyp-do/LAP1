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
    public partial class EditPersonalNotes : ContentPage
    {
        private readonly PersonalNotes currenPersonalNotes = Globals.CurrentPersonalNotes;
        private readonly CourseNotes currentCourseNotes = Globals.CurrentCourseNotes;
        private readonly Course currentCourse = Globals.CurrentCourse;
        private readonly User currentUser = Globals.CurrentUser;
        private SQLiteAsyncConnection _editNotesConnection;

        public EditPersonalNotes()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            CourseLabel.Text = currentUser.UserId.ToString();
            NoteTitleEntry.Text = currenPersonalNotes.NotesTitle;
            NoteEditor.Text = currenPersonalNotes.Note;

            base.OnAppearing();
        }

        private async void NoteEditSaveButton_OnClicked(object sender, EventArgs e)
        {
            var note = new PersonalNotes()
            {
                NotesId = currenPersonalNotes.NotesId,
                UserId = currentUser.UserId,
                NotesTitle = NoteTitleEntry.Text,
                Note = NoteEditor.Text,
            };

            _editNotesConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            await _editNotesConnection.CreateTableAsync<PersonalNotes>();

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