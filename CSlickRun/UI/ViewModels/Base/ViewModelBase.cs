using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.Logic;
using Newtonsoft.Json;

namespace CSlickRun.UI.ViewModels.Base;

/// <summary>
/// Viewmodel-Base-Class
/// </summary>
public partial class ViewModelBase : ObservableObject
{
    public ViewModelBase()
    {
    }

    protected async Task OnSaveFinished()
    {
        await UIHelper.ShowToastMessage("Save successful", ToastMessageType.Info);
    }
}