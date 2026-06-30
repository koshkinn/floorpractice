using FloorMAUI.Views;

namespace FloorMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AddPage", typeof(AddPage));
            Routing.RegisterRoute("Partners", typeof(Partners));
            Routing.RegisterRoute("EditPage", typeof(EditPage));
            Routing.RegisterRoute("HistoryPage", typeof(HistoryPage));
            Routing.RegisterRoute("MaterialPage", typeof(MaterialPage));
        }
    }
}
