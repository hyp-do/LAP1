using SQLite;

namespace LAP1.Database
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        public int StudentId { get; set; }

        public string Password { get; set; }

    }
}