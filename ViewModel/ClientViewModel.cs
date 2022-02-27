using Biuty.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Biuty.ViewModel
{
    class ClientViewModel : INotifyPropertyChanged
    {
        private OurClient selectedClient;
        public ObservableCollection<OurClient> OurClients { get; set; }
        public OurClient SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ClientViewModel()
        {
            OurClients = new ObservableCollection<OurClient>();
            using (ModelDB db = new ModelDB())
            {


                var query = from client in db.Client
                            select new
                            {
                                FirstName = client.FirstName,
                                LastName = client.LastName,
                                Patronomic = client.Patronymic,
                                BirthDay = client.Birthday,
                                Phone = client.Phone,
                                Email = client.Email,
                                RegDate = client.RegistrationDate,
                                LastVisit = db.ClientService.Max(p => p.StartTime),
                                Count = db.ClientService.Count(p => p.ClientID.Equals(client.LastName))
                            };

               

                foreach (var i in query)
                {
                    OurClient ourClient = new OurClient();
                    ourClient.FirstName = i.FirstName;
                    ourClient.Patronomic = i.Patronomic;
                    ourClient.LastName = i.LastName;
                    ourClient.Birthday = i.BirthDay;
                    ourClient.Phone = i.Phone;
                    ourClient.Email = i.Email;
                    ourClient.RegistrationDate = i.RegDate;
                    ourClient.LastVisit = i.LastVisit;
                    ourClient.Count = i.Count;
                    ourClient.Tags = getTitle(i.LastName,db);
                    OurClients.Add(ourClient);
                }
            }
        }

        private string getTitle(string firstName,ModelDB db)
        {
            var gen = from p in db.Client
                      join c in db.Tag on p.GenderCode equals c.Title
                      where p.LastName.Equals(firstName)
                      select new { Title = c.Title };

            return gen.FirstOrDefault().Title;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
