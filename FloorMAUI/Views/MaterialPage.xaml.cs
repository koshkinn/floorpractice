using FloorMAUI.ViewModels;

namespace FloorMAUI.Views;

public partial class MaterialPage : ContentPage
{
	public MaterialPage()
	{
		InitializeComponent();
        BindingContext = new MaterialViewModel();
    }
}