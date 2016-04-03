using System.Data.Entity.ModelConfiguration;
using AppBootModels;


namespace AppBootContexts
{
    public class FileVersionMap: ComplexTypeConfiguration<FileVersion>
    {
        public FileVersionMap()
        {
            Property(v => v.Major).HasColumnName("MajorVersion");
            Property(v => v.Minor).HasColumnName("MinorVersion");
            Property(v => v.Build).HasColumnName("BuildVersion");
            Property(v => v.Revision).HasColumnName("RevisionVersion");
            Ignore(v => v.Version);
        }
    }
}