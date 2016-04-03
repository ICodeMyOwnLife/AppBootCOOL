using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CB.Model.Common;


namespace AppBootModels
{
    public class ApplicationInfo: ObservableObject
    {
        #region Fields
        private DateTime? _createdOn;
        private string _description;
        private string _directory;
        private int? _id;
        private DateTime? _modifiedOn;
        private string _name;
        #endregion


        #region  Constructors & Destructor
        public ApplicationInfo() { }

        public ApplicationInfo(string folderPath)
        {
            if (!System.IO.Directory.Exists(folderPath)) throw new DirectoryNotFoundException();
            Name = Path.GetFileName(folderPath);
            Directory = folderPath;
        }
        #endregion


        #region  Properties & Indexers
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

        public virtual ICollection<FileInfo> Files { get; set; } = new HashSet<FileInfo>();

        public int? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
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
        #endregion


        #region Methods
        public void CopyFrom(ApplicationInfo application, bool includeId = true)
        {
            Name = application.Name;
            Directory = application.Directory;
            Description = application.Description;
            CreatedOn = application.CreatedOn;
            ModifiedOn = application.ModifiedOn;
            if (includeId) Id = application.Id;
        }

        public void SetFiles(ICollection<FileInfo> files)
        {
            for (var i = 0; i < Files.Count;)
            {
                var existingFile = Files.ElementAt(i);
                if (files.Contains(existingFile)) ++i;
                else Files.Remove(existingFile);
            }
            foreach (var file in files.Where(file => !Files.Contains(file)))
            {
                Files.Add(file);
            }
        }
        #endregion
    }
}