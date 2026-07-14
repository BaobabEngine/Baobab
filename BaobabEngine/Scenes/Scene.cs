using Foster.Framework;
using System;

namespace BaobabEngine.Scenes;

/// <summary>
/// Used to create contained scenes with their own objects and behavior.
/// </summary>
public abstract class Scene: IDisposable
{
    public abstract void Initialize();

    public abstract void Update();

    public abstract void Render(in Batcher batcher, in Window window);

    public abstract void Dispose();
}