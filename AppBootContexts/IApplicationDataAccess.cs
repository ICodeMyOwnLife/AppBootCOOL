using System.Threading.Tasks;
using AppBootModels;


namespace AppBootContexts
{
    public interface IApplicationDataAccess
    {
        #region Abstract
        void DeleteApplication(ApplicationInfo application);
        Task DeleteApplicationAsync(ApplicationInfo application);
        ApplicationInfo GetApplication(int id);
        Task<ApplicationInfo> GetApplicationAsync(int applicationId);
        ApplicationInfo[] GetApplications();
        Task<ApplicationInfo[]> GetApplicationsAsync();
        ApplicationInfo SaveApplication(ApplicationInfo application);
        Task<ApplicationInfo> SaveApplicationAsync(ApplicationInfo application);
        #endregion
    }
}