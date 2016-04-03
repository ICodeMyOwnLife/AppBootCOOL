using System.Threading.Tasks;
using AppBootContexts;
using AppBootModels;


namespace AppBootEntityDataAccess
{
    public class FileDataDataAccess: IFileDataDataAccess
    {
        #region Methods
        public FileData GetFileData(int fileInfoId)
        {
            using (var context = new AppBootContext())
            {
                return context.FileDatas.Find(fileInfoId);
            }
        }

        public async Task<FileData> GetFileDataAsync(int fileInfoId)
        {
            using (var context = new AppBootContext())
            {
                return await context.FileDatas.FindAsync(fileInfoId);
            }
        }

        public FileData SaveFileData(FileData fileData)
        {
            using (var context = new AppBootContext())
            {
                var data = context.FileDatas.Find(fileData.FileInfoId);
                if (data != null)
                {
                    data.CopyFrom(fileData, false);
                    context.SaveChanges();
                    return data;
                }

                context.FileDatas.Add(fileData);
                context.SaveChanges();
                return fileData;
            }
        }

        public async Task<FileData> SaveFileDataAsync(FileData fileData)
        {
            using (var context = new AppBootContext())
            {
                var data = await context.FileDatas.FindAsync(fileData.FileInfoId);
                if (data != null)
                {
                    data.CopyFrom(fileData, false);
                    await context.SaveChangesAsync();
                    return data;
                }

                context.FileDatas.Add(fileData);
                await context.SaveChangesAsync();
                return fileData;
            }
        }
        #endregion
    }
}