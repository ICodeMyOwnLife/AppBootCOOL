using CB.Model.Common;


namespace AppBootModels
{
    public class FileUpdate: ObservableObject
    {
        #region Fields
        private FileInfo _info;
        private UpdateState _state = UpdateState.Pending;
        #endregion


        #region  Properties & Indexers
        public FileInfo Info
        {
            get { return _info; }
            set { SetProperty(ref _info, value); }
        }

        public UpdateState State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
        #endregion
    }
}