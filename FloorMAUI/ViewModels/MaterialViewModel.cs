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
    internal class MaterialViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _id;
        private string _type;
        private string _name;
        private string _article;
        private double _minprice;
        private double _coefficient;
        private double _defectpercentage;
        public int Id { get => _id; set => _id = value; }
        public string? Type { get => _type; set => _type = value; }
        public string? Name { get => _name; set => _name = value; }
        public string? Article { get => _article; set => _article = value; }
        public double MinPrice { get => _minprice; set => _minprice = value; }
        public double Coefficient { get => _coefficient; set => _coefficient = value; }
        public double DefectPercentage { get => _defectpercentage; set => _defectpercentage = value; }
        DbService dbService = new DbService();
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async Task LoadProducts()
        {
            var products = await dbService.GetProductsAsync();
            Products.Clear();
            foreach (var item in products)
            {
                Products.Add(item);
            }
            OnPropertyChanged(nameof(Products));
        }
        private double _height;
        private double _width;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                ((Command)GetMaterialAmountCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                ((Command)GetMaterialAmountCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                ((Command)GetMaterialAmountCommand).ChangeCanExecute();
                OnPropertyChanged();
            }
        }
        int _materialAmount;
        public int MaterialAmount { 
            get => _materialAmount; 
            set
            {
                _materialAmount = value;
                OnPropertyChanged();
            }
        }
        public async Task GetMaterialAmount()
        {
            MaterialAmount = await dbService.GetMaterialAmount(SelectedProduct.Id, Height, Width);
        }

        public ObservableCollection<Product> Products { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand GetMaterialAmountCommand { get; set; }
        public MaterialViewModel()
        {
            CancelCommand = new Command(async () => await Cancel());
            GetMaterialAmountCommand = new Command
                (
                    execute: async () => await GetMaterialAmount(),
                    canExecute: () => SelectedProduct != null 
                    && Width != null 
                    && Height != null 
                    && Width != 0 
                    && Height != 0
                );
            Products = new ObservableCollection<Product>();
            Task.Run(async () => await LoadProducts());;
        }
    }
}
