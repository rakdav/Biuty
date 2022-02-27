using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Biuty.Model
{
    public class OurClient : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string patronomic;
        private DateTime? birthday;
        private string phone;
        private string email;
        private DateTime registrationDate;
        private DateTime lastVisit;
        private int count;
        private string tags;
        private string color;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string Patronomic
        {
            get { return patronomic; }
            set
            {
                patronomic = value;
                OnPropertyChanged("Patronomic");
            }
        }
        public DateTime? Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set
            {
                registrationDate = value;
                OnPropertyChanged("RegistrationDate");
            }
        }
        public DateTime LastVisit
        {
            get { return lastVisit; }
            set
            {
                lastVisit = value;
                OnPropertyChanged("LastVisit");
            }
        }
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        public string Tags
        {
            get { return tags; }
            set
            {
                tags = value;
                OnPropertyChanged("Tags");
            }
        }

        public string Color
        {
            get { return color;}
            set
            {
                color = value;
                OnPropertyChanged("color");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
