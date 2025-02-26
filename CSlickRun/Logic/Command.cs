using CSlickRun.UI.Windows;
using System.Diagnostics;
using System.Windows;

namespace CSlickRun.Logic;

public class Command
{
    public Command(string name, List<CommandPath> paths, string note)
    {
        Name = name;
        Paths = paths;
        Note = note;
    }

    public Command()
    {
    }

    public string Name { get; set; }
    public List<CommandPath>? Paths { get; set; }
    public string Note { get; set; }

    public void Execute()
    {
        if (Name == "Config")
        {
            var confWindow = Application.Current.Windows
                .OfType<ConfigWindow>()
                .FirstOrDefault();

            if (confWindow != null)
            {
                confWindow.Activate();
                return;
            }
            var configWindow = new ConfigWindow();
            configWindow.Show();
            return;
        }

        if (Paths != null)
        {
            foreach (var path in Paths)
            {
                var process = new Process();
                var info = new ProcessStartInfo();
                info.FileName = path.Path;
                info.Arguments = path.Argument;
                info.UseShellExecute = true;

                process.StartInfo = info;
                process.Start();
            }
        }
    }

}