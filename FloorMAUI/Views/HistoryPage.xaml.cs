using FloorMAUI.ViewModels;

namespace FloorMAUI.Views;
public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
        BindingContext = new HistoryViewModel();
    }
}