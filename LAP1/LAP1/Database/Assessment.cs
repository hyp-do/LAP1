using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using LAP1.Annotations;
using SQLite;

namespace LAP1.Database
{
    public class Assessment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }

        private string _assessmentTitle;

        public string AssessmentTitle
        {
            get { return _assessmentTitle; }

            set
            {
                if (_assessmentTitle == value)
                {
                    return;
                }
                else
                {
                    _assessmentTitle = value;
                }

                OnPropertyChanged(nameof(AssessmentTitle));
            }
        }

        private DateTime _assessmentStartDate;

        public DateTime AssessmentStartDate
        {
            get { return _assessmentStartDate; }

            set
            {
                if (_assessmentStartDate == value)
                {
                    return;
                }
                else
                {
                    _assessmentStartDate = value;
                }

                OnPropertyChanged(nameof(AssessmentStartDate));
            }
        }

        private DateTime _assessmentEndDate;

        public DateTime AssessmentEndDate
        {
            get { return _assessmentEndDate; }

            set
            {
                if (_assessmentEndDate == value)
                {
                    return;
                }
                else
                {
                    _assessmentEndDate = value;
                }

                OnPropertyChanged(nameof(AssessmentEndDate));
            }
        }

        private string _type;


        public string Type
        {
            get { return _type; }

            set
            {
                if (_type == value)
                {
                    return;
                }
                else
                {
                    _type = value;
                }

                OnPropertyChanged(nameof(Type));
            }
        }

        public int CourseId { get; set; }

        private bool _assessmentNotification;

        public bool AssessmentNotification
        {
            get { return _assessmentNotification; }

            set
            {
                if (_assessmentNotification == value)
                {
                    return;
                }
                else
                {
                    _assessmentNotification = value;
                }

                OnPropertyChanged(nameof(AssessmentNotification));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
