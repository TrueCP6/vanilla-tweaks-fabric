# vanilla-tweaks-fabric
TODO: add description

TODO: come up with better name

# How to contribute
vanilla-tweaks-fabric is made with Blazor, which is essentially C# integrated with HTML and CSS. This project runs on WebAssembly in the browser.

## Opening the project
### Visual Studio Community
[Download Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) and in the install process select .NET Core cross-platform development under workloads. Once done installing you can open the project by cloning the repository and opening the `CVESite2.sln` file.

### Visual Studio Code
[How to set up your Blazor development enviroment](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/3-exercise-configure-enviromnent).

## Running the project
In both Visual Studio Code and Visual Studio Community press F5 or the run button to open a browser window with the site open.

## Improving the look of the site
The main page is `Pages/Index.razor`. It implements the components (these can be thought of as methods for html) found in the `Shared` directory of the project. For styling purposes you can mostly ignore the C# code and write normal HTML. The CSS is completely standard and can be found in `wwwroot/css/app.cs`.

## Further questions
If you have any further questions dm or mention me in fabricord in Discord at TrueCP5#5894.