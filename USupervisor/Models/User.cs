using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor.Models
{
    abstract class User : INotifyPropertyChanged
    {
        protected string name;
        protected string email;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
                OnPropertyChanged("Name");
            }
        }

        protected void DeleteUser()
        {
            //Add delete code 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
