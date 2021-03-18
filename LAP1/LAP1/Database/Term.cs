using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace LAP1
{
    public class Term : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }

        private string _termTitle;

        public string TermTitle
        {
            get { return _termTitle; }

            set
            {
                if (_termTitle == value)
                {
                    return;
                }
                else
                {
                    _termTitle = value;
                }

                OnPropertyChanged(nameof(TermTitle));
            }
        }

        private DateTime _termStartDate;

        public DateTime TermStartDate
        {
            get { return _termStartDate; }

            set
            {
                if (_termStartDate == value)
                {
                    return;
                }
                else
                {
                    _termStartDate = value;
                }

                OnPropertyChanged(nameof(TermStartDate));
            }
        }

        private DateTime _termEndDate;

        public DateTime TermEndDate
        {
            get { return _termEndDate; }

            set
            {
                if (_termEndDate == value)
                {
                    return;
                }
                else
                {
                    _termEndDate = value;
                }

                OnPropertyChanged(nameof(TermStartDate));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}