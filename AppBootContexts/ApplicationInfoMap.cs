using System.Data.Entity.ModelConfiguration;
using AppBootModels;


namespace AppBootContexts
{
    public class ApplicationInfoMap: EntityTypeConfiguration<ApplicationInfo>
    {
        #region  Constructors & Destructor
        public ApplicationInfoMap()
        {
            ToTable("Application");

            Property(p => p.Id).HasColumnOrder(0);
            Property(p => p.Name).HasColumnOrder(1).HasMaxLength(128).IsUnicode(false).IsRequired();
            Property(p => p.Directory).HasColumnOrder(2).HasMaxLength(512).IsUnicode(false).IsRequired();
            Property(p => p.Description).HasColumnOrder(3).HasMaxLength(1024).IsUnicode(false);
            Property(p => p.CreatedOn).HasColumnOrder(4);
            Property(p => p.ModifiedOn).HasColumnOrder(5);

            HasMany(a => a.Files).WithRequired(f => f.Application).HasForeignKey(f => f.ApplicationId)
                                 .WillCascadeOnDelete();

            MapToStoredProcedures(s =>
            {
                s.Insert(i => i.HasName("InsertApplication", "dbo"));
                s.Update(u => u.HasName("UpdateApplication", "dbo"));
                s.Delete(d => d.HasName("DeleteApplication", "dbo"));
            });
        }
        #endregion
    }
}