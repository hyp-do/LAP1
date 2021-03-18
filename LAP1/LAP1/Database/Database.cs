using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LAP1.Database
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Term>().Wait();
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<Assessment>().Wait();
        }

        public Task<List<Term>> GetTermAsync()
        {
            return _database.Table<Term>().ToListAsync();
        }

        public Task<List<Course>> GetCourseAsync()
        {
            return _database.Table<Course>().ToListAsync();
        }

        public Task<List<Assessment>> GetAssessmentAsync()
        {
            return _database.Table<Assessment>().ToListAsync();
        }

        public Task<int> SaveTermAsync(Term term)
        {
            return _database.InsertAsync(term);
        }

        public Task<int> SaveCourseAsync(Course course)
        {
            return _database.InsertAsync(course);
        }

        public Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            return _database.InsertAsync(assessment);
        }
    }
}
