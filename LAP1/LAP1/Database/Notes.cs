using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace LAP1.Database
{
    public class Notes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int NotesId { get; set; }
        // public int UserId { get; set; }
        // public int CourseId { get; set; }

        private string _notesTitle;

        public string NotesTitle
        {
            get { return _notesTitle; }

            set
            {
                if (_notesTitle == value)
                {
                    return;
                }
                else
                {
                    _notesTitle = value;
                }

                OnPropertyChanged(nameof(NotesTitle));
            }
        }


        private string _note;

        public string Note
        {
            get { return _note; }

            set
            {
                if (_note == value)
                {
                    return;
                }
                else
                {
                    _note = value;
                }

                OnPropertyChanged(nameof(Note));
            }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
