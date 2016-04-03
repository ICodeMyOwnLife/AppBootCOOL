using System.Data.Entity;
using AppBootModels;


namespace AppBootContexts
{
    public class AppBootContext: DbContext
    {
        #region  Constructors & Destructor
        public AppBootContext(): base("name=AppBootContext") { }
        #endregion


        #region  Properties & Indexers
        public DbSet<ApplicationInfo> Applications { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<FileInfo> FileInfos { get; set; }
        #endregion


        #region Override
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ApplicationInfoMap());
            modelBuilder.Configurations.Add(new FileInfoMap());
            modelBuilder.Configurations.Add(new FileDataMap());
            modelBuilder.Configurations.Add(new FileVersionMap());
        }
        #endregion
    }
}