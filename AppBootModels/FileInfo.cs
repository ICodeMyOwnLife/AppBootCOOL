using System;
using System.Diagnostics;
using System.IO;
using CB.Model.Common;


namespace AppBootModels
{
    public class FileInfo: ObservableObject
    {
        #region Fields
        private ApplicationInfo _application;
        private int _applicationId;
        private DateTime? _createdOn;
        private string _description;
        private string _directory;
        private string _extension;
        private FileData _fileData;
        private int? _id;
        private bool _isInit;
        private DateTime? _modifiedOn;
        private string _name;

        /*public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }*/
        private FileVersion _version = new FileVersion();
        #endregion


        #region  Constructors & Destructor
        public FileInfo() { }

        public FileInfo(string filePath, string appFolder)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found", filePath);
            SetDataFrom(filePath, appFolder);
        }
        #endregion


        #region  Properties & Indexers
        public ApplicationInfo Application
        {
            get { return _application; }
            set { SetProperty(ref _application, value); }
        }

        public int ApplicationId
        {
            get { return _applicationId; }
            set { SetProperty(ref _applicationId, value); }
        }

        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { SetProperty(ref _createdOn, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Directory
        {
            get { return _directory; }
            set { SetProperty(ref _directory, value); }
        }

        public string Extension
        {
            get { return _extension; }
            set { SetProperty(ref _extension, value); }
        }

        public FileData FileData
        {
            get { return _fileData; }
            set { SetProperty(ref _fileData, value); }
        }

        public int? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public bool IsInit
        {
            get { return _isInit; }
            set { SetProperty(ref _isInit, value); }
        }

        public DateTime? ModifiedOn
        {
            get { return _modifiedOn; }
            set { SetProperty(ref _modifiedOn, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public FileVersion Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }
        #endregion


        #region Methods
        public static FileVersion GetFileVersion(string filePath)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return new FileVersion(fileVersionInfo.FileVersion);
        }

        //private string _version;
        public string GetFilePath() => Path.Combine(Directory, Name);

        public string GetFullPath() => Path.GetFullPath(GetFilePath());

        public void Start()
        {
            Process.Start(GetFilePath());
        }
        #endregion


        #region Implementation
        private static string GetRelativePath(string rootDirectory, string mainDirectory)
        {
            if (!rootDirectory.EndsWith("\\")) rootDirectory += "\\";
            if (!mainDirectory.EndsWith("\\")) mainDirectory += "\\";
            var rootUri = new Uri(rootDirectory);
            var mainUri = new Uri(mainDirectory);
            return Uri.UnescapeDataString(rootUri.MakeRelativeUri(mainUri).ToString().Trim('/').Replace("/", "\\"));
        }

        private void SetDataFrom(string filePath, string appFolder)
        {
            Name = Path.GetFileName(filePath);
            Extension = Path.GetExtension(filePath);
            Directory = GetRelativePath(appFolder, Path.GetDirectoryName(filePath));
            Version = GetFileVersion(filePath);
        }
        #endregion


        public void CopyFrom(FileInfo fileInfo, bool includeId = true)
        {
            Application = fileInfo.Application;
            ApplicationId = fileInfo.ApplicationId;
            Name = fileInfo.Name;
            Directory = fileInfo.Directory;
            Description = fileInfo.Description;
            Extension = fileInfo.Extension;
            Version = fileInfo.Version;
            FileData = fileInfo.FileData;
            IsInit = fileInfo.IsInit;
            CreatedOn = fileInfo.CreatedOn;
            ModifiedOn = fileInfo.ModifiedOn;

            if (includeId) Id = fileInfo.Id;
        }
    }
}