using System.ComponentModel;
using System.IO;
using System.Windows;
using CSlickRun.Logic;
using CSlickRun.Logic.Helpers;
using UserControl = System.Windows.Controls.UserControl;

namespace CSlickRun.UI.Controls;

/// <summary>
///     Interaction logic for ToastMessage.xaml
/// </summary>
public partial class ToastMessage : UserControl
{
    public ToastMessage()
    {
        InitializeComponent();
        Visibility = Visibility.Collapsed;
        Opacity = 0;
    }

    public async Task<bool> Show(string toastMessage, ToastMessageType type)
    {
        var toastMessageTypeDescription = type.GetAttribute<DescriptionAttribute>()?.Description;
        var toastMessageImagePath = Path.Combine(Global.ImagePath, toastMessageTypeDescription ?? "Info") + ".png";

        ToastMessageImage.Source = UIHelper.GetImageFromPath(toastMessageImagePath);
        ToastMessageText.Text = toastMessage;
        Visibility = Visibility.Visible;
        Opacity = 1;

        await Task.Delay(400);

        while (Opacity > 0)
        {
            await Task.Delay(40);
            Opacity -= 0.05;
        }

        Visibility = Visibility.Collapsed;
        return true;
    }
}