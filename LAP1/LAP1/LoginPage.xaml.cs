using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP1.Database;
using LAP1.Helpers;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection _userConnection;
        private User currentUser = Globals.CurrentUser;
        public LoginPage()
        {
            InitializeComponent();

            _userConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _userConnection.CreateTableAsync<Term>();

            List<Term> termList = await _userConnection.Table<Term>().ToListAsync();

            if (!termList.Any())
            {
                var testData = new TestData();
                testData.AddTestData();
            }

            base.OnAppearing();
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            // var userName = StudentIdEntry.Text;
            // var password = StudentPasswordEntry.Text;

            var validUser = new User();
            var validPassword = new User();

            bool isError = false;

            if ((!String.IsNullOrEmpty(StudentIdEntry.Text)) && (!String.IsNullOrEmpty(StudentPasswordEntry.Text)))
            {
                try
                {
                    validUser = await _userConnection.Table<User>()
                        .Where(u => u.StudentId.Equals(StudentIdEntry.Text))
                        .FirstAsync();

                    validPassword = await _userConnection.Table<User>()
                        .Where(p => p.Password.Equals(StudentPasswordEntry.Text))
                        .FirstAsync();

                }
                catch (Exception exception)
                {
                    await DisplayAlert("ERROR", "Please enter a valid Student ID Number and Password combination.",
                        "OK");
                    isError = true;
                }

                
                //if ((!(validUser.StudentId.ToString() == StudentIdEntry.Text.ToString())) ||
                //    (!(validUser.Password.ToString() == StudentPasswordEntry.Text.ToString())))
                //{
                //    await DisplayAlert("ERROR", "Please enter a valid Student ID Number and Password combination.",
                //        "OK");
                //}
                if (isError == false) 
                {
                    Globals.CurrentUser = validUser;
                
                    await Navigation.PushAsync(new MainPage());
                
                }
            }
            else
            {
                await DisplayAlert("ERROR", "Student ID and Password are required",
                    "OK");
            }
        }
    }
}