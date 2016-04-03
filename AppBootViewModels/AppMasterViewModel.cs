using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using AppBootContexts;
using AppBootModels;


namespace AppBootViewModels
{
    public class AppMasterViewModel: AppReadWriteViewModelBase
    {
        #region Fields
        private ICommand _addApplicationCommand;
        private ICollection<ApplicationInfo> _applications;
        private bool _canAddApplication;
        private readonly AppBootContext _context = CreateDataContext();
        #endregion


        #region  Properties & Indexers
        public ICommand AddApplicationCommand
            => GetCommand(ref _addApplicationCommand, _ => AddApplication(), _ => CanAddApplication);

        public ICollection<ApplicationInfo> Applications
        {
            get { return _applications; }
            private set { SetProperty(ref _applications, value); }
        }

        public bool CanAddApplication
        {
            get { return _canAddApplication; }
            private set { SetProperty(ref _canAddApplication, value); }
        }
        #endregion


        #region Methods
        public void AddApplication()
        {
            using (var browseDialog = new FolderBrowserDialog())
            {
                if (browseDialog.ShowDialog() == DialogResult.OK)
                {
                    Applications.Add(new ApplicationInfo(browseDialog.SelectedPath));
                }
            }
        }
        #endregion


        #region Override
        protected override async Task LoadAsync()
        {
            SetEnability(false);
            await _context.Applications.ToListAsync();
            Applications = _context.Applications.Local;
            SetEnability(true);
        }

        protected override async Task SaveAsync()
        {
            SetEnability(false);
            await _context.SaveChangesAsync();
            await _context.Applications.ToListAsync();
            SetEnability(true);
        }

        protected override void SetEnability(bool value)
        {
            base.SetEnability(value);
            CanAddApplication = value;
        }
        #endregion
    }
}