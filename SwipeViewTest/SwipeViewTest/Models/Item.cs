using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SwipeViewTest.Models
{
    public class Item : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        private bool completed;
        public bool Completed
        {
            get => completed;
            set
            {
                completed = value;
                OnPropertyChanged();
            }
        }
        private bool retryAvailable;
        public bool RetryAvailable
        {
            get => retryAvailable;
            set
            {
                retryAvailable = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}