using System.Threading.Tasks;
using System.Windows.Input;


namespace AppBootViewModels
{
    public abstract class AppReadWriteViewModelBase: AppBootViewModelBase
    {
        #region Fields
        protected bool _canLoad = true;
        protected bool _canSave;
        protected ICommand _loadAsyncCommand;
        protected ICommand _saveAsyncCommand;
        #endregion


        #region Abstract
        protected abstract Task LoadAsync();

        protected abstract Task SaveAsync();
        #endregion


        #region  Properties & Indexers
        public virtual bool CanLoad
        {
            get { return _canLoad; }
            private set { SetProperty(ref _canLoad, value); }
        }

        public virtual bool CanSave
        {
            get { return _canSave; }
            private set { SetProperty(ref _canSave, value); }
        }

        public virtual ICommand LoadAsyncCommand
            => GetCommand(ref _loadAsyncCommand, async _ => await LoadAsync(), _ => CanLoad);

        public virtual ICommand SaveAsyncCommand
            => GetCommand(ref _saveAsyncCommand, async _ => await SaveAsync(), _ => CanSave);
        #endregion


        #region Implementation
        protected virtual void SetEnability(bool value)
        {
            CanLoad = CanSave = value;
        }
        #endregion
    }
}