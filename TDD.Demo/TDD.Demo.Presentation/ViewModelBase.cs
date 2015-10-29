using System.ComponentModel;
using TDD.Demo.Domain;

namespace TDD.Demo.Presentation
{
    public abstract class ViewModelBase<TModel> : NotificationObject, IViewModel<TModel> where TModel : EntityBase
    {
        protected ViewModelBase()
        {
            PropertyChanged += OnViewModelPropertyChanged;
        }

        private string _warningMessage;

        public string WarningMessage
        {
            get { return _warningMessage; }
            set
            {
                if (Equals(_warningMessage, value))
                {
                    return;
                }

                _warningMessage = value;
                RaisePropertyChanged();
            }
        }

        private TModel _model;

        public TModel Model
        {
            get { return _model; }
            set
            {
                if (Equals(_model, value))
                {
                    return;
                }

                if (_model != null)
                {
                    _model.PropertyChanged -= ModelOnPropertyChanged;
                }

                _model = value;
                _model.PropertyChanged += ModelOnPropertyChanged;

                RaisePropertyChanged();
            }
        }

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        protected virtual void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
