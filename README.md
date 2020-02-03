# verySmallOS
A very small OS built using C#, CoreRT and CosmosOS

## How does it work
### Technologies used
* [CosmosOS](https://www.gocosmos.org/): An amazing framework for creating low-level code using C#
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/): Dubious language choice for a kernel but it definitely can be done
* [Zerosharp](https://github.com/MichalStrehovsky/zerosharp) / [CoreRT](https://github.com/dotnet/corert): A bootloader (Zerosharp) and bare-metal .NET runtime (CoreRT). Trying to make this work as a bootloader instead of Syslinux (what is currently used)

### Platforms used/tested
* [VMWare](https://www.vmware.com/): WorksOnMyMachineTM
* [VS 2017](https://visualstudio.microsoft.com/vs/older-downloads/): Pretty good IDE, works well with CosmosOS

## How to use
The commands supported work pretty similar to their POSIX equivalents. If you want to write to a file, use the ```>>``` operator as such:

![write](https://user-images.githubusercontent.com/54983399/73618253-a3db9600-4626-11ea-87f7-614c474a9e39.PNG)

<sub><sup>Don't worry, a proper text editor is planned</sup></sub>

## How to build
It's pretty straight forward if you get [CosmosOS](https://www.gocosmos.org/) running

## Screenshots 
![help](https://user-images.githubusercontent.com/54983399/73618013-a4732d00-4624-11ea-9af8-5535bacc4570.PNG)
![start](https://user-images.githubusercontent.com/54983399/73618015-a4732d00-4624-11ea-84e8-fb9e4aebb0cb.PNG)
![file_operations](https://user-images.githubusercontent.com/54983399/73618016-a4732d00-4624-11ea-836b-a96de4f33740.PNG)
![graphicalMode_cursor](https://user-images.githubusercontent.com/54983399/73618011-a3da9680-4624-11ea-9864-5503d1af2505.PNG)
![graphicalMode_shapes](https://user-images.githubusercontent.com/54983399/73618012-a3da9680-4624-11ea-8059-cd7d7a190e2e.PNG)

## License
Licensed under BSD. For full license see: [`LICENSE`](LICENSE).
