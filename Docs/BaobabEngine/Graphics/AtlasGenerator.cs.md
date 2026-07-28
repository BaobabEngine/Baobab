# [Atlas Generator](../../../BaobabEngine/Graphics/AtlasGenerator.cs)
Used to generate texture atlases that improve performance when drawing many images to the screen.

The atlas generator inherits from `IDisposable`. Make sure that you dispose of it when it is no longer needed.

## Constructors
- `public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice)`
- `public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice, List<string> assets)`   *The assets property is a collection of paths relative to the content root.*

## Properties
- `private Texture? _atlas` - Stores the texture that is generated from the atlas. There is no need to access it manually because textures are returned and already seperated through methods.
- `public string ContentRoot { get; }` - The path that all assets will be found under. Make sure this folder is included at build time so the assets can actually be found.
- `private readonly Dictionary<string, Subtexture> _textures` - The internal collection of texture references.

## Methods
- `public void AddAsset(string assetPath)` - Adds an asset to the atlas relative to the content root. Note that if assets are added to the atlas, the atlas must be manually repacked.
- `public void RemoveAsset(string assetPath)` - Removes an asset from the asset. Heed the aforementioned warning.
- `public Subtexture GetTexture(string textureName)` - Returns a reference to a texture from the atlas.
- `public void Pack()` - Packs the atlas and generates its texture.
- `public void Dispose()` - Disposes of the atlas and frees its memory. Make sure no references are still being used of the atlas before this method is called to ensure no bugs will arise.

*Tip:* Use the secondary constructor to avoid manually packing your atlas. The second atlas already takes the assets you will need and packs them.