using FloorMAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FloorMAUI.ViewModels
{
    [QueryProperty(nameof(CurrentPartner), "CurrentPartner")]
    internal class EditPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        DbService dbService = new DbService();
        private string _name;
        private string _type;
        private string _ceoname;
        private string _email;
        private string _phone;
        private string _address;
        private string _itn;
        private int _rating;

        public string Name
        {
            get => _name;
            set { _name = value;
                ((Command)EditCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set { _type = value;
                ((Command)EditCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string CeoName
        {
            get => _ceoname;
            set { _ceoname = value;
                ((Command)EditCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set { _email = value;
                ((Command)EditCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value;
                ((Command)EditCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => _address;
            set { _address = value;
                OnPropertyChanged();
            }
        }

        public string Itn
        {
            get => _itn;
            set { _itn = value; OnPropertyChanged(); }
        }

        public int Rating
        {
            get => _rating;
            set { _rating = value;
                ((Command)EditCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }
        private Partner _currentPartner;
        public Partner CurrentPartner
        {
            get => _currentPartner;
            set
            {
                _currentPartner = value;
                OnPropertyChanged();
                if (_currentPartner != null)
                {
                    Name = _currentPartner.Name;
                    Type = _currentPartner.Type;
                    CeoName = _currentPartner.CeoName;
                    Email = _currentPartner.Email;
                    Phone = _currentPartner.Phone;
                    Address = _currentPartner.Address;
                    Itn = _currentPartner.Itn;
                    Rating = _currentPartner.Rating;
                }
            }
        }
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async Task SaveChanges()
        {
            CurrentPartner.Name = Name;
            CurrentPartner.Type = Type;
            CurrentPartner.CeoName = CeoName;
            CurrentPartner.Email = Email;
            CurrentPartner.Phone = Phone;
            CurrentPartner.Address = Address;
            CurrentPartner.Itn = Itn;
            CurrentPartner.Rating = Rating;
            bool isSuccess = await dbService.UpdatePartnerAsync(CurrentPartner.Id, CurrentPartner);
            await Shell.Current.GoToAsync("..");
        }
        public ICommand CancelCommand { get; }
        public ICommand EditCommand { get; }
        public EditPageViewModel()
        {
            CancelCommand = new Command(async () => await Cancel());
            EditCommand = new Command(execute: async () => await SaveChanges(),
                canExecute: () =>
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(Type) &&
            !string.IsNullOrWhiteSpace(CeoName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Phone));
        }
    }
}
