using System.Data.Entity;


namespace AppBootContexts
{
    public class AppBootContextInitializer: DropCreateDatabaseAlways<AppBootContext>
    {
        #region Override
        protected override void Seed(AppBootContext context)
        {
            base.Seed(context);
        }
        #endregion
    }
}