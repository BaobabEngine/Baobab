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

    public void SwitchScene(Scene newScene)
    {
        _currentScene.Dispose();
        _currentScene = newScene;
    }
}