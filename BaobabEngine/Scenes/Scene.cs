using Foster.Framework;
using System;

namespace BaobabEngine.Scenes;

/// <summary>
/// Used to create contained scenes with their own objects and behavior.
///
/// Note:
/// `Update` and `Render` are the proper methods to call when updating and rendering a scene. These enforce safety
/// with potentially trying to use disposed information. However, custom logic for updating and rendering is implemented
/// through `UpdateScene` and `RenderScene`.
/// </summary>
public abstract class Scene: IDisposable
{
    public bool IsDisposed { get; private set; }

    public void Update(float deltaTime)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        UpdateScene(deltaTime);
    }

    public void Render(in Window window)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        RenderScene(window);
    }

    public void Dispose()
    {
        if (IsDisposed) return;
        
        DisposeScene();
        IsDisposed = true;
    }
    
    protected abstract void UpdateScene(float deltaTime);
    protected abstract void RenderScene(Window window);

    protected virtual void DisposeScene() { }
}
