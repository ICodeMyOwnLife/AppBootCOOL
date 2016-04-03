using System.Threading.Tasks;
using AppBootModels;


namespace AppBootContexts
{
    public interface IAppFileDataAccess
    {
        #region Abstract
        void DeleteFile(FileInfo file);
        Task DeleteFileAsync(FileInfo file);
        FileInfo[] GetFiles(int applicationId);
        Task<FileInfo[]> GetFilesAsync(int applicationId);
        FileInfo SaveFile(FileInfo file);
        Task<FileInfo> SaveFileAsync(FileInfo file);
        #endregion
    }
}