using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayPersonalNotes : ContentPage
    {
        private readonly CourseNotes currentCourseNotes = Globals.CurrentCourseNotes;
        private readonly PersonalNotes currentPersonalNotes = Globals.CurrentPersonalNotes;
        private readonly User currentUser = Globals.CurrentUser;
        private readonly Course currentCourse = Globals.CurrentCourse;
        private readonly SQLiteAsyncConnection _displayNotesConnection;

        public DisplayPersonalNotes()
        {
            InitializeComponent();

            _displayNotesConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _displayNotesConnection.CreateTableAsync<PersonalNotes>();

            var currentNote = await _displayNotesConnection.Table<PersonalNotes>()
                .Where(n => n.NotesId.Equals(currentPersonalNotes.NotesId))
                .FirstAsync();


            PersonalLabel.Text = currentUser.StudentId.ToString();
            NoteTitleEntry.Text = currentNote.NotesTitle;
            NoteEditor.Text = currentNote.Note;


            base.OnAppearing();
        }

        private async void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPersonalNotes());
        }

        private async void ShareNotesButton_OnClicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = NoteTitleLabel.Text,
                Text = NoteEditor.Text
            });
        }

        private async void DeleteNotesButton_OnClicked(object sender, EventArgs e)
        {
            var deleteNote = await DisplayAlert("Warning", "Do you want to delete this note?", "Yes", "No");

            if (deleteNote)
            {
                var deleteNoteConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

                await deleteNoteConnection.DeleteAsync(currentPersonalNotes);
                await Navigation.PopAsync();
            }
        }
    }
}