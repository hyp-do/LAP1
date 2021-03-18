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
    public partial class EditTermPage : ContentPage
    {
        private readonly Term currentTerm = Globals.CurrentTerm;
        public EditTermPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            //var termEditConnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            //{
            //    await termEditConnection.CreateTableAsync<Term>();

            //    var editTermList = await termEditConnection.Table<Term>()
            //        .Where(t => t.TermId.Equals(MainPage.currentTerm.TermId))
            //        .ToListAsync();

            //}

            termTitle.Text = currentTerm.TermTitle;
            termBeginDate.Date = currentTerm.TermStartDate;
            termEndDate.Date = currentTerm.TermEndDate;

            base.OnAppearing();
        }

        private async void TermSave_OnClicked(object sender, EventArgs e)
        {
            Term term = new Term()
            {
                TermId = currentTerm.TermId,
                TermTitle = termTitle.Text,
                TermStartDate = termBeginDate.Date,
                TermEndDate = termEndDate.Date
            };

            var termConnection = DependencyService.Get<ISQLiteDb>().GetConnection();

            if (Validation.IsFieldNull(term.TermTitle))
            {
                if (term.TermStartDate < term.TermEndDate)
                {
                    await termConnection.UpdateAsync(term);

                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("ERROR", "The Start Date must be before the End Date", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "There is an empty field, check all fields before submitting.", "OK");
            }

        }
    }
}