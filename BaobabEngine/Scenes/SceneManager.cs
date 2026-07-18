using Foster.Framework;
using System;

namespace BaobabEngine.Scenes;

public class SceneManager(Scene startingScene)
{
    public Type Type => _currentScene.GetType();

    public bool IsDisposed => _currentScene.IsDisposed;
    
    private Scene _currentScene = startingScene;

    public void Update(float deltaTime) => _currentScene.Update(deltaTime);
    public void Render(in Window window) => _currentScene.Render(window);
    public void Dispose() => _currentScene.Dispose();

    /// <summary>
    /// Disposes of the current scene and switches to a new scene.
    /// </summary>
    /// <param name="newScene">The scene that you want to switch to.</param>
    public void SwitchScene(Scene newScene)
    {
        _currentScene.Dispose();
        _currentScene = newScene;
    }
}