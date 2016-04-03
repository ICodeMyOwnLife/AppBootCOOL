using System;
using System.Windows.Input;
using AppBootModels;
using AppBootViewModels;


namespace AppMasterCOOL
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
        private void DatMain_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var application = datMain.SelectedItem as ApplicationInfo;
            var appUpdater = new AppUpdaterCOOL.MainWindow();
            if(application?.Id == null) return;
            var appUpdaterViewModel = new AppUpdaterViewModel { ApplicationId = application.Id.Value };
            appUpdater.DataContext = appUpdaterViewModel;
            appUpdater.ShowDialog();
        }
        #endregion
    }
}