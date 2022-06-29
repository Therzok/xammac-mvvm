using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace xammacmvvm;

public class ViewModel : INotifyPropertyChanged, INotifyPropertyChanging
{
    string _label = string.Empty;
    public string Label
    {
        get => _label;
        set
        {
            OnPropertyChanging();
            _label = value;
            OnPropertyChanged();
        }
    }

    void OnPropertyChanging([CallerMemberName] string? callerName = null) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(callerName));

    void OnPropertyChanged([CallerMemberName] string? callerName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;
}

