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

        private string data;
        public ObservableCollection<OurClient> OurClients { get; set; }
        public ObservableCollection<OurClient> ShowClients { get; set; }
        public ObservableCollection<string> ListData { get; set; }

        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public OurClient SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        private RelayCommand showRecords;
        public RelayCommand ShowRecords
        {
            get
            {
                return showRecords ??
                  (showRecords = new RelayCommand(obj =>
                  {
                      if (!data.Equals("Все"))
                      {
                          int count = int.Parse(data);
                          ShowClients.Clear();
                          for (int i = 0; i < count; i++)
                              ShowClients.Add(OurClients[i]);
                      }

                  }));
            }
            set
            {
                
            }
        }
        public ClientViewModel()
        {
            OurClients = new ObservableCollection<OurClient>();
            ShowClients = new ObservableCollection<OurClient>();
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
                    ShowClients.Add(ourClient);
                }
                
                ListData = new ObservableCollection<string>();
                ListData.Add("Все");
                ListData.Add("10");
                ListData.Add("50");
                ListData.Add(OurClients.Count.ToString());
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
