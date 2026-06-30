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
    internal class AddPageViewModel : INotifyPropertyChanged
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
            set
            {
                _name = value;
                ((Command)CreateCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                ((Command)CreateCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string CeoName
        {
            get => _ceoname;
            set
            {
                _ceoname = value;
                ((Command)CreateCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                ((Command)CreateCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                ((Command)CreateCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
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
            set
            {
                _rating = value;
                ((Command)CreateCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async Task Create()
        {
            Partner partner = new Partner()
            {
                Type = this.Type,
                Name = this.Name,
                CeoName = this.CeoName,
                Email = this.Email,
                Phone = this.Phone,
                Rating = this.Rating,
                Address = this.Address
            };
            await dbService.CreatePartnerAsync(partner);
            await Shell.Current.GoToAsync("..");
        }
        public ICommand CancelCommand { get; }
        public ICommand CreateCommand { get; }
        public AddPageViewModel()
        {
            CancelCommand = new Command(async () => await Cancel());
            CreateCommand = new Command(execute: async () => await Create(),
                canExecute: () =>
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(Type) &&
            !string.IsNullOrWhiteSpace(CeoName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Phone));

        }
    }
}
