using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBootModels;
using FileInfo = AppBootModels.FileInfo;


namespace AppBootViewModels
{
    public class AppBootViewModel: AppBootViewModelBase
    {
        #region Fields
        private const string CHECKING = "Checking for updates...";
        private const string STARTING = "Starting program";
        private const string UPDATING = "Updating...";
        private readonly object _locker = new object();
        #endregion


        #region  Properties & Indexers
        public ICollection<FileUpdate> FileUpdates { get; } = new ObservableCollection<FileUpdate>();
        #endregion


        #region Methods
        public async Task BootAsync()
        {
            State = CHECKING;
            try
            {
                var appId = GetAppId();
                using (var context = CreateDataContext())
                {
                    var app = await context.Applications.FindAsync(appId);

                    foreach (var fileInfo in app.Files.AsEnumerable().Where(IsUpdated))
                    {
                        FileUpdates.Add(new FileUpdate { Info = fileInfo, State = UpdateState.Pending });
                    }

                    State = UPDATING;
                    await UpdateAsync();
                    State = STARTING;
                    await WaitToClose();
                    app.Files.FirstOrDefault(f => f.IsInit)?.Start();
                }
            }
            catch (Exception exception)
            {
                State = exception.Message;
            }
        }
        #endregion


        #region Implementation
        private static int GetAppId()
        {
            return int.Parse(ConfigurationManager.AppSettings["appId"]);
        }

        private static bool IsUpdated(FileInfo file)
        {
            var currentFile = file.Directory + file.Name;
            return !File.Exists(currentFile) || !file.Version.HasVersion ||
                   file.Version.CompareTo(FileInfo.GetFileVersion(currentFile)) > 0;
        }

        private void Update()
        {
            var count = 0.0;
            /*foreach (var fileUpdate in FileUpdates)
            {
                Update(fileUpdate);
            }*/

            FileUpdates.AsParallel().ForAll(f =>
            {
                Update(f);
                lock (_locker)
                {
                    ++count;
                    Progress = count / FileUpdates.Count;
                }
            });
        }

        private static void Update(FileUpdate fileUpdate)
        {
            fileUpdate.State = UpdateState.Updating;
            try
            {
                using (var context = CreateDataContext())
                {
                    var fileData = context.FileDatas.Find(fileUpdate.Info.Id);

                    if (fileData == null || !fileData.CheckData())
                    {
                        fileUpdate.State = UpdateState.Corrupted;
                        return;
                    }
                    WriteFile(fileUpdate.Info, fileData);
                    fileUpdate.State = UpdateState.Updated;
                }
            }
            catch (Exception exception)
            {
                fileUpdate.State = UpdateState.Failed;
            }
        }

        private async Task UpdateAsync() => await Task.Run(() => Update());

        private static async Task WaitToClose()
        {
            await Task.Delay(2000);
            Application.Current.MainWindow.Close();
        }

        private static void WriteFile(FileInfo fileInfo, FileData fileData)
        {
            var filePath = fileInfo.GetFullPath();
            var fileDir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            File.WriteAllBytes(filePath, fileData.Data);
        }
        #endregion
    }
}