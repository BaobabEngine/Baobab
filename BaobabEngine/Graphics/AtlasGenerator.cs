using Foster.Framework;
using System.IO;
using System.Collections.Generic;

namespace BaobabEngine.Graphics;

/// <summary>
///  A class that uses Aseprite files and Foster's Packer class to generate
///  texture atlases for your game.
/// </summary>
public class AtlasGenerator
{
    private readonly GraphicsDevice _graphicsDevice;
    
    private Texture _atlas;
    
    // The packer that will be used internally to pack images.
    private readonly Packer _packer = new();
    
    // The root directory where all assets will be found.
    public string ContentRoot { get; }

    // The paths to the assets relevant to the ContentRoot.
    private readonly List<string> _assets;

    // This dictionary will contain all the textures that are used in an atlas.
    private readonly Dictionary<string, Subtexture> _textures = new();

    public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice)
    {
        ContentRoot = contentRoot;
        _graphicsDevice = graphicsDevice;
    }

    public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice, List<string> assets)
    {
        ContentRoot = contentRoot;
        _graphicsDevice = graphicsDevice;
        _assets = assets;

        Pack();
    }

    // Make sure to never include the root folder when adding an asset.
    // The root folder for assets is always handled using ContentRoot. 
    public void AddAsset(string assetPath) => _assets.Add(Path.Combine(ContentRoot, assetPath));
    public void RemoveAsset(string assetPath) => _assets.Remove(Path.Combine(ContentRoot, assetPath));

    public Subtexture GetTexture(string textureName)
    {
        return _textures[textureName];
    }

    public void Pack()
    {
        // Clear the texture collection if it already has textures.
        if (_textures.Count > 0)
            _textures.Clear();
        
        // Loads the assets that were given.
        foreach (var asset in _assets)
        {
            var assetType = Path.GetExtension(asset);
            
            if (assetType == ".ase" || assetType == ".aseprite")
            {
                var file = new Aseprite(Path.Combine(ContentRoot, asset));
                var fileFrames = file.RenderAllFrames();

                // Adds the images and their frames (if animated) to the packer.
                if (fileFrames.Length > 1)
                    for (var i = 0; i < fileFrames.Length; i++)
                    {
                        // For sprites with multiple frames, returns names like, "spriteName0" or "spriteName1".
                        var assetName = Path.GetFileNameWithoutExtension(asset);
                        _packer.Add($"{assetName}{i}", fileFrames[i]);
                    }
                else // Used for single frame assets.
                {
                    var assetName = Path.GetFileNameWithoutExtension(asset);
                    _packer.Add(assetName, fileFrames[0]);
                }

                foreach (var image in fileFrames) image.Dispose();
            }
            else if (assetType == ".png")
            {
                // Load the image
                Image image = new(Path.Combine(ContentRoot, asset));
                var assetName = Path.GetFileNameWithoutExtension(asset);
                
                _packer.Add(assetName, image);

                image.Dispose();
            }
        }
        
        var atlasOutput = _packer.Pack();

        // Generates the atlas and adds entries to the Textures dictionary.
        _atlas = new Texture(_graphicsDevice, atlasOutput.Pages[0]);
        foreach (var entry in atlasOutput.Entries)
        {
            _textures[entry.Name] = new Subtexture(_atlas, entry.Source, entry.Frame);
        }
    }
}