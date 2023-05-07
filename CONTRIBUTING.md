## Contributing

1. Create an [issue](https://github.com/joakimskoog/AnApiOfIceAndFire/issues) or find an [issue](https://github.com/joakimskoog/AnApiOfIceAndFire/issues) that you would like to fix.
2. Fork this repository
3. Implement the new functionality/bug fix.
4. Write tests for your code
5. Submit a pull request that is up to date with the master branch
6. Watch us accept it and deploy it to production

## Worth noting
* All contributions are welcome, it doesn't matter if it's a big feature or a small data fix.
* The data files are located [here](https://github.com/joakimskoog/AnApiOfIceAndFire/tree/master/data)

## How to set up local development
1. Fork this repository
2. Git clone your fork (for more information about forking and cloning refer [here](https://docs.github.com/en/get-started/quickstart/contributing-to-projects))
3. Download and install [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/). .NET 6.0 requires the 2022 version.
4. Download and install [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). On Windows, to check which architecture version you need to download (x86, x64, Arm64) search for and open System Information in the Windows search bar and then look for System Type.
5. Right click on the `AnApiOfIceAndFire.sln` file in the top level directory of the project and open with Visual Studio 2022.
6. Once the project is opened in Visual Studio, click Run in the top menu bar or click F5. This will build and run the project. If you encounter errors during the build process, make sure you followed the steps above to install the correct tools. Otherwise, try right clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages". 
7. You should now be able to access the project running locally on https://localhost:7007. If you encounter an error message about your connection not being secure and you are running Chrome, refer to [this](https://stackoverflow.com/questions/44066709/your-connection-is-not-private-neterr-cert-common-name-invalid) Stack Overflow post.
