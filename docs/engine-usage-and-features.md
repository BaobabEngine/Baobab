# Baobab Engine

Baobab is a lightweight C# game engine built on top of the Foster framework. It is designed to keep the feel of a proper game engine while preserving a code-first workflow: you can build your own systems, scenes, sprites, and asset pipelines without being locked into a GUI-driven editor. The project is distributed as a NuGet package and targets a straightforward C# development experience in editors like VS Code or Visual Studio.

## What Baobab gives you

Baobab focuses on the parts of a game engine that are useful for direct coding workflows:

- Scene management for switching and isolating runtime state
- Sprite and animation support built on Foster textures and subtextures
- Camera transforms for position, zoom, and rotation
- Collision helpers for bounding boxes and circles
- Timing utilities for event-driven timers
- Atlas generation for sprite sheets and Aseprite assets

This makes the engine a good fit for developers who want a small, controllable foundation instead of a fully opinionated editor-first engine.

## Core architecture

The engine is intentionally compact. The main runtime pieces live under the `BaobabEngine` project and are organized by responsibility:

```text
BaobabEngine/
  Collisions/      collision primitives and intersection checks
  Graphics/        sprites, animation, cameras, atlas generation
  Scenes/          scene abstraction and scene switching
  Time/            timer utilities
  BaobabEngine.csproj
```

### Scenes

The scene system is built around an abstract `Scene` class that exposes `Update` and `Render` methods and enforces disposal safety via `ObjectDisposedException.ThrowIf(...)` before executing scene logic.

```csharp
public abstract class Scene : IDisposable
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
}
```

This lets developers create isolated game states and swap between them through `SceneManager`.

It is also possible to switch and manage scenes manually, but the scene manager is intended to make this more convenient.

### Scene manager

`SceneManager` keeps a reference to the current scene and exposes `Update`, `Render`, `Dispose`, and `SwitchScene` methods:

```csharp
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
```

This is a simple state-management layer without a lot of framework ceremony.

### Graphics and sprites

The graphics layer includes:

- `Sprite` for drawing a `Subtexture` with scale and rotation
- `AnimatedSprite` for frame-based animation playback
- `Animation` for a set of frames and frame delay
- `Camera` for transform-based viewport movement
- `AtlasGenerator` for packing Aseprite or PNG assets into texture atlases

The `Sprite` model is very simple, but intentionally useful:

```csharp
public class Sprite
{
    public Subtexture Texture;
    public float Scale;
    public float Rotation;

    public float Width => Texture.Width * Scale;
    public float Height => Texture.Height * Scale;

    public void Draw(in Batcher batcher, Vector2 position, Vector2 origin, bool mirrorX = false, bool mirrorY = false)
    {
        var xScale = (mirrorX) ? -Scale : Scale;
        var yScale = (mirrorY) ? -Scale : Scale;

        batcher.Image(Texture, position, origin,
            new Vector2(xScale, yScale), Rotation, Color.White);
    }
}
```

That means most visual behavior stays close to the underlying Foster layer while remaining easy to manipulate from C#.

### Animation support

`AnimatedSprite` maintains a dictionary of named animations, each of which contains an `Animation` object with the array of frames and delay between frames. It handles switching animations and stepping through the current sequence over time.

```csharp
public class AnimatedSprite : Sprite
{
    private readonly Dictionary<string, Animation> _animations;
    private string _currentAnimation;
    private int _currentFrame;
    private float _animationDelay;
    private float _elapsedTime;

    public AnimatedSprite(Dictionary<string, Animation> spriteAnimations,
        string startingAnimation,
        float spriteScale = 1,
        float spriteRotation = 0)
    {
        _animations = spriteAnimations;
        _currentAnimation = startingAnimation;
        _animationDelay = spriteAnimations[startingAnimation].Delay;
        _elapsedTime = 0;

        Scale = spriteScale;
        Rotation = spriteRotation;
    }

    public void PlayAnimation(string animationName, bool restartIfNotChanged = true)
    {
        if (_currentAnimation == animationName && !restartIfNotChanged)
            return;

        _currentAnimation = animationName;
        _animationDelay = _animations[_currentAnimation].Delay;
        _currentFrame = 0;
        UpdateTexture();
    }
}
```

This makes it easy to build actor states like idle, run, attack, and hurt without forcing a custom animation system.

### Camera and transforms

The camera class wraps a `Matrix3x2` transform and exposes `Origin`, `Position`, `Zoom`, and `Rotation`. It can be applied to a Foster `Batcher` and supports resetting back to its starting configuration.

```csharp
public class Camera(Vector2 origin, Vector2 startingPosition, bool relative = false)
{
    public Vector2 Origin = origin;
    public Vector2 Position = startingPosition;
    public Vector2 Zoom = new(1.0f);
    public float Rotation;

    public bool Relative = relative;

    public Matrix3x2 Matrix => Transform.CreateMatrix(Position, Origin, Zoom, Rotation);

    public void Apply(in Batcher batcher)
    {
        batcher.PushMatrix(Matrix, Relative);
    }
}
```

This is a straightforward viewport abstraction for 2D game work.

### Collision tooling

Baobab includes basic collision wrappers in the `Collisions` namespace:

- `BoundingBox` for axis-aligned rectangle intersections
- `CircleBound` for circular hit detection

`BoundingBox` can test intersection against another `BoundingBox` or a circle using a closest-point calculation. `CircleBound` supports distance-based circle-to-circle comparison and a rectangle intersection shortcut.

```csharp
public bool Intersects(BoundingBox other)
{
    return !(Right < other.Left ||
             Left > other.Right ||
             Bottom < other.Top ||
             Top > other.Bottom);
}
```

This is useful for simple 2D gameplay collisions without a large physics engine.

### Timers

The time utility is intentionally minimal. `Timer` gets a duration, accumulates `deltaTime`, and raises `OnComplete` when the interval is reached:

```csharp
public class Timer(float durationInSeconds)
{
    public event EventHandler? OnComplete;

    private float _elapsedTime;

    public void Update(float deltaTime, object? sender, EventArgs args)
    {
        _elapsedTime += deltaTime;

        if (_elapsedTime >= durationInSeconds)
        {
            _elapsedTime -= durationInSeconds;
            OnComplete?.Invoke(sender, args);
        }
    }
}
```

That gives you a simple way to trigger one-off or repeating behaviors tied to frame timing.

### Asset atlas generation

The `AtlasGenerator` class loads `.ase`, `.aseprite`, or `.png` assets, packs them together with Foster’s `Packer`, and exposes the generated `Subtexture`s by name.

```csharp
public void Pack()
{
    foreach (var asset in _assets)
    {
        var assetType = Path.GetExtension(asset);

        if (assetType == ".ase" || assetType == ".aseprite")
        {
            var file = new Aseprite(Path.Combine(ContentRoot, asset));
            var fileFrames = file.RenderAllFrames();

            if (fileFrames.Length > 1)
            {
                for (var i = 0; i < fileFrames.Length; i++)
                {
                    var assetName = Path.GetFileNameWithoutExtension(asset);
                    _packer.Add($"{assetName}{i}", fileFrames[i]);
                }
            }
            else
            {
                var assetName = Path.GetFileNameWithoutExtension(asset);
                _packer.Add(assetName, fileFrames[0]);
            }
        }
    }

    var atlasOutput = _packer.Pack();
    _atlas = new Texture(_graphicsDevice, atlasOutput.Pages[0]);
}
```

This is one of the more distinctive engine features in the repo, as it is built to work with sprite atlas pipelines and Aseprite workflow.

## How to use Baobab

The project README describes the intended usage pattern as a code-first engine layered on top of Foster. It specifically notes that there are no wrapper abstractions for the Foster game loop itself. Instead, you create a class inheriting from `Foster.Framework.App` and implement the lifecycle methods:

- `Startup`
- `Shutdown`
- `Update`
- `Render`

Then you instantiate the app and call `.Run()` to start the game.

The README also notes that the project is currently early-stage, but a NuGet package is already published:

```bash
dotnet package add BaobabEngine.Baobab
```

The engine is meant to be used by coding directly in C#, rather than relying on a built-in GUI editor.

## Project status and roadmap

The README outlines a short roadmap centered on features the project still wants to add:

1. GUI library
2. Audio support
3. More quality-of-life features

This indicates Baobab is currently a foundational engine with core rendering, scene, and helper systems already in place, while remaining in an early stage.

## Stack summary

- Language: C#
- Runtime: .NET 10 library project
- Framework: Foster Framework (`FosterFramework` 0.3.0)
- Repository package ID: `BaobabEngine.Baobab`

## Good next steps for developers

If you are adopting Baobab, the natural next step is to:

1. Create a Foster `App` subclass.
2. Define scenes, camera logic, and sprite atlases.
3. Build your game loop around `Update` and `Render` methods.
4. Add custom systems for gameplay, entities, or data flows without needing a rigid engine structure.

This matches the project’s central philosophy: freedom and clarity over editor-enforced conventions.

## License and attribution

This project is licensed under the MIT license, and it explicitly credits the Foster Framework as a foundational dependency.

---

This documentation was added to the `docs/engine-usage-and-features` branch for review.

