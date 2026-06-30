using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FloorMAUI.Services;
namespace FloorMAUI.ViewModels
{
    internal class PartnersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RefreshDataAsync()
        {
            DbService dbService = new DbService();
            Partners.Clear();
            PartnerProducts.Clear();
            var partnersList = await dbService.GetPartnersAsync();
            var productsList = await dbService.GetPartnerProductsAsync();
            foreach (var product in productsList)
            {
                PartnerProducts.Add(product);
            }
            foreach (var partner in partnersList)
            {
                partner.TotalAmount = PartnerProducts
                    .Where(item => item.PartnerId == partner.Id)
                    .Sum(item => item.Amount);

                Partners.Add(partner);
            }
        }

        public async Task GoToAddPage()
        {
            await Shell.Current.GoToAsync("AddPage");
        }

        public async Task GoToEditPage(Partner selectedPartner)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"CurrentPartner", selectedPartner }
            };
            await Shell.Current.GoToAsync("EditPage", navigationParameter);
        }

        public async Task GoToHistoryPage(int id)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"PartnerId", id }
            };
            await Shell.Current.GoToAsync("HistoryPage", navigationParameter);
        }

        public async Task GoToMaterialPage()
        {
            await Shell.Current.GoToAsync("MaterialPage");
        }

        public ObservableCollection<Partner> Partners { get; set; }
        public ObservableCollection<PartnerProduct> PartnerProducts { get; set; }
        public ICommand AddPageCommand { get; }
        public ICommand EditPageCommand { get; }
        public ICommand HistoryPageCommand { get; }
        public ICommand MaterialPageCommand { get; }
        public PartnersViewModel()
        {
            DbService dbService = new DbService();
            AddPageCommand = new Command(async () => await GoToAddPage());
            EditPageCommand = new Command<Partner>(async (partner) => await GoToEditPage(partner));
            HistoryPageCommand = new Command<int>(async (id) => await GoToHistoryPage(id));
            MaterialPageCommand = new Command(async () => await GoToMaterialPage());
            Partners = new ObservableCollection<Partner>();
            PartnerProducts = new ObservableCollection<PartnerProduct>();
        }
    }
}
