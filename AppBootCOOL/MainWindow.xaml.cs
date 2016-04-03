using System.Windows;
using AppBootViewModels;


namespace AppBootCOOL
{
    public partial class MainWindow
    {
        #region  Constructors & Destructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region Event Handlers
        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vmd = DataContext as AppBootViewModel;
            if (vmd != null) await vmd.BootAsync();
        }
        #endregion
    }
}