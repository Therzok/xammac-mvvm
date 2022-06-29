using System;
namespace xammacmvvm
{
    [Register(nameof(ObservableNSObject))]
	public class ObservableNSObject : NSObject
	{
        readonly ViewModel _model;

        public ObservableNSObject(ViewModel model)
        {
            _model = model;
            model.PropertyChanging += Model_PropertyChanging;
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            WillChangeValue(e.PropertyName!);
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DidChangeValue(e.PropertyName!);
        }

        // This is called by objc when it tries to look up a property that is not defined.
        // Alternatively, we can SetValueForKey on all the viewmodel properties in the constructor and PropertyChanged.
        // and we don't have to do this on every lookup. It's possible the latter approach is more source-generation friendly.
        public override NSObject ValueForUndefinedKey(NSString key)
        {
            if (key == nameof(_model.Label))
            {
                return new NSString(_model.Label);
            }
            return base.ValueForUndefinedKey(key);
        }

        protected override void Dispose(bool disposing)
        {
            _model.PropertyChanging -= Model_PropertyChanging;
            _model.PropertyChanged -= Model_PropertyChanged;

            base.Dispose(disposing);
        }
    }
}

