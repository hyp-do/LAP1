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
    public partial class PersonalNotesPage : ContentPage
    {
        //public static Notes currentNotes = new Notes();
        //private Notes currentNotes = Globals.CurrentNote;
        private SQLiteAsyncConnection _notesConnection;
        private ObservableCollection<PersonalNotes> _notes;
        // private Course currentCourse = Globals.CurrentCourse;
        private User currentUser = Globals.CurrentUser;

        public PersonalNotesPage()
        {
            InitializeComponent();

            _notesConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            // noteLabel.Text = currentCourse.CourseName;

            await _notesConnection.CreateTableAsync<PersonalNotes>();

            //var addNoteMessage = new Notes()
            //{
            //    NotesId = -1,
            //    NotesTitle = "Create a note!",
            //    Note = "Create a note!"
            //};

            var notesList = await _notesConnection.Table<PersonalNotes>()
                .Where(n => n.UserId.Equals(currentUser.UserId))
                .ToListAsync();

            _notes = new ObservableCollection<PersonalNotes>(notesList);

            if (notesList.Count <= 0)
            {
                notesView.ItemsSource = null;
            }
            else
            {
                notesView.ItemsSource = _notes;
            }


            base.OnAppearing();
        }

        private async void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNPersonalNotes());
        }

        private async void NotesView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Globals.CurrentPersonalNotes = (PersonalNotes) e.SelectedItem;

            await Navigation.PushAsync(new DisplayPersonalNotes());
        }
    }
}