using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AppBootContexts;
using AppBootModels;


namespace AppBootEntityDataAccess
{
    public class ApplicationDataAccess: IApplicationDataAccess
    {
        #region Methods
        public void DeleteApplication(ApplicationInfo application)
        {
            using (var context = new AppBootContext())
            {
                context.Applications.Remove(application);
                context.SaveChanges();
            }
        }

        public async Task DeleteApplicationAsync(ApplicationInfo application)
        {
            using (var context = new AppBootContext())
            {
                context.Applications.Remove(application);
                await context.SaveChangesAsync();
            }
        }

        public ApplicationInfo GetApplication(int applicationId)
        {
            using (var context = new AppBootContext())
            {
                return context.Applications.Find(applicationId);
            }
        }

        public async Task<ApplicationInfo> GetApplicationAsync(int applicationId)
        {
            using (var context = new AppBootContext())
            {
                return await context.Applications.FindAsync(applicationId);
            }
        }

        public ApplicationInfo[] GetApplications()
        {
            using (var context = new AppBootContext())
            {
                context.Applications.Load();
                return context.Applications.Local.ToArray();
            }
        }

        public async Task<ApplicationInfo[]> GetApplicationsAsync()
        {
            using (var context = new AppBootContext())
            {
                await context.Applications.LoadAsync();
                return context.Applications.Local.ToArray();
            }
        }

        public ApplicationInfo SaveApplication(ApplicationInfo application)
        {
            using (var context = new AppBootContext())
            {
                var app = context.Applications.Find(application.Id);
                if (app != null)
                {
                    app.CopyFrom(application, false);
                    context.SaveChanges();
                    return app;
                }

                context.Applications.Add(application);
                context.SaveChanges();
                return application;
            }
        }

        public async Task<ApplicationInfo> SaveApplicationAsync(ApplicationInfo application)
        {
            using (var context = new AppBootContext())
            {
                var app = await context.Applications.FindAsync(application.Id);
                if (app != null)
                {
                    app.CopyFrom(application, false);
                    await context.SaveChangesAsync();
                    return app;
                }

                context.Applications.Add(application);
                await context.SaveChangesAsync();
                return application;
            }
        }
        #endregion
    }
}