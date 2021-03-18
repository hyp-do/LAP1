using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using LAP1.Annotations;

namespace LAP1.Database
{
    public class Course : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }

        public int TermId { get; set; }

        private string _courseName;

        public string CourseName
        {
            get { return _courseName; }

            set
            {
                if (_courseName == value)
                {
                    return;
                }
                else
                {
                    _courseName = value;
                }

                OnPropertyChanged(nameof(CourseName));
            }
        }

        private string _courseStatus;

        public string CourseStatus
        {
            get { return _courseStatus; }

            set
            {
                if (_courseStatus == value)
                {
                    return;
                }
                else
                {
                    _courseStatus = value;
                }

                OnPropertyChanged(nameof(CourseStatus));
            }
        }

        private DateTime _courseStartDate;

        public DateTime CourseStartDate
        {
            get { return _courseStartDate; }

            set
            {
                if (_courseStartDate == value)
                {
                    return;
                }
                else
                {
                    _courseStartDate = value;
                }

                OnPropertyChanged(nameof(CourseStartDate));
            }
        }

        private DateTime _courseEndDate;

        public DateTime CourseEndDate
        {
            get { return _courseEndDate; }

            set
            {
                if (_courseEndDate == value)
                {
                    return;
                }
                else
                {
                    _courseEndDate = value;
                }

                OnPropertyChanged(nameof(CourseEndDate));
            }
        }

        private string _instructorName;

        public string InstructorName
        {
            get { return _instructorName; }

            set
            {
                if (_instructorName == value)
                {
                    return;
                }
                else
                {
                    _instructorName = value;
                }

                OnPropertyChanged(nameof(InstructorName));
            }
        }

        private string _instructorEmail;

        public string InstructorEmail
        {
            get { return _instructorEmail; }

            set
            {
                if (_instructorEmail == value)
                {
                    return;
                }
                else
                {
                    _instructorEmail = value;
                }

                OnPropertyChanged(nameof(InstructorEmail));
            }
        }

        private string _instructorPhone;

        public string InstructorPhone
        {
            get { return _instructorPhone; }

            set
            {
                if (_instructorPhone == value)
                {
                    return;
                }
                else
                {
                    _instructorPhone = value;
                }

                OnPropertyChanged(nameof(InstructorPhone));
            }
        }

        private bool _courseNotification;

        public bool CourseNotification
        {
            get { return _courseNotification; }

            set
            {
                if (_courseNotification == value)
                {
                    return;
                }
                else
                {
                    _courseNotification = value;
                }

                OnPropertyChanged(nameof(CourseNotification));
            }
        }
        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
