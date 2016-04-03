using System.Threading.Tasks;
using AppBootModels;


namespace AppBootContexts
{
    public interface IFileDataDataAccess
    {
        #region Abstract
        FileData GetFileData(int fileInfoId);
        Task<FileData> GetFileDataAsync(int fileInfoId);
        FileData SaveFileData(FileData fileData);
        Task<FileData> SaveFileDataAsync(FileData fileData);
        #endregion
    }
}