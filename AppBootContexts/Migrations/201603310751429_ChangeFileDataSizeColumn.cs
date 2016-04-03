namespace AppBootContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFileDataSizeColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileData", "Size", c => c.Long(nullable: false));
            AlterStoredProcedure(
                "dbo.InsertFileData",
                p => new
                    {
                        FileInfoId = p.Int(),
                        Data = p.Binary(),
                        Hash = p.Binary(maxLength: 16, fixedLength: true),
                        Size = p.Long(),
                    },
                body:
                    @"INSERT [dbo].[FileData]([FileInfoId], [Data], [Hash], [Size])
                      VALUES (@FileInfoId, @Data, @Hash, @Size)"
            );
            
            AlterStoredProcedure(
                "dbo.UpdateFileData",
                p => new
                    {
                        FileInfoId = p.Int(),
                        Data = p.Binary(),
                        Hash = p.Binary(maxLength: 16, fixedLength: true),
                        Size = p.Long(),
                    },
                body:
                    @"UPDATE [dbo].[FileData]
                      SET [Data] = @Data, [Hash] = @Hash, [Size] = @Size
                      WHERE ([FileInfoId] = @FileInfoId)"
            );
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileData", "Size", c => c.Int(nullable: false));
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
