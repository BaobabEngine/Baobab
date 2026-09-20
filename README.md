# [Baobâb](https://github.com/BaobabEngine/Baobab)
Baobab is a custom engine made with C# to merge the control of frameworks with the features of a proper game engine.
This project is currently in early development, but a usable, initial build has been published to Nuget. 
You can use it by running `dotnet package add BaobabEngine.Baobab` in your project.

## What is Baobab
Baobab is a C# class library built off the *[Foster](https://github.com/FosterFramework/Foster)* framework. 
Many classes and methods are provided to help developers build games on their own.

Baobab is distributed as a Nuget package for several reasons. The most notable benefit is that the engine does not become reliant on a built-in GUI editor.
Developers don't have to learn another editor, and can just use the environment they are comfortable with. 
As a result, context switching is minimized due to the ability to use a common code editor like *VS Code* where all work can be kept central and organized.
Nuget packages are typically lightweight. Using Nuget minimizes friction to install the engine and doesn't require formal installation.
Developers who just want to try Baobab don't have to worry about a long or complicated installation process.

Being built off *Foster*, Baobab tools are interchangeable with their already present structure. 
Developers can pick and choose between *Foster* features and engine features freely. In addition, developers can branch or modify Baobab features to tune the system to their likings.

With a code-first design, developers are able to freely decide how they want to build their systems. 
The engine does not force a rigid structure for implementations like sprite creation, asset loading, audio playing, etc.
Developers are also capable of building their own sub-engines or libraries to provide extra features or template the tools they use the most.

Baobab is completely free and open source. You will NEVER have to pay any fees to use this engine. Baobab is licensed under the MIT license, which allows community members to continue to improve provided features, regardless of whether the engine is actively maintained or not.

Baobab is designed for developers who love control and freedom, without sacrificing quality of life.

## How to use Baobab
Currently, no abstractions are provided for the *Foster* game loop. You can view *Foster's* ["Shape"](https://github.com/FosterFramework/Samples/blob/main/Shapes/Program.cs) example to get an understanding of how Foster's game loop works.
Essentially, the setup for a *Foster* game is to implement a class that inherits from the `Foster.Framework.App` class.
This class then requires the implementation of the following methods:
1. Startup - Ran when the program starts.
2. Shutdown - Run before the program ends.
3. Update - Ran every frame to update game data.
4. Render - Ran every frame after the `Update` method to actually draw sprites.

*Note that these methods require overrides, and don't have default functionality.*
More features are being planned to make the game loop more convenient.

Once you have a game class setup, you will create an instance of the class and use the `.Run()` method to start the game.

## Roadmap
These are the next features that are planned for Baobab:
1. GUI library
2. Audio support
3. More quality of life features

The primary source for feature ideas will be requests. Feature requests can be submitted as described under the *[Contributions](#contributions)* section.

## Contributions
This is a list of very appreciated contributions:
* Code help
* Documentation support
* Feature requests


If you want to suggest any code or documentation adjustments, you can submit a pull request to the GitHub [repository](https://GitHub.com/BaobabEngine/Baobab).

Feature requests can be submitted as an issue to the repository.

Any major changes must first be discussed and approved. Note that not all features may be merged.

Thank you to all who choose to contribute!

## License and Credit
Baobab is produced under the [MIT license](LICENSE).

Be sure to checkout the *[Foster](https://GitHub.com/FosterFramework/Foster)* framework. This project wouldn't be possible without the amazing team working on this framework.
