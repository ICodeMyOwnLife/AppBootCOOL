namespace AppBootContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                        Directory = c.String(nullable: false, maxLength: 512, unicode: false),
                        Description = c.String(maxLength: 1024, unicode: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                        Directory = c.String(nullable: false, maxLength: 512, unicode: false),
                        Extension = c.String(nullable: false, maxLength: 8, unicode: false),
                        MajorVersion = c.Int(),
                        MinorVersion = c.Int(),
                        BuildVersion = c.Int(),
                        RevisionVersion = c.Int(),
                        Description = c.String(maxLength: 1024, unicode: false),
                        IsInit = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.FileData",
                c => new
                    {
                        FileInfoId = c.Int(nullable: false),
                        Data = c.Binary(nullable: false),
                        Hash = c.Binary(nullable: false, maxLength: 16, fixedLength: true),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileInfoId)
                .ForeignKey("dbo.FileInfo", t => t.FileInfoId, cascadeDelete: true)
                .Index(t => t.FileInfoId);
            
            CreateStoredProcedure(
                "dbo.InsertApplication",
                p => new
                    {
                        CreatedOn = p.DateTime(defaultValueSql: "NULL"),
                        Description = p.String(maxLength: 1024, unicode: false),
                        Directory = p.String(maxLength: 512, unicode: false),
                        ModifiedOn = p.DateTime(defaultValueSql: "NULL"),
                        Name = p.String(maxLength: 128, unicode: false),
                    },
                body:
                    @"INSERT [dbo].[Application]([Name], [Directory], [Description], [CreatedOn], [ModifiedOn])
                      VALUES (@Name, @Directory, @Description, GETDATE(), GETDATE())
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Application]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Application] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.UpdateApplication",
                p => new
                    {
                        Id = p.Int(),
                        CreatedOn = p.DateTime(defaultValueSql: "NULL"),
                        Description = p.String(maxLength: 1024, unicode: false),
                        Directory = p.String(maxLength: 512, unicode: false),
                        ModifiedOn = p.DateTime(defaultValueSql: "NULL"),
                        Name = p.String(maxLength: 128, unicode: false),
                    },
                body:
                    @"UPDATE [dbo].[Application]
                      SET [Name] = @Name, [Directory] = @Directory, [Description] = @Description, [ModifiedOn] = GETDATE()
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DeleteApplication",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Application]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.InsertFileInfo",
                p => new
                    {
                        ApplicationId = p.Int(),
                        CreatedOn = p.DateTime(defaultValueSql: "NULL"),
                        Description = p.String(maxLength: 1024, unicode: false),
                        Directory = p.String(maxLength: 512, unicode: false),
                        Extension = p.String(maxLength: 8, unicode: false),
                        IsInit = p.Boolean(),
                        ModifiedOn = p.DateTime(defaultValueSql: "NULL"),
                        Name = p.String(maxLength: 128, unicode: false),
                        BuildVersion = p.Int(),
                        MajorVersion = p.Int(),
                        MinorVersion = p.Int(),
                        RevisionVersion = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[FileInfo]([Name], [Directory], [Extension], [MajorVersion], [MinorVersion], [BuildVersion], [RevisionVersion], [Description], [IsInit], [CreatedOn], [ModifiedOn], [ApplicationId])
                      VALUES (@Name, @Directory, @Extension, @MajorVersion, @MinorVersion, @BuildVersion, @RevisionVersion, @Description, @IsInit, GETDATE(), GETDATE(), @ApplicationId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[FileInfo]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[FileInfo] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.UpdateFileInfo",
                p => new
                    {
                        Id = p.Int(),
                        ApplicationId = p.Int(),
                        CreatedOn = p.DateTime(defaultValueSql: "NULL"),
                        Description = p.String(maxLength: 1024, unicode: false),
                        Directory = p.String(maxLength: 512, unicode: false),
                        Extension = p.String(maxLength: 8, unicode: false),
                        IsInit = p.Boolean(),
                        ModifiedOn = p.DateTime(defaultValueSql: "NULL"),
                        Name = p.String(maxLength: 128, unicode: false),
                        BuildVersion = p.Int(),
                        MajorVersion = p.Int(),
                        MinorVersion = p.Int(),
                        RevisionVersion = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[FileInfo]
                      SET [Name] = @Name, [Directory] = @Directory, [Extension] = @Extension, [MajorVersion] = @MajorVersion, [MinorVersion] = @MinorVersion, [BuildVersion] = @BuildVersion, [RevisionVersion] = @RevisionVersion, [Description] = @Description, [IsInit] = @IsInit, [CreatedOn] = GETDATE(), [ModifiedOn] = GETDATE(), [ApplicationId] = @ApplicationId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.DeleteFileInfo",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[FileInfo]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.InsertFileData",
                p => new
                    {
                        FileInfoId = p.Int(),
                        Data = p.Binary(),
                        Hash = p.Binary(maxLength: 16, fixedLength: true),
                        Size = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[FileData]([FileInfoId], [Data], [Hash], [Size])
                      VALUES (@FileInfoId, @Data, @Hash, @Size)"
            );
            
            CreateStoredProcedure(
                "dbo.UpdateFileData",
                p => new
                    {
                        FileInfoId = p.Int(),
                        Data = p.Binary(),
                        Hash = p.Binary(maxLength: 16, fixedLength: true),
                        Size = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[FileData]
                      SET [Data] = @Data, [Hash] = @Hash, [Size] = @Size
                      WHERE ([FileInfoId] = @FileInfoId)"
            );
            
            CreateStoredProcedure(
                "dbo.DeleteFileData",
                p => new
                    {
                        FileInfoId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[FileData]
                      WHERE ([FileInfoId] = @FileInfoId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.DeleteFileData");
            DropStoredProcedure("dbo.UpdateFileData");
            DropStoredProcedure("dbo.InsertFileData");
            DropStoredProcedure("dbo.DeleteFileInfo");
            DropStoredProcedure("dbo.UpdateFileInfo");
            DropStoredProcedure("dbo.InsertFileInfo");
            DropStoredProcedure("dbo.DeleteApplication");
            DropStoredProcedure("dbo.UpdateApplication");
            DropStoredProcedure("dbo.InsertApplication");
            DropForeignKey("dbo.FileInfo", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.FileData", "FileInfoId", "dbo.FileInfo");
            DropIndex("dbo.FileData", new[] { "FileInfoId" });
            DropIndex("dbo.FileInfo", new[] { "ApplicationId" });
            DropTable("dbo.FileData");
            DropTable("dbo.FileInfo");
            DropTable("dbo.Application");
        }
    }
}
