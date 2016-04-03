using System;
using System.Data.Entity;
using AppBootContexts;
using AppBootModels;


namespace AppBootTestConsole
{
    internal class Program
    {
        #region Implementation
        private static void Main()
        {
            Database.SetInitializer(new AppBootContextInitializer());
            using (var context = new AppBootContext())
            {
                var app = new ApplicationInfo
                {
                    Name = "Test",
                    Directory = "Test",
                    Description = "Test app"
                };
                app.Files.Add(new FileInfo(@"D:\a.txt", @"D:\"));
                app.Files.Add(new FileInfo(@"D:\b.txt", @"D:\"));
                app.Files.Add(new FileInfo(@"D:\c.txt", @"D:\"));

                context.Applications.Add(app);
                context.SaveChanges();
            }
            Console.ReadLine();
        }
        #endregion
    }
}