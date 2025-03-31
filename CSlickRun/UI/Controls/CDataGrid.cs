using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSlickRun.UI.Controls;

public class CDataGrid : DataGrid
{
    public static readonly DependencyProperty OnSizeChangedCommandProperty =
        DependencyProperty.Register(nameof(OnSizeChangedCommand), typeof(ICommand), typeof(CDataGrid));

    public ICommand OnSizeChangedCommand
    {
        get => (ICommand)GetValue(OnSizeChangedCommandProperty);
        set => SetValue(OnSizeChangedCommandProperty, value);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        OnSizeChangedCommand.Execute(this);

    }
}