using Foster.Framework;
using System;
using System.Numerics;

namespace BaobabEngine.UI;

public enum ThreeSliceTexture
{
    Left = 0,
    Middle = 1,
    Right = 2
}

public class ThreeSlice : IUiElement
{
    private Subtexture[] _textures;

    public Vector2 Position { get; set; }

    public float Width { get; set; }

    public float Scale { get; set; }

    public float Height => _textures[(int)ThreeSliceTexture.Left].Height * Scale;

    public bool IsVisible { get; set; }

    public Color TextureColor { get; set; }

    public ThreeSlice()
    {
        _textures = new Subtexture[3];
    }

    public ThreeSlice(Subtexture leftTexture,
        Subtexture middleTexture,
        Subtexture rightTexture)
    {
        _textures = [leftTexture, middleTexture, rightTexture];
    }
    
    public ThreeSlice(Subtexture leftTexture,
        Subtexture middleTexture,
        Subtexture rightTexture,
        Vector2 position,
        float width,
        float scale,
        Color color,
        bool isVisible = false)
    {
        _textures = [leftTexture, middleTexture, rightTexture];
        Position = position;
        Width = width;
        Scale = scale;
        TextureColor = color;
        IsVisible = isVisible;
    }

    public void SetTexture(ThreeSliceTexture selectedTexture, Subtexture newTexture)
    {
        _textures[(int)selectedTexture] = newTexture;
    }

    public Subtexture GetTexture(ThreeSliceTexture selectedTexture)
    {
        return _textures[(int)selectedTexture];
    }

    public void Draw(in Batcher batcher)
    {
        var drawingPos = Position;
        var middleSectionWidth = ((Width * Scale) - (_textures[(int)ThreeSliceTexture.Left].Width * Scale)) - (_textures[(int)ThreeSliceTexture.Right].Width * Scale);
        middleSectionWidth = Math.Clamp(middleSectionWidth, 0, float.MaxValue);
        var middleSectionScaleX = middleSectionWidth / _textures[(int)ThreeSliceTexture.Middle].Width;
        
        
        batcher.Image(_textures[(int)ThreeSliceTexture.Left], drawingPos, new Vector2(0), new Vector2(Scale), 0f, TextureColor);
        drawingPos.X += _textures[(int)ThreeSliceTexture.Left].Width * Scale;

        batcher.Image(_textures[(int)ThreeSliceTexture.Middle], drawingPos, new Vector2(0), new Vector2(middleSectionScaleX, Scale), 0f, TextureColor);
        drawingPos.X += middleSectionWidth;
        
        batcher.Image(_textures[(int)ThreeSliceTexture.Right], drawingPos, new Vector2(0), new Vector2(Scale), 0f, TextureColor);
    }
}