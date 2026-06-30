using FloorMAUI.ViewModels;
namespace FloorMAUI.Views;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
        BindingContext = new AddPageViewModel();
        InitializeComponent();
	}
}