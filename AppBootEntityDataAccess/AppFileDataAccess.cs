using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AppBootContexts;
using AppBootModels;


namespace AppBootEntityDataAccess
{
    public class AppFileDataAccess: IAppFileDataAccess
    {
        #region Methods
        public void DeleteFile(FileInfo file)
        {
            using (var context = new AppBootContext())
            {
                context.FileInfos.Remove(file);
                context.SaveChanges();
            }
        }

        public async Task DeleteFileAsync(FileInfo file)
        {
            using (var context = new AppBootContext())
            {
                context.FileInfos.Remove(file);
                await context.SaveChangesAsync();
            }
        }

        public FileInfo[] GetFiles(int applicationId)
        {
            using (var context = new AppBootContext())
            {
                return context.FileInfos.Where(f => f.ApplicationId == applicationId).ToArray();
            }
        }

        public async Task<FileInfo[]> GetFilesAsync(int applicationId)
        {
            using (var context = new AppBootContext())
            {
                return await context.FileInfos.Where(f => f.ApplicationId == applicationId).ToArrayAsync();
            }
        }

        public FileInfo SaveFile(FileInfo fileInfo)
        {
            using (var context = new AppBootContext())
            {
                var file = context.FileInfos.Find(fileInfo.Id);
                if (file != null)
                {
                    file.CopyFrom(fileInfo, false);
                    context.SaveChanges();
                    return file;
                }

                context.FileInfos.Add(fileInfo);
                context.SaveChanges();
                return fileInfo;
            }
        }

        public async Task<FileInfo> SaveFileAsync(FileInfo fileInfo)
        {
            using (var context = new AppBootContext())
            {
                var file = await context.FileInfos.FindAsync(fileInfo.Id);
                if (file != null)
                {
                    file.CopyFrom(fileInfo, false);
                    await context.SaveChangesAsync();
                    return file;
                }

                context.FileInfos.Add(fileInfo);
                await context.SaveChangesAsync();
                return fileInfo;
            }
        }
        #endregion
    }
}