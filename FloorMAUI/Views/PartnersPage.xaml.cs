using FloorMAUI.ViewModels;

namespace FloorMAUI.Views;

public partial class Partners : ContentPage
{
	public Partners()
	{
		InitializeComponent();
		BindingContext = new PartnersViewModel();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PartnersViewModel viewModel)
        {
            await viewModel.RefreshDataAsync();
        }
    }
} 