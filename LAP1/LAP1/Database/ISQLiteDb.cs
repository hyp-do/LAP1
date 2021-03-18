using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LAP1.Database
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
