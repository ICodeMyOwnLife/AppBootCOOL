using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBootContexts;
using AppBootModels;
using Microsoft.Win32;


namespace AppBootViewModels
{
    public class AppUpdaterViewModel: AppReadWriteViewModelBase
    {
        #region Fields
        private ICommand _addFilesCommand;
        private ApplicationInfo _application;
        private int _applicationId;
        private bool _canAddFiles;
        private ICollection<FileInfo> _files;
        private readonly AppBootContext _context = CreateDataContext();
        #endregion


        #region  Properties & Indexers
        public ICommand AddFilesCommand => GetCommand(ref _addFilesCommand, _ => AddFiles(), _ => CanAddFiles);

        public ApplicationInfo Application
        {
            get { return _application; }
            private set { SetProperty(ref _application, value); }
        }

        public int ApplicationId
        {
            get { return _applicationId; }
            set { SetProperty(ref _applicationId, value); }
        }

        public bool CanAddFiles
        {
            get { return _canAddFiles; }
            private set { SetProperty(ref _canAddFiles, value); }
        }

        public ICollection<FileInfo> Files
        {
            get { return _files; }
            private set { SetProperty(ref _files, value); }
        }
        #endregion


        #region Methods
        public void AddFiles()
        {
            var openDialog = new OpenFileDialog
            {
                InitialDirectory = Application.Directory,
                Multiselect = true,
                ShowReadOnly = true,
            };
            if (openDialog.ShowDialog() == true)
            {
                AddFiles(openDialog.FileNames);
            }
        }
        #endregion


        #region Override
        protected override async Task LoadAsync()
        {
            SetEnability(false);
            Application = await _context.Applications.FindAsync(ApplicationId);
            await Task.Run(() => Files = Application.Files.ToList());
            SetEnability(true);
        }

        protected override async Task SaveAsync()
        {
            SetEnability(false);
            await _context.SaveChangesAsync();
            SetEnability(true);
        }

        protected override void SetEnability(bool value)
        {
            base.SetEnability(value);
            CanAddFiles = value;
        }
        #endregion


        #region Implementation
        private void AddFiles(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                Application.Files.Add(new FileInfo(fileName, Application.Directory)
                {
                    FileData = new FileData(fileName)
                });

                Files = Application.Files.ToList();
            }
        }
        #endregion
    }
}