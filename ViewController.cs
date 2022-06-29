using ObjCRuntime;

namespace xammacmvvm;

public partial class ViewController : NSViewController
{
    ViewModel _model;
    ObservableNSObject _vm;
    NSTextField _label;

    protected ViewController(NativeHandle handle) : base(handle)
    {
        _model = new ViewModel();
        _vm = UniqueAssociation.Instance.GetOrCreate<ObservableNSObject>(_model);

    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        _label = NSTextField.CreateLabel("Initial");
        View = _label;

        _ = LoopUpdateLabel();

        // Do any additional setup after loading the view.
    }

    async Task LoopUpdateLabel()
    {
        for (int i = 0; i < 10; ++i)
        {
            await Task.Delay(1000);

            _model.Label = new NSString($"{i}");
        }
    }

    public override void ViewDidAppear()
    {
        base.ViewDidAppear();

        _label.Bind(new NSString("value"), _vm, nameof(ViewModel.Label), null);
    }

    public override void ViewDidDisappear()
    {
        _label.Unbind(new NSString("value"));

        base.ViewDidDisappear();
    }

    public override NSObject RepresentedObject
    {
        get => base.RepresentedObject;
        set
        {
            base.RepresentedObject = value;

            // Update the view, if already loaded.
        }
    }
}
