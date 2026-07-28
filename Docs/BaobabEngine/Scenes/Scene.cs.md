# [Scene](../../../BaobabEngine/Scenes/Scene.cs)
An abstract class representing a single scene in a game with it own initialization, updating, rendering, and destruction logic.

Scene inherits from `IDisposable`!

## Methods
- `public void Update(float deltaTime)` - Called externally to update the scene. It also throws an error if the scene is disposed.
- `public void Render(in Window window)` - Called externally to draw the scene. It also thrown an error if the scene is disposed.
- `public void Dispose()` - Disposes of the scene.
- `protected abstract void UpdateScene(float deltaTime)` - This is where the user will define their update logic.
- `protected abstrct void RenderScene(Window window)` - This is where the user will define their rendering logic.
- `protected virtual void DisposeScene()` - This is where the user defines the disposal protocol of the scene.