# CSlickRun

CSlickRun is a WPF application designed to make executing applications, opening folders, visiting websites, etc. more seamless by providing a command line you can access with a shortcut where one simply inputs the name of the command to be executed. These commands as well as the UI can be completely customized.
## Features

- Create and edit custom commands with multiple paths and arguments.
- Execute commands from a small commandline.
- Manage application settings, including appearance and behavior.
- Support for global hotkeys to quickly access commands.
- Auto-start functionality to launch the application on system startup.

## Requirements

- .NET 8.0
- Visual Studio 2022 or later

## Installation

1. Clone the repository:
   git clone https://github.com/cgrafbus/CSlickRun.git
2. Open the solution file `CSlickRun.sln` in Visual Studio.
3. Restore NuGet packages:
   dotnet restore
4. Build the solution:
   dotnet build
5. Run the application:
   dotnet run --project CSlickRun


## Usage

### Creating a Command

1. Open the application.
2. Type 'Config' into the Commandline to open the Config-Window
3. Navigate to the "Commands" section.
4. Click "Add" to create a new command.
5. Fill in the command details, including name, paths, arguments, etc.
6. Click "Save" to save the command.

### Executing a Command

1. Open the application.
2. Simply type the name of your command in the commandline

### Managing Settings

1. Open the application.
2. Type 'Config' into the Commandline to open the Config-Window
3. Navigate to the "Settings" section.
4. Adjust the settings as needed, including appearance and behavior.
5. Click "Save" to apply the changes.

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix:
   git checkout -b feature-name
3. Make your changes and commit them:
   git commit -m "Description of your changes"
4. Push your changes to your fork:
   git push origin feature-name
5. Create a pull request with a description of your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/) for MVVM support.
- [Newtonsoft.Json](https://www.newtonsoft.com/json) for JSON serialization.
- [Xceed WPF Toolkit](https://github.com/xceedsoftware/wpftoolkit) for the ColorCanvas
- [SlickRun](https://www.bayden.com/slickrun) for the entire idea

## Contact

For any questions or feedback, please contact [claudio.graf.bus@gmail.com](mailto:claudio.graf.bus@gmail.com).
   
   
