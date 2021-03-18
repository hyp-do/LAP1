using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using LAP1.Database;
using LAP1.Droid.Database;
using SQLite;

[assembly: Dependency(typeof(SQLiteDb))]

namespace LAP1.Droid.Database
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var fileName = "wguterm_db.db3";
            var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var completePath = System.IO.Path.Combine(folderPath, fileName);

            return new SQLiteAsyncConnection(completePath);
        }
    }
}