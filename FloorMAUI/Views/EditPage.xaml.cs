using FloorMAUI.ViewModels;
namespace FloorMAUI.Views;

public partial class EditPage : ContentPage
{
	public EditPage()
	{
		BindingContext = new EditPageViewModel();
		InitializeComponent();
	}
}