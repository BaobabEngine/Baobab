# [Scene Manger](../../../BaobabEngine/Scenes/SceneManager.cs)
Used to manage multiple scenes and cleany switch from one scene to the next.

## Constructor
- `public SceneManager(Scene startingScene)`

## Properties
- `public Type Type` - Returns the type of the current scene.
- `public bool IsDisposed` - Returns if the current scene is disposed.
- `private Scene _currentScene` - The current scene in memory.

## Methods
- `public void Update(float deltaTime)` - Updates the current scene.
- `public void Render(in Window window)` - Draws the current scene.
- `public void Dispose()` - Disposes of the current scene.
- `public void SwitchScene(Scene newScene)` - Disposes of the current scene and switches to the next one.