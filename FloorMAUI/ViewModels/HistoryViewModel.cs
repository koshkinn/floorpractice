using FloorMAUI.Services;
using FloorMAUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FloorMAUI.ViewModels
{
    [QueryProperty(nameof(ReceivedPartnerId), "PartnerId")]
    internal class HistoryViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _productName;
        private int _partnerId;
        private int _amount;
        private int _receivedPartnerId;

        public int Id {
            get => _id;
            set => _id = value; }

        public string ProductName {
            get => _productName;
            set => _productName = value; }

        public int PartnerId {
            get => _partnerId;
            set => _partnerId = value; }

        public int Amount {
            get => _amount;
            set => _amount = value; }


        public DateOnly? SellDate { get; set; }

        public int ReceivedPartnerId
        {
            get => _receivedPartnerId;
            set
            {
                if (_receivedPartnerId != value)
                {
                    _receivedPartnerId = value;
                    OnPropertyChanged();
                    _ = RefreshDataAsync(value);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        DbService dbService = new DbService();
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        //public async Task LoadHistory(int id)
        //{
        //    var history = await dbService.GetHistoryAsync(id);
        //}
        public async Task RefreshDataAsync(int id)
        {
            var historyData = await dbService.GetHistoryAsync(id);
            History.Clear();
            foreach (var item in historyData)
            {
                History.Add(item);
            }
            OnPropertyChanged(nameof(History));
        }
        public ObservableCollection<PartnerProduct> History { get; set; }
        public ICommand CancelCommand { get; }
        public HistoryViewModel()
        {
            CancelCommand = new Command(async () => await Cancel());
            History = new ObservableCollection<PartnerProduct>();
        }
    }
}
