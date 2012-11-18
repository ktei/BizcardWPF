using Caliburn.Micro;

namespace LiteApp.Bizcard.ViewModels
{
    public abstract class EditableViewModel : PropertyChangedBase
    {
        public const string IS_DIRTY = "IsDirty";

        protected bool _isDirty = false;

        public virtual bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    NotifyOfPropertyChange(() => IsDirty);
                }
            }
        }
    }
}
